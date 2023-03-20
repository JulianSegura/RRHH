using Microsoft.AspNetCore.Identity;

namespace RRHH.API.Data.Entities;
public class AplicationUser : IdentityUser
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public bool Enabled { get; set; }
}