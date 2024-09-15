using EmployeeManagement.Repository.Contracts;
using EmployeeManagement.Repository.Entities;
using EmployeeManagement.Service.Contracts;
using EmployeeManagement.Service.Dtos;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Service.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmpolyeeRepository _employeeRepository;
    public EmployeeService(IEmpolyeeRepository empolyeeRepository)
    {
        _employeeRepository = empolyeeRepository;
    }
    public async Task<bool> Create(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
    {
        string photoPath = string.Empty;
        photoPath = await SavePhotoAsync(createEmployeeDto.Photo);
        var employee = new Employee
        {
            FirstName = createEmployeeDto.FirstName,
            LastName = createEmployeeDto.LastName,
            PhoneNumber = createEmployeeDto.PhoneNumber,
            Email = createEmployeeDto.Email,
            DateOfBirth = createEmployeeDto.DateOfBirth,
            PhotoPath = photoPath,
        };
        return await _employeeRepository.Create(employee, cancellationToken);
    }

    public async Task<EmployeeListViewModel> Get(EmployeeSearchFilterModel? employeeSearchFilterDto, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(employeeSearchFilterDto, cancellationToken);
        var totalRecord = await _employeeRepository.GetTotalRecords(employeeSearchFilterDto, cancellationToken);
        int totalPages = (int)Math.Ceiling((double)totalRecord / employeeSearchFilterDto.PageSize);

        var employeeDto = employee.Select(x => new EmployeeDto
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber,
            Email = x.Email,
            DateOfBirth = x.DateOfBirth,
            PhotoPath = x.PhotoPath
        }).ToList();
        var employeeListViewModel = new EmployeeListViewModel
        {
            EmployeesDtos = employeeDto,
            EmployeeSearchFilterModel = employeeSearchFilterDto,
            TotalPages = totalPages,

        };
        return employeeListViewModel;
    }

    public async Task<EmployeeDto> Get(int? id, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.Get(id, cancellationToken);
        var employeeDto = new EmployeeDto
        {
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            PhoneNumber = employee.PhoneNumber,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            PhotoPath = employee.PhotoPath
        };

        return employeeDto;
    }

    public async Task<bool> Update(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
    {
        string photoPath = string.Empty;
        // File upload logic
        photoPath = await SavePhotoAsync(updateEmployeeDto.Photo);
        var employee = new Employee
        {
            Id = updateEmployeeDto.Id,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            Email = updateEmployeeDto.Email,
            DateOfBirth = updateEmployeeDto.DateOfBirth,
            PhotoPath = photoPath
        };

        return await _employeeRepository.Update(employee, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await _employeeRepository.Delete(id, cancellationToken);
    }

    private async Task<string> SavePhotoAsync(IFormFile photo)
    {
        if (photo == null)
        {
            return string.Empty;
        }

        var folderPath = Path.Combine("wwwroot", "uploads");

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photo.FileName);
        var filePath = Path.Combine(folderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await photo.CopyToAsync(stream);
        }

        return "/uploads/" + fileName;
    }

}
