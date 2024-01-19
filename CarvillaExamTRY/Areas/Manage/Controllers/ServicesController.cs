using CarvillaExamTRY.Areas.Manage.ViewModels.ServicesVMs;
using CarvillaExamTRY.DAL;
using CarvillaExamTRY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarvillaExamTRY.Areas.Manage.Controllers
{
	[Area("Manage")]
	public class ServicesController : Controller
	{
		private readonly AppDbContext _db;

		public ServicesController(AppDbContext db)
        {
			_db = db;
		}
        public async Task<IActionResult> Index()
		{
			List<Service> services = await _db.Services.ToListAsync();
			return View(services);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ServiceCreateVM vm)
		{
			return View();
		}
		public IActionResult Update(int id)
		{
			return View();
		}
		[HttpPost]
		public IActionResult Update(ServiceUpdateVM vm)
		{
			return View();
		}
		public IActionResult Delete(int id)
		{
			return View();
		}

	}
}
