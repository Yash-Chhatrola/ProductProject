using Microsoft.AspNetCore.Mvc;
using ProductMVCProject.Data;

namespace ProductMVCProject.Controllers
{
    public class DropdownController : Controller
    {
        private readonly AppDbContext context;

        public DropdownController(AppDbContext context)
        {
            this.context = context;
        }
        public JsonResult Country()
        {
            var cnt = context.Countries.ToList();
            return new JsonResult(cnt);
        }
        public JsonResult State(int id)
        {
            var st = context.States.Where(e=> e.CountryId == id).ToList();
            return new JsonResult(st);
        }
        public JsonResult City(int id)
        {
            var ct = context.Cities.Where(e=> e.StateId == id);
            return new JsonResult(ct);
        }
        public IActionResult CascadeDropdown()
        {
            return View();
        }
    }
}
