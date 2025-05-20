namespace ChatApp.Entities
{
    public class Message
    {
        public required string User { get; set; }
        public required string Text { get; set; }
        public bool Offensive { get; set; }

    }
}
