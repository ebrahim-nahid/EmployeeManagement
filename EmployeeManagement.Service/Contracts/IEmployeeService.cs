using EmployeeManagement.Service.Dtos;
using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Service.Contracts;

public interface IEmployeeService
{
    Task<bool> Create(CreateEmployeeDto category, CancellationToken cancellationToken);
    Task<EmployeeListViewModel> Get(EmployeeSearchFilterModel employeeSearchFilterModel, CancellationToken cancellationToken);
    Task<EmployeeDto?> Get(int? id, CancellationToken cancellationToken);
    Task<bool> Update(UpdateEmployeeDto category, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
}
