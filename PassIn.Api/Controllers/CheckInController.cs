using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.CheckIns.DoCheckIn;
using PassIn.Communication.Responses;

namespace PassIn.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CheckInController : ControllerBase
{
    [HttpPost("{attendeeId}")]
    [ProducesResponseType(typeof(ResponseRegiteredJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    public IActionResult CheckIn([FromRoute] Guid attendeeId)
    {
        DoAttendeeCheckInUseCase useCase = new DoAttendeeCheckInUseCase();

        var checkInId = useCase.Execute(attendeeId); 

        return Created(string.Empty, checkInId);
    }
}
