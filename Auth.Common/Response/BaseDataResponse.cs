using System.Collections.Generic;

namespace Auth.Common.Response
{
    public class BaseResponse<T> : BaseResponse
    {
        public BaseResponse(T data = default, bool success = false) : base(success)
        {
            Data = data;
        }

        public T Data { get; private set; }

        new public BaseResponse<T> Error(string errorMessage) => (BaseResponse<T>)base.Error(errorMessage);
    }
}
