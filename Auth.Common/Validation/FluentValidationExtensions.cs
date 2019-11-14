using Auth.Common.Response;
using FluentValidation;

namespace Auth.Common.Validation
{
    public static class FluentValidationExtensions
    {
        public static bool IsValid<T>(this AbstractValidator<T> validator,T data, BaseResponse response)
        {
            var validationResult = validator.Validate(data);
            foreach (var item in validationResult.Errors)
                response.Errors.Add(new Notification(item.ErrorMessage));
            return validationResult.IsValid;
        }
    }
}