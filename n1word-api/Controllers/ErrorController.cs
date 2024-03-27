using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using n1word_api.Service;
using System.Collections.Generic;

namespace n1word_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [Route("/Error")]
        public IActionResult HandelError() 
        {
            // 可以從HttpContext中獲取到異常信息
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context?.Error;
            
            var code = 500; // 內部服務錯誤

            if (exception is Exception) code = 500; // 例子: 自定義異常類型和錯誤代碼
            
            
            _logger.LogError(exception, "全局異常捕獲");
            Response.StatusCode = code; // 設置HTTP響應狀態碼

            // 記錄異常信息，返回錯誤響應
            return new ObjectResult(new
            {
                error = new
                {
                    message = "阿！伺服器發生了點問題，請稍後再試。",
                }
            });

        }
    }
}
