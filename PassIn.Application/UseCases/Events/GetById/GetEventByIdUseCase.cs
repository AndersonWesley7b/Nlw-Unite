using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.GetById;
public class GetEventByIdUseCase
{
    public ResponseEventJson Execute(Guid id)
    {
        var dbContext = new PassInDbContext();

        var entity = dbContext.Events.Find(id);
        if (entity is null)
            throw new PassInException("An events with this id dont exist.");

        return new ResponseEventJson
        {
            Id = entity.Id,
            MaximumAttendees = entity.Maximum_Attendees,
            Details = entity.Details,
            Title = entity.Title,
            AttendeesAmount = -1
        };

    }
}
