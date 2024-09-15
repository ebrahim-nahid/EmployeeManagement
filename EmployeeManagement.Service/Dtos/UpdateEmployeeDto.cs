using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Service.Dtos;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
    public string? PhotoPath { get; set; }
    public IFormFile? Photo { get; set; }
}