using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;
using School.MVC.DAL.Interfaces.Repositories;

namespace School.MVC.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService subjectService)
        {
            _subjectService = subjectService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _subjectService.Get(15, "Subject15"));
        }
    }
}
