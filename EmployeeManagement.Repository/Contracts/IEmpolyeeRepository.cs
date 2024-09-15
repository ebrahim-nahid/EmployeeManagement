using EmployeeManagement.Repository.Entities;
using EmployeeManagement.Service.Dtos;

namespace EmployeeManagement.Repository.Contracts;

public interface IEmpolyeeRepository
{
    Task<bool> Create(Employee employee, CancellationToken cancellationToken);
    Task<IReadOnlyList<Employee>> Get(EmployeeSearchFilterModel employeeSearchFilterModel, CancellationToken cancellationToken);
    Task<Employee?> Get(int? id, CancellationToken cancellationToken);
    Task<bool> Update(Employee employee, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
    Task<int> GetTotalRecords(EmployeeSearchFilterModel searchModel, CancellationToken cancellationToken);

}
