namespace Auth.Common.Response
{
    public class Notification
    {
        public Notification(string errorMessage)
        {
            Message = errorMessage;
        }

        public string Message { get; set; }
    }
}
