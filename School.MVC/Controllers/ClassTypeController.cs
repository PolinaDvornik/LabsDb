using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;

namespace School.MVC.Controllers
{
    public class ClassTypeController : Controller
    {
        private readonly IClassTypeService _classTypeService;

        public ClassTypeController(IClassTypeService classTypeService)
        {
            _classTypeService = classTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _classTypeService.Get(15, "ClassType15"));
        }
    }
}
