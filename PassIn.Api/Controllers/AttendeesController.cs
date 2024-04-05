using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Attendees.GetAllByEventId;
using PassIn.Application.UseCases.Events.GetById;
using PassIn.Application.UseCases.Events.NewFolder.RegisterAttendee;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AttendeesController : ControllerBase
{
    [HttpPost("{eventId}/register")]
    [ProducesResponseType(typeof(ResponseRegiteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult Register([FromRoute] Guid eventId, [FromBody] RequestRegisterEventJson request)
    {
        var useCase = new RegisterAttendeeOnEventUseCase();

        var response = useCase.Execute(eventId, request);

        return Created(string.Empty, response);
    }

    [HttpGet("{eventId}")]
    [ProducesResponseType(typeof(ResponseRegiteredJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseAllAttendeesjson), StatusCodes.Status404NotFound)]
    public IActionResult GetAll([FromRoute] Guid eventId)
    {
        var useCase = new GetAllAttendeesByEventIdUseCase();

        var response = useCase.Execute(eventId);

        return Ok(response);
    }
}
