using Core.Entities;

namespace Entities.DTOs.User;

public class UserForLoginDto:IDto
{
    public string Username { get; set; }
    public string Password { get; set; }

}