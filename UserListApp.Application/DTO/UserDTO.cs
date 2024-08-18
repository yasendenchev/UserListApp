namespace UserListApp.Application.DTO;
public class UserDTO
{
    public int? Id { get; set; }

    [NameValidation]
    public string? Name { get; set; }

    [EmailValidation]
    public string? Email { get; set; }

    [PhoneValidation]
    public string? PhoneNumber { get; set; }
}
