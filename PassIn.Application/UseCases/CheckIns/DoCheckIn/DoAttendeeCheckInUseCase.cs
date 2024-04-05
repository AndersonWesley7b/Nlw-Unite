using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;


namespace PassIn.Application.UseCases.CheckIns.DoCheckIn;
public class DoAttendeeCheckInUseCase
{

    private readonly PassInDbContext _dbContext;

    public DoAttendeeCheckInUseCase()
    {
        _dbContext = new PassInDbContext();
    }

    public ResponseRegiteredJson Execute(Guid attendeeId)
    {
        Validate(attendeeId);

        var entity = new CheckIn
        {
            Attendee_Id = attendeeId,
            Created_At = DateTime.UtcNow,
        };

        _dbContext.CheckIns.Add(entity);
        _dbContext.SaveChanges();

        return new ResponseRegiteredJson
        {
            Id = entity.Id,
        };

    }

    private void Validate(Guid attendeeId)
    {
        var existsAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);

        if (existsAttendee)
            throw new NotFoundException("The attendee with this Id was not found.");

        var existCheckIn = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);

        if (existCheckIn)
            throw new ConflictException("Attendee can not do checking twice in the same event.");

    }
}
