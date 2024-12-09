namespace Application.Common.Record.Home
{
    public class AddContactRecord
    {
        public required string FullName { set; get; }
        public string? PhoneNumber { set; get; }
        public string? Message { set; get; }
        public string? Subject { set; get; }
    }
}
