using CarvillaExamTRY.Areas.Manage.ViewModels.ServicesVMs;
using CarvillaExamTRY.DAL;
using CarvillaExamTRY.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarvillaExamTRY.Areas.Manage.Controllers
{
	[Area("Manage")]
	[Authorize]
	public class ServicesController : Controller
	{
		private readonly AppDbContext _db;

		public ServicesController(AppDbContext db)
        {
			_db = db;
		}
		[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
		{
			List<Service> services = await _db.Services.ToListAsync();
			return View(services);
		}
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ServiceCreateVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			Service service = new Service()
			{
				Title = vm.Title,
				Description = vm.Description,	
				Icon = vm.Icon
			};
			await _db.Services.AddAsync(service);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}
		[Authorize(Roles = "Admin")]
		public async  Task<IActionResult> Update(int id)
		{
			var service = await _db.Services.FindAsync(id);
			ServiceUpdateVM updated = new ServiceUpdateVM()
			{
				Title = service.Title,
				Description = service.Description,
				Icon = service.Icon
			};
			return View(updated);
		}
		[HttpPost]
		public async Task<IActionResult> Update(ServiceUpdateVM vm)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			Service service  = await _db.Services.FindAsync(vm.Id);
			if(service == null)
			{
				throw new Exception("Service is null.");
			};
			service.Title = vm.Title;
			service.Description = vm.Description;
			service.Icon = vm.Icon;
			await _db.SaveChangesAsync();	
			return RedirectToAction("Index");

		}
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int id)
		{
			var service = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
			_db.Services.Remove(service);
			await _db.SaveChangesAsync();
			return RedirectToAction("Index");
		}

	}
}
