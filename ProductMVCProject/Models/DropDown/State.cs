using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMVCProject.Models.DropDown
{
    [Table("states")]
    public class State
    {
        [Key]
        [Column("id")]  // Maps to the 'id' column in the database
        public int Id { get; set; }

        [Column("name")]  // Maps to the 'name' column in the database
        public string Name { get; set; }

        [Column("country_id")]  // Maps to the 'country_id' column in the database
        public int CountryId { get; set; }

        [Column("country_code")]  // Maps to the 'state_code' column in the database
        public string CountryCode { get; set; }

        [Column("iso2")]  // Maps to the 'iso2' column in the database
        public string? Iso2 { get; set; }

        [Column("fips_code")]  // Maps to the 'fips_code' column in the database
        public string? FipsCode { get; set; }

        [Column("type")]  // Maps to the 'type' column in the database
        public string? Type { get; set; }

        [Column("latitude")]  // Maps to the 'latitude' column in the database
        public decimal? Latitude { get; set; }

        [Column("longitude")]  // Maps to the 'longitude' column in the database
        public decimal? Longitude { get; set; }

        [Column("created_at")]  // Maps to the 'created_at' column in the database
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]  // Maps to the 'updated_at' column in the database
        public DateTime UpdatedAt { get; set; }

        [Column("flag")]  // Maps to the 'flag' column in the database
        public bool Flag { get; set; }

        [Column("wikiDataId")]  // Maps to the 'wikiDataId' column in the database
        public string? WikiDataId { get; set; }
    }
}
