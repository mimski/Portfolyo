using API.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortfolioController(
    IUploadPortfolioService uploadService,
    IGetPortfolioService getService,
    ILogger<PortfolioController> logger) : ControllerBase
{
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            logger.LogWarning("File not provided or empty.");

            return BadRequest("No file found");
        }

        logger.LogInformation("Received upload: {Name} ({Size} bytes)", file.FileName, file.Length);

        try
        {
            var portfolio = await uploadService.UploadAndComputeAsync(file.OpenReadStream());

            return Ok(PortfolioDto.FromDomain(portfolio));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error uploading portfolio");

            return StatusCode(500, "Internal error uploading portfolio");
        }
    }

    [HttpGet]
    public IActionResult GetPortfolio()
    {
        var portfolio = getService.GetCurrentPortfolio();

        if (portfolio == null)
        {
            return NotFound("Portfolio not found");
        }

        return Ok(PortfolioDto.FromDomain(portfolio));
    }
}
