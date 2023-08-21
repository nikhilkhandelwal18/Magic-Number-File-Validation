using FileValidation.API.FileValidation.DTOs;
using FileValidation.API.FileValidation.Utilities;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FileValidation.API.FileValidation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileValidationController : ControllerBase
    {
        [SwaggerOperation(Tags = new[] { "File Validation" })]
        [HttpPost]
        [Route("filevalidation")]
        public async Task<IActionResult> ValidateFileWithMagicNumber(FileRequest fileRequest)
        {
            if (fileRequest == null)
                return BadRequest("Can not be null");

            if (fileRequest.SupportedTypes == null || !fileRequest.SupportedTypes.Any())
                return BadRequest($"At least one supported type is required.");

            if (fileRequest.Attachments == null || !fileRequest.Attachments.Any())
                return BadRequest($"At least one attachment is required.");

            bool isValid = FileValidationUtils.MagicNumberValidation(fileRequest.SupportedTypes.ConvertAll(s => s.ToUpper()), fileRequest.Attachments);

            return Ok(isValid);
        }

    }
}
