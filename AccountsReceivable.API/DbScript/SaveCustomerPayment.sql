go
IF EXISTS ( SELECT * 
            FROM   sysobjects 
            WHERE  id = object_id(N'SaveCustomerPayment') 
                   and OBJECTPROPERTY(id, N'IsProcedure') = 1 )
BEGIN
    DROP PROCEDURE SaveCustomerPayment
END
go
CREATE PROC SaveCustomerPayment
@CompanyId AS INT,
@CustomerId AS INT,
@TransactionAmount AS DECIMAL(10,2),
@TransactionModeId AS INT,
@TransactionType AS NVARCHAR(200),--DEPOSIT, ORDER PAYMENT ETC
@TransactionDate AS DATETIME,
@CardNumber AS NVARCHAR(50),
@CardHolderName AS NVARCHAR(MAX),
@ChequeNo AS NVARCHAR(500),
@ChequeHolderName AS NVARCHAR(MAX),
@Note AS NVARCHAR(MAX),
@CreatedFrom AS NVARCHAR(500),
@OrderDetails AS NVARCHAR(MAX),--<ORDERID-DUEAMOUNT#>
@UserId AS INT,
@CreatedAt DATETIME
AS
BEGIN
	DECLARE @CustomerWalletId AS INT
	DECLARE @WalletTransactionModeId AS INT
	DECLARE @DepositCustomerWalletTransactionId AS INT
	DECLARE @DepositCustomerWalletTransactionDetail AS INT
	DECLARE @BalanceTransactionAmount AS DECIMAL(10,2) = @TransactionAmount

	SET @CustomerWalletId = (SELECT TOP 1 CustomerWalletId FROM CustomerWallet WHERE CustomerId = @CustomerId)
	SET @WalletTransactionModeId = (SELECT TOP 1 TransactionModeId FROM TransactionMode WHERE ModeName = 'Wallet')

	INSERT INTO 
		CustomerWalletTransaction 
			(CustomerWalletId, TransactionAmount, TransactionType, TransactionModeId, TransactionDate, CardNumber, CreatedBy, CreatedDate, CardHolderName, ChequeNo, ChequeHolderName, Note)
	VALUES 
			(@CustomerWalletId, @TransactionAmount, 'Deposit', @TransactionModeId, @TransactionDate, @CardNumber, @UserId, @CreatedAt, @CardHolderName, @ChequeNo, @ChequeHolderName, @Note)
	SET @DepositCustomerWalletTransactionId = SCOPE_IDENTITY()

	INSERT INTO 
		CustomerWalletTransactionDetail 
			(CustomerWalletTransactionId, ReferenceTable, ReferenceId, Amount, CreatedBy, CreatedDate)
	VALUES 
			(@DepositCustomerWalletTransactionId, NULL, NULL, @TransactionAmount, @UserId, @CreatedAt)
	SET @DepositCustomerWalletTransactionDetail = SCOPE_IDENTITY()

	IF(@TransactionType = 'ORDER PAYMENT' AND ISNULL(@OrderDetails,'') <> '')
	BEGIN
		DECLARE @RowID INT
		DECLARE @Value NVARCHAR(500)

		DECLARE CURSOR_ORDPAYMENT CURSOR
		FOR
			SELECT RowID, Value from dbo.Split('#', @OrderDetails)
		OPEN CURSOR_ORDPAYMENT
		FETCH NEXT FROM CURSOR_ORDPAYMENT INTO @RowID, @Value
		WHILE @@FETCH_STATUS = 0
		BEGIN
		   IF(ISNULL(@Value,'') <> '')
		   BEGIN
				DECLARE @OrderId AS INT
				DECLARE @OrderDueAmount AS DECIMAL(10,2)
				DECLARE @OrderPaidAmount AS DECIMAL(10,2) = 0.00

				SET @OrderId = (SELECT TOP 1 Value FROM dbo.Split('-', @Value) WHERE RowID = 1)
				SET @OrderDueAmount = ISNULL((SELECT TOP 1 Value FROM dbo.Split('-', @Value) WHERE RowID = 2),0.00)

				IF(ISNULL(@BalanceTransactionAmount,0.00)>=ISNULL(@OrderDueAmount,0.00))
				BEGIN
					SET @OrderPaidAmount = ISNULL(@OrderDueAmount,0.00)
					SET @BalanceTransactionAmount = ISNULL(@BalanceTransactionAmount,0.00) - ISNULL(@OrderDueAmount,0.00)
				END
				ELSE IF (ISNULL(@BalanceTransactionAmount,0.00)>0.00)
				BEGIN
					SET @OrderPaidAmount = ISNULL(@BalanceTransactionAmount,0.00)
					SET @BalanceTransactionAmount = ISNULL(@BalanceTransactionAmount,0.00) - ISNULL(@OrderDueAmount,0.00)
				END

				IF(ISNULL(@OrderPaidAmount,0.00)>0.00)
				BEGIN
					DECLARE @OrdPaymentCustomerWalletTransactionId AS INT
					DECLARE @OrdPaymentCustomerWalletTransactionDetail AS INT

					INSERT INTO 
						CustomerWalletTransaction 
							(CustomerWalletId, TransactionAmount, TransactionType, TransactionModeId, TransactionDate, CardNumber, CreatedBy, CreatedDate)
					VALUES 
							(@CustomerWalletId, @OrderPaidAmount, 'ORDER PAYMENT', @WalletTransactionModeId, @TransactionDate, @CardNumber, @UserId, @CreatedAt)
					SET @OrdPaymentCustomerWalletTransactionId = SCOPE_IDENTITY()

					INSERT INTO 
						CustomerWalletTransactionDetail 
							(CustomerWalletTransactionId, ReferenceTable, ReferenceId, Amount, CreatedBy, CreatedDate)
					VALUES 
							(@OrdPaymentCustomerWalletTransactionId, 'CustomerWalletTransaction', @DepositCustomerWalletTransactionId, @OrderPaidAmount, @UserId, @CreatedAt)
					SET @OrdPaymentCustomerWalletTransactionDetail = SCOPE_IDENTITY()

					INSERT INTO 
						OrderPayment
							(OrderId, CustomerWalletId, Amount, TransactionModeId, TransactionDate, CreatedBy, CreatedDate)
					VALUES
							(@OrderId, @CustomerWalletId, @OrderPaidAmount, @TransactionModeId, @TransactionDate, @UserId, @CreatedAt)
				END
		   END
			
			FETCH NEXT FROM CURSOR_ORDPAYMENT INTO @RowID, @Value
		END;
		CLOSE CURSOR_ORDPAYMENT;
		DEALLOCATE CURSOR_ORDPAYMENT;
	END
SELECT @CustomerWalletId AS CustomerWalletId, @CustomerId AS CustomerId, 'Success' AS [Message]
END
GO