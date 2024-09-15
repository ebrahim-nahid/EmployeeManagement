using EmployeeManagement.Service.Contracts;
using EmployeeManagement.Service.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index(EmployeeSearchFilterModel employeeSearchFilterDto, CancellationToken cancellationToken)
        {
            var employees = await _employeeService.Get(employeeSearchFilterDto, cancellationToken);
            return View(employees);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Get(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEmployeeDto createEmployeeDto, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                bool result = await _employeeService.Create(createEmployeeDto, cancellationToken);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(createEmployeeDto);
        }

        public async Task<IActionResult> Edit(int? id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Get(id, cancellationToken);
            if (employee == null)
            {
                return NotFound();
            }
            var updateEmployeeDto = new UpdateEmployeeDto
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DateOfBirth = employee.DateOfBirth,
                PhotoPath = employee.PhotoPath,
            };

            return View(updateEmployeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateEmployeeDto updateEmployeeDto, CancellationToken cancellationToken)
        {
            if (updateEmployeeDto.Id <= 0)
            {
                return BadRequest();
            }


            if (ModelState.IsValid)
            {
                bool result = await _employeeService.Update(updateEmployeeDto, cancellationToken);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(updateEmployeeDto);
        }

 
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var employee = await _employeeService.Delete(id, cancellationToken);
            if (!employee)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
