using InterviewTask.API.Domain.Enums;
using InterviewTask.API.Feature.Authentication.GetRoleFeature;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InterviewTask.API.Shared
{
    public class CustomizedAuthorize : ActionFilterAttribute
    {
        private readonly Features _features;
        private readonly IRoleFeature _roleFeature;
        private readonly UserState _userState;

        public CustomizedAuthorize(Features features , IRoleFeature roleFeature , UserState userState)
        {
            _features = features;
            _roleFeature = roleFeature;
            _userState = userState;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var user =  context.HttpContext.User;

        }
    }
}
