using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid _id)
    {
        var dbContext = new PassInDbContext();

        var entity = dbContext.Events.Include(ev => ev.Attendees).FirstOrDefault(ev => ev.Id == _id);
        if (entity is null)
            throw new NotFoundException("An events with this id dont exist.");

        return new ResponseEventJson
        {
            Id = entity.Id,
            MaximumAttendees = entity.Maximum_Attendees,
            Details = entity.Details,
            Title = entity.Title,
            AttendeesAmount = entity.Attendees.Count
        };

    }
}
