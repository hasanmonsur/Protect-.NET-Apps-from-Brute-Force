using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebSeriLogApi.Contacts;
using WebSeriLogApi.Helpers;
using WebSeriLogApi.Models;
using WebSeriLogApi.Services;

namespace WebSeriLogApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        private readonly ILogger<SampleController> _logger;
        private readonly IMaskService _maskService;

        public SampleController(ILogger<SampleController> logger, IMaskService maskService)
        {
            _logger = logger;
            _maskService = maskService;
        }

        [HttpPost]
        public IActionResult SubmitForm(UserInputModel model)
        {
            // Log user input exclude sensitive data
            _logger.LogInformationWithoutSensitiveData("Received input from user: {@UserInput}", model);

            // Log user input without sensitive data
            _logger.LogInformation("Received input from user: {@UserInput}", model);

            // Avoid logging sensitive data
            if (model.Password != null)
            {
                _logger.LogWarning("Password provided for user: {UserEmail}", model.Email);
                // Do not log the actual password
            }

            // Mask logging sensitive data
            if (model.Email != null)
            {
                var vemail = _maskService.MaskEmail(model.Email);
                _logger.LogWarning("Mask Email provided for user: {UserEmail}", vemail);
                // Do not log the actual Email
            }

            return Ok();
        }
    }
}
