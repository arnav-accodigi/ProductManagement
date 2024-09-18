using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ProductManagement.Data.Exceptions;

namespace ProductManagement.API.Controllers;

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
            return new ObjectResult(new { error = ex.Message }) { StatusCode = statusCode };
        }
    }
}
