using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;

namespace School.MVC.Controllers
{
    public class ClassController : Controller
    {
        private readonly IClassService _classService;

        public ClassController(IClassService classService)
        {
            _classService = classService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _classService.Get(15, "Class15"));
        }
    }
}
