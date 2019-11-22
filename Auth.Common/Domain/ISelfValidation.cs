using Auth.Common.Response;

namespace Auth.Common.Domain
{
    public interface ISelfValidation
    {
         bool IsValid(BaseResponse response);
    }
}