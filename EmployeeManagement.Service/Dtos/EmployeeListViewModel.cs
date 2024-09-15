using EmployeeManagement.Service.Dtos;

namespace EmployeeManagement.Web.Models;

public class EmployeeListViewModel
{
    public IReadOnlyList<EmployeeDto> EmployeesDtos { get; set; }
    public EmployeeSearchFilterModel EmployeeSearchFilterModel { get; set; }
    public int TotalPages { get; set; }

}
