using Microsoft.AspNetCore.Http;

namespace EmployeeManagement.Service.Dtos;

public class CreateEmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IFormFile Photo { get; set; }
}