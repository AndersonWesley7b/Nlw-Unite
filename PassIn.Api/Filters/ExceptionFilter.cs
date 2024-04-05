using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using System.Net;

namespace PassIn.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext _context)
    {
        bool result = _context.Exception is PassInException;

        if (result)
        {
            HandleProjectException(_context);
        }
        else
        {
            ThrowUnkowError(_context);
        }
    }
     
    private void HandleProjectException(ExceptionContext _context)
    {
        if(_context.Exception is NotFoundException)
        {
            _context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            _context.Result = new NotFoundObjectResult(new ResponseErrorJson(_context.Exception.Message));
        }
        else if(_context.Exception is ErrorOnValidationException)
        {
            _context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            _context.Result = new BadRequestObjectResult(new ResponseErrorJson(_context.Exception.Message));
        }
        else if (_context.Exception is ConflictException)
        {
            _context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Conflict;
            _context.Result = new ConflictObjectResult(new ResponseErrorJson(_context.Exception.Message));
        }
    }
    
    private static void ThrowUnkowError(ExceptionContext _context)
    {
        _context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        _context.Result = new ObjectResult(new ResponseErrorJson("Unknown error"));
    }

}
