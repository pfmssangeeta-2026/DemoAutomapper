namespace JWTToeknDemo.Models
{
    public class ProblemStatement
    {
        public string Statuscode { get; set; } = "500";
        public string Title { get; set; } = "Internal Server Error";
        public string Description { get; set; }
        public string traceId { get; set; }
    }
}
