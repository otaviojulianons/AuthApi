using System.Collections.Generic;

namespace Auth.Common.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data = default, bool success = false)
        {
            Data = data;
            Success = success;
        }

        public T Data { get; private set; }
        public bool Success { get; private set; }
        public List<Notification> Errors { get; private set; } = new List<Notification>();

        public BaseResponse<T> Error(string errorMessage)
        {
            Errors.Add(new Notification(errorMessage));
            return this;
        }
            
    }
}
