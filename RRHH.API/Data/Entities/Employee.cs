namespace RRHH.API.Data.Entities;

public class Employee
{
    public string Id { get; set; } = new Guid().ToString();
    public string EmployeeCode { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateHired { get; set; }
    public decimal MonthlySalary { get; set; }
    public virtual Position Position { get; set; }
    public virtual ICollection<Payment> Payments { get; set; } = new HashSet<Payment>();
    public bool Enabled { get; set; }
}