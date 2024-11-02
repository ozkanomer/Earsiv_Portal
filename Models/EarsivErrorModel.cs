namespace Earsiv_Portal.Models
{
    public class EarsivErrorModel
    {
        public string? Error { get; set; }
        public List<Message> Messages { get; set; }
    }

    public class Message
    {
        public string? Type { get; set; }
        public string? Text { get; set; }
    }
}
