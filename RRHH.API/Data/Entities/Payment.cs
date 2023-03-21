namespace RRHH.API.Data.Entities;

public class Payment
{
    public string Id { get; set; } = new Guid().ToString();
    public DateTime Date { get; set; }
    public int CorrespondingMonth { get; set; }
    public virtual Employee Employee { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal EmployeeAFPDiscount { get; set;}
    public decimal EmployeeInsuranceDiscount { get;set;}
    public decimal CompanyAFPDiscount { get; set; }
    public decimal CompanyInsuranceDiscount { get; set; }
    public decimal NetAmount { get { return GrossAmount-EmployeeAFPDiscount-EmployeeInsuranceDiscount; } }
}