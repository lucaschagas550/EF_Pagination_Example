using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EF_Pagination_Example.Test.Helpers
{
    public static class InitializeController
    {
        public static void Context(ControllerBase controllerBase)
        {
            var controllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext()
            };

            controllerBase.ControllerContext = controllerContext;
        }
    }
}