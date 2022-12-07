using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;

namespace School.MVC.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _teacherService.Get(15, "Teacher15"));
        }
    }
}
