using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EF_Pagination_Example.Test.Helpers
{
    public static class Response
    {
        public static string GetResponse<T>(ActionResult<T> actionResult)
        {
            var objectResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            return JsonConvert.SerializeObject(objectResult.Value);
        }
    }
}