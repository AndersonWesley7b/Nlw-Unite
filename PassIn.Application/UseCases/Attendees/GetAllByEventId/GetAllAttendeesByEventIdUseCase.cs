using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEventId;
public class GetAllAttendeesByEventIdUseCase
{
    private readonly PassInDbContext _passInDbContext;
    public GetAllAttendeesByEventIdUseCase()
    {
        _passInDbContext = new PassInDbContext();
    }
    public ResponseAllAttendeesjson Execute(Guid _eventId)
    {
        var entity = _passInDbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(ev => ev.Id == _eventId);
        if (entity is null)
            throw new NotFoundException("An event with this id does not exist");

        return new ResponseAllAttendeesjson
        {
            Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
            { 
                Id = attendee.Id,
                Email = attendee.Email,
                CreatedAt = DateTime.UtcNow,
                Name = attendee.Name,
                CheckedInAt = attendee.CheckIn?.Created_At 
            }).ToList(),

        };
    }
}
