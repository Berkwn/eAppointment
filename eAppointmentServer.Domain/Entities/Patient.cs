namespace eAppointmentServer.Domain.Entities;

public sealed class Patient
{
    public Patient()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public string FullName => string.Join(" ", FirstName, LastName);
    public string IdentityNumber { get; set; } = default!;
    public string City { get; set; }=string.Empty;
    public string Town { get; set; } = default!;
    public string FullAddress { get; set; } = string.Empty;


}
