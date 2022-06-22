namespace kaleyra_WhatsApp
{
    public class WhatsAppModel
    {
        public string? to { get; set; }
        public string? type { get; set; }
        public bool preview_url { get; set; }
        public string? callback { get; set; }
        public Text? text { get; set; }
    }

    public class Text
    {
        public string? message { get; set; }
    }
}
