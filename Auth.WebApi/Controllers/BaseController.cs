using Auth.Common.Response;
using Microsoft.AspNetCore.Mvc;

namespace Auth.WebApi.Controllers
{
    public class BaseController : Controller
    {
        protected BaseResponse<T> FormatResponse<T>(BaseResponse<T> response)
        {
            Response.StatusCode = response.Success ? 200 : 400;
            return response;
        }
    }
}