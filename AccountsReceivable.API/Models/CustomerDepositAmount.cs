using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AccountsReceivable.API.Models
{
    public class CustomerDepositAmount
    {
        [Key]
        public int Id { get; set; }
        public string Msg { get; set; }
    }
}
