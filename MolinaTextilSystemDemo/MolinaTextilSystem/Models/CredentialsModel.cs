namespace MolinaTextilSystem.Models
{
    public class CredentialsModel
    {
        public string EmployeeUsername { get; set; }
        public string EmployeePassword { get; set; }
        public EmployeeRol EmployeeRolID { get; set; }
    }

    public enum EmployeeRol
    {
        Administrator,
        Employee
    }
}
