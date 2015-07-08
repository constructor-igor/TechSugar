namespace Demo
{
    public class UserMessage
    {
        public string Message { get; private set; }             // immutable

        public UserMessage(string message)
        {
            Message = message;
        }
    }

    public class ResponseMessage
    {
        public string Response { get; private set; }            // immutable

        public ResponseMessage(string response)
        {
            Response = response;
        }
    }
}
