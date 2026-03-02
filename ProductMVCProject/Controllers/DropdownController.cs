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
            var st = context.States.Where(e => e.CountryId == id).ToList();
            return new JsonResult(st);
        }
        public JsonResult City(int id)
        {
            var ct = context.Cities.Where(e => e.StateId == id).ToList();
            return new JsonResult(ct);
        }
        public IActionResult CascadeDropdown()
        {
            return View();
        }
        public IActionResult SaveData([FromBody] DrpRequestDTO req)
        {
            if (req.CountryId == 0 || req.CountryId == null)
                return BadRequest("Invalid selection");

            var countryName = context.Countries.Where(e => e.Id == req.CountryId).Select(e => e.Name).FirstOrDefault() ?? "";
            var stateName = context.States.Where(e => e.Id == req.StateId).Select(e => e.Name).FirstOrDefault() ?? "No State Available";
            var cityName = context.Cities.Where(e => e.Id == req.CityId).Select(e => e.Name).FirstOrDefault() ?? "No City Available";

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
        public IActionResult SaveData1(int CountryId, int StateId, int CityId)
        {
            if (CountryId == 0 || CountryId == null)
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

        // ===================== MULTI SELECTED DROPDOWN =====================

        public JsonResult CountryMulti()
        {
            var cnt = context.Countries.ToList();
            return new JsonResult(cnt);
        }

        // NOTE: accepts multiple ids
        public JsonResult StateMulti([FromQuery] List<int> id)
        {
            var st = context.States.Where(e => id.Contains(e.CountryId)).ToList();
            return new JsonResult(st);
        }

        public JsonResult CityMulti(int id)
        {
            var ct = context.Cities.Where(e => e.StateId == id).ToList();
            return new JsonResult(ct);
        }
        public IActionResult MultiSaveData([FromBody] DrpRequestDTOMulti req)
        {
            if (req.CountryId == null || req.CountryId.Count == 0)
                return BadRequest("Invalid selection");
            var countryNames = context.Countries.Where(c => req.CountryId.Contains(c.Id)).Select(c => c.Name).ToList();
            // Join into one string
            var countryNameText = string.Join(", ", countryNames);

            var stateName = context.States.Where(e => e.Id == req.StateId).Select(e => e.Name).FirstOrDefault() ?? "No State Available";
            var cityName = context.Cities.Where(e => e.Id == req.CityId).Select(e => e.Name).FirstOrDefault() ?? "No City Available";

            var result = new SaveResult
            {
                CountryName = countryNameText,
                StateName = stateName,
                CityName = cityName,
                DataCreated = DateTime.Now
            };
            context.saveResults.Add(result);
            context.SaveChanges();

            return Ok(new { success = true });
        }

        // shows latest single save
        public JsonResult LatestSingle()
        {
            var last = context.saveResults
                .OrderByDescending(x => x.Id)
                .Select(x => new {
                    x.Id,
                    x.CountryName,
                    x.StateName,
                    x.CityName,
                    x.DataCreated
                })
                .FirstOrDefault();

            return new JsonResult(last);
        }

        // shows latest multi save (same table, but countryname has multiple text)
        public JsonResult LatestMulti()
        {
            var last = context.saveResults
                .OrderByDescending(x => x.Id)
                .Select(x => new {
                    x.Id,
                    x.CountryName,
                    x.StateName,
                    x.CityName,
                    x.DataCreated
                })
                .FirstOrDefault();

            return new JsonResult(last);
        }
    }
}
