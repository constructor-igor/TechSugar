namespace Task1.Producer
{
    public class ProducerMessage
    {
        public string Text { get; set; }
        public int Duration { get; set; }

        public ProducerMessage()
        {
        }
        public ProducerMessage(string text, int duration)
        {
            Text = text;
            Duration = duration;
        }
    }
}
