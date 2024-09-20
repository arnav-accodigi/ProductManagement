using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Data.Exceptions;

namespace ProductManagement.API.Controllers;

public class ErrorResponse(string errorMessage)
{
    public string Error { get; set; } = errorMessage;
}

public abstract class BaseController : ControllerBase
{
    protected async Task<IActionResult> HandleAsync(Func<Task<IActionResult>> func)
    {
        try
        {
            var result = await func();
            return result;
        }
        catch (Exception ex)
        {
            var statusCode = ex switch
            {
                RecordNotFoundException => StatusCodes.Status404NotFound,
                ValidationException => StatusCodes.Status400BadRequest,
                _ => StatusCodes.Status500InternalServerError
            };
            return new ObjectResult(new ErrorResponse(ex.Message)) { StatusCode = statusCode };
        }
    }
}
