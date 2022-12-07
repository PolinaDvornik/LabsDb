using Microsoft.AspNetCore.Mvc;
using School.MVC.BLL.Interfaces.Services;

namespace School.MVC.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return View(await _scheduleService.Get(15, "Schedule15"));
        }
    }
}
