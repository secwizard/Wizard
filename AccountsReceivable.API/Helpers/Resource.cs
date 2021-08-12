using Newtonsoft.Json;
namespace AccountsReceivable.API.Helpers
{
    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
