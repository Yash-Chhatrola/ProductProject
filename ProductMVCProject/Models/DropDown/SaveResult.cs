using Microsoft.VisualBasic;

namespace ProductMVCProject.Models.DropDown
{
    public class SaveResult
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; } = string.Empty;
        public string CityName { get; set; } = string.Empty;
        public DateTime DataCreated { get; set; }
    }

    public class DrpRequestDTO
    {
        public int CountryId { get; set; }
        public int? StateId { get; set; } = 0;
        public int? CityId { get; set; } = 0;
    }

    public class DrpRequestDTOMulti
    {
        public List<int> CountryId { get; set; } = new(); // ✅ array
        public int? StateId { get; set; } = 0;
        public int? CityId { get; set; } = 0;
    }
}
