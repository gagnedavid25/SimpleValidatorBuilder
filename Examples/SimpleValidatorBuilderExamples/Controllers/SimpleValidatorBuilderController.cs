using Microsoft.AspNetCore.Mvc;

namespace SimpleValidatorBuilderExamples.Controllers;

[ApiController]
[Route("[controller]")]
public class SimpleValidatorBuilderController : ControllerBase
{
    private readonly ILogger<SimpleValidatorBuilderController> _logger;

    public SimpleValidatorBuilderController(ILogger<SimpleValidatorBuilderController> logger)
    {
        _logger = logger;
    }
}
