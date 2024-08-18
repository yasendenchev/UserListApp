namespace UserListApp.Application.DTO;
public class PagedUsersDTO
{
    public IEnumerable<UserDTO> Users { get; set; }

    public int? TotalCount { get; set; }
}
