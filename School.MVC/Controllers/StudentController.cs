using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;

namespace School.MVC.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _studentService.Get(15, "Student15"));
        }
    }
}
