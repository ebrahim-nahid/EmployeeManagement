using EmployeeManagement.Repository.Contracts;
using EmployeeManagement.Repository.DatabaseContext;
using EmployeeManagement.Repository.Entities;
using EmployeeManagement.Service.Dtos;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Repositories;

public class EmployeeRepository : IEmpolyeeRepository
{
    private readonly AppDbContext _DbContext;
    public EmployeeRepository(AppDbContext appDbContext)
    {
        _DbContext = appDbContext;
    }

    public async Task<bool> Create(Employee employee, CancellationToken cancellationToken)
    {
        _DbContext.Employees.Add(employee);
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }


    public async Task<int> GetTotalRecords(EmployeeSearchFilterModel searchModel, CancellationToken cancellationToken)
    {
        var query = _DbContext.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(searchModel.SearchName))
        {
            query = query.Where(e => e.FirstName.Contains(searchModel.SearchName) || e.LastName.Contains(searchModel.SearchName));
        }

        if (!string.IsNullOrEmpty(searchModel.SearchEmail))
        {
            query = query.Where(e => e.Email.Contains(searchModel.SearchEmail));
        }

        if (!string.IsNullOrEmpty(searchModel.SearchMobile))
        {
            query = query.Where(e => e.PhoneNumber.Contains(searchModel.SearchMobile));
        }

        if (searchModel.SearchDOB.HasValue)
        {
            query = query.Where(e => e.DateOfBirth == searchModel.SearchDOB.Value);
        }

        return await query.CountAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Employee>> Get(EmployeeSearchFilterModel? searchModel, CancellationToken cancellationToken)
    {
        var query = _DbContext.Employees.AsQueryable();

        if (!string.IsNullOrEmpty(searchModel.SearchName))
        {
            query = query.Where(e => e.FirstName.Contains(searchModel.SearchName) || e.LastName.Contains(searchModel.SearchName));
        }

        if (!string.IsNullOrEmpty(searchModel.SearchEmail))
        {
            query = query.Where(e => e.Email.Contains(searchModel.SearchEmail));
        }

        if (!string.IsNullOrEmpty(searchModel.SearchMobile))
        {
            query = query.Where(e => e.PhoneNumber.Contains(searchModel.SearchMobile));
        }

        if (searchModel.SearchDOB.HasValue)
        {
            query = query.Where(e => e.DateOfBirth == searchModel.SearchDOB.Value);
        }

        return await query
            .Skip((searchModel.CurrentPage - 1) * searchModel.PageSize)
            .Take(searchModel.PageSize)
            .ToListAsync(cancellationToken);

    }

    public async Task<Employee?> Get(int? id, CancellationToken cancellationToken)
    {
        var employee = await _DbContext.Employees.FindAsync(id, cancellationToken);
        if (employee == null)
        {
            Console.WriteLine($"employee with id {id} is not found.");
        }
        return employee;
    }

    public async Task<bool> Update(Employee employee, CancellationToken cancellationToken)
    {
        _DbContext.Entry(employee).State = EntityState.Modified;
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var employee = await Get(id, cancellationToken);
        if (employee != null)
        {
            _DbContext.Remove(employee);
        }
        return await _DbContext.SaveChangesAsync(cancellationToken) > 0;
    }

}
