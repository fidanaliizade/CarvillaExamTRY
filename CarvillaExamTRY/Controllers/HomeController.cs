using CarvillaExamTRY.DAL;
using CarvillaExamTRY.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarvillaExamTRY.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _db.Services.ToListAsync();
            return View(services);
        }
    }
}
