using Microsoft.AspNetCore.Mvc;
using ProductMVCProject.Data;
using ProductMVCProject.Models.DropDown;

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
        public IActionResult SaveData([FromBody] DrpRequestDTO req)
        {
            var countryName = context.Countries.Where(e => e.Id == req.CountryId).Select(e => e.Name).FirstOrDefault() ?? "";
            var stateName = context.States
                .Where(e => e.Id == req.StateId)
                .Select(e => e.Name)
                .FirstOrDefault() ?? "";

            var cityName = context.Cities
                .Where(e => e.Id == req.CityId)
                .Select(e => e.Name)
                .FirstOrDefault() ?? "";

            // Create the record using only the strings
            var result = new SaveResult
            {
                CountryName = countryName,
                StateName = stateName,
                CityName = cityName,
                DataCreated = DateTime.Now
            };

            context.saveResults.Add(result);
            context.SaveChanges();

            return Ok(new { success = true });
        }
        [HttpPost]
        public IActionResult SaveData1(int CountryId,int StateId ,int CityId)
        {
            if (CountryId == 0)
                return BadRequest("Invalid selection");

            // store the name of id 
            var countryName = context.Countries.Where(e => e.Id == CountryId).Select(e => e.Name).FirstOrDefault() ?? "";
            var stateName = context.States.Where(e => e.Id == StateId).Select(e => e.Name).FirstOrDefault() ?? "No State Available";
            var cityName = context.Cities.Where(e => e.Id == CityId).Select(e => e.Name).FirstOrDefault() ?? "No City Available";

            // Create the record using only the strings
            var result = new SaveResult
            {
                CountryName = countryName,
                StateName = stateName,
                CityName = cityName,
                DataCreated = DateTime.Now
            };

            context.saveResults.Add(result);
            context.SaveChanges();

            return Ok(new { success = true });
        }
    }
}
