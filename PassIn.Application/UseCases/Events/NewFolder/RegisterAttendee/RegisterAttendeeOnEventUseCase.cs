using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PassIn.Application.UseCases.Events.NewFolder.RegisterAttendee;
public class RegisterAttendeeOnEventUseCase
{

    private readonly PassInDbContext _dbContext;

    public RegisterAttendeeOnEventUseCase()
    {
        _dbContext = new PassInDbContext();
    }

    public ResponseRegiteredJson Execute(Guid _eventId, RequestRegisterEventJson request)
    {
        Validate(_eventId, request);

        var entity = new Attendee
        {
            Created_At = DateTime.UtcNow,
            Email = request.Email,
            Event_Id = _eventId,
            Name = request.Name,
        };

        _dbContext.Attendees.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegiteredJson
        {
            Id = entity.Id,
        };

    }

    private void Validate(Guid _eventId, RequestRegisterEventJson request)
    {
        var eventEntity = _dbContext.Events.Find(_eventId);

        if (eventEntity is null)
            throw new NotFoundException("An event with this id does not exist.");

        if (string.IsNullOrEmpty(request.Name))
            throw new ErrorOnValidationException("The name is invalid.");

        if (!EmailIsValid(request.Email))
            throw new ErrorOnValidationException("The e-mail is invalid.");

        var attendeeAlreadyRegistered = _dbContext
            .Attendees
            .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == _eventId);

        if (attendeeAlreadyRegistered)
            throw new ConflictException("You can not register twice on the same event.");

        var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == _eventId);

        if(attendeesForEvent > eventEntity.Maximum_Attendees)
            throw new ErrorOnValidationException("There is no room for this event.");

    }

    private bool EmailIsValid(string _email)
    {
        try
        {
            new MailAddress(_email);

            return true;
        }
        catch
        {
            return false;
        }  
    }

}
