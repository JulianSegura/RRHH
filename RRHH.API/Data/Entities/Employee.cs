namespace RRHH.API.Data.Entities;

public class Employee
{
    public string Id { get; init; } = new Guid().ToString();
    public string EmployeeNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateHired { get; set; }
    public decimal Salary { get; set; }
    public virtual Position Position { get; set; }
    public bool Enabled { get; set; }
}