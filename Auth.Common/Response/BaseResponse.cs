using System.Collections.Generic;

namespace Auth.Common.Response
{
    public class BaseResponse<T>
    {
        public BaseResponse(T data = default)
        {
            Data = data;
        }

        public T Data { get; set; }
        public List<Notification> Errors { get; set; }

        public BaseResponse<T> Error(string errorMessage)
        {
            Errors.Add(new Notification(errorMessage));
            return this;
        }
            
    }
}
