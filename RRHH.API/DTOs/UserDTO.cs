using RRHH.API.Data.Entities;
using System.Runtime.CompilerServices;

namespace RRHH.API.DTOs;

public class UserDTO
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
}
