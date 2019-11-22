using System.Collections.Generic;

namespace Auth.Common.Response
{
    public class BaseResponse
    {
        public BaseResponse(bool success = false)
        {
            Success = success;
        }

        public bool Success { get; private set; }

        public List<Notification> Errors { get; private set; } = new List<Notification>();

        public BaseResponse Error(string errorMessage)
        {
            Errors.Add(new Notification(errorMessage));
            return this;
        }
            
    }
}
