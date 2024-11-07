namespace eAppointmentServer.Application.Features.Users.GetAllUsers;

public sealed record GetAllUsersQueryResponse
{
    public  Guid id { get; set; }
    public string firstName { get; set; } =string.Empty;
    public string LastName { get; set; }= string.Empty;
    public string fullName { get; set; }= string.Empty;
    public string email { get; set; } = string.Empty;
    public string userName { get; set; } = string.Empty;
    public List<Guid>? roleIds { get; set; }
    public List<string?> RoleNames { get; set; } = new();

}
   

