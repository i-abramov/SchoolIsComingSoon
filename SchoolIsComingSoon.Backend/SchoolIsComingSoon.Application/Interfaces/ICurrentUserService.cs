namespace SchoolIsComingSoon.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string UserName { get; }
        string FirstName { get; }
        string LastName { get; }
        string Email { get; }
        string Role { get; }
    }
}