using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.Events.Register;
public class RegisterEventUseCase
{
    public ResponsesRegiteredEventJson Execute(RequestEventJson request)
    {
        Validate(request);

        var dbContext = new PassInDbContext();

        var Entity = new Event
        {
            Title = request.Title,
            Details = request.Details,
            Maximum_Attendees = request.MaximumAttendees,
            Slug = request.Title.ToLower().Replace(" ", "-"),
        };

        dbContext.Events.Add(Entity);
        dbContext.SaveChanges();

        return new ResponsesRegiteredEventJson
        {
            Id = Entity.Id,
        };

    }

    private void Validate(RequestEventJson request)
    {
        if (request.MaximumAttendees <= 0)
            throw new PassInException("The Maximum attendes is invalid!");

        if (string.IsNullOrWhiteSpace(request.Title))
            throw new PassInException("The title is invalid!");
        
        if (string.IsNullOrWhiteSpace(request.Details))
            throw new PassInException("The details is invalid!");
    }

}
