namespace Domain.Entities.System
{
    public class LogEntity : BaseEntity
    {

        public DateTime CreatedTime { get; set; }
        public string? Level { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public string? Exception { get; set; }
        public string? Logger { get; set; }
        public string? RequestUrl { get; set; }
        public string? RequestType { get; set; }

    }
}
