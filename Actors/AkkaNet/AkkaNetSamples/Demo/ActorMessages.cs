namespace Demo
{
    public class UserMessage
    {
        public string Message { get; private set; }

        public UserMessage(string message)
        {
            Message = message;
        }
    }

    public class ResponseMessage
    {
        public string Response { get; private set; }

        public ResponseMessage(string response)
        {
            Response = response;
        }
    }
}
