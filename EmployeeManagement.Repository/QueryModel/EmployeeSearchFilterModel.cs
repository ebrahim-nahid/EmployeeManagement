namespace EmployeeManagement.Service.Dtos;

public class EmployeeSearchFilterModel
{
    public string? SearchName { get; set; }
    public string? SearchEmail { get; set; }
    public string? SearchMobile { get; set; }
    public DateTime? SearchDOB { get; set; }

    // Pagination properties
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 2;
    public int TotalPages { get; set; } = 1;
    public int TotalRecords { get; set; } = 0;
}
