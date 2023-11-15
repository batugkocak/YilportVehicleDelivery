using Core.Entities;

namespace Entities.DTOs.User;

public class UserForRegisterDto : IDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int RoleId { get; set; }
}