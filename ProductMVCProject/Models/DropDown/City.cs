using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMVCProject.Models.DropDown
{
    [Table("cities")]
    public class City
    {
        [Key]
        [Column("id")]  // Maps to the 'id' column in the database
        public int Id { get; set; }

        [Column("name")]  // Maps to the 'name' column in the database
        public string Name { get; set; }

        [Column("state_id")]  // Maps to the 'state_id' column in the database
        public int StateId { get; set; }

        [Column("state_code")]  // Maps to the 'state_code' column in the database
        public string StateCode { get; set; }

        [Column("country_id")]  // Maps to the 'country_id' column in the database
        public int CountryId { get; set; }

        [Column("country_code")]  // Maps to the 'country_code' column in the database
        public string CountryCode { get; set; }

        [Column("latitude")]  // Maps to the 'latitude' column in the database
        public decimal Latitude { get; set; }

        [Column("longitude")]  // Maps to the 'longitude' column in the database
        public decimal Longitude { get; set; }

        [Column("created_at")]  // Maps to the 'created_at' column in the database
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]  // Maps to the 'updated_at' column in the database
        public DateTime UpdatedAt { get; set; }

        [Column("flag")]  // Maps to the 'flag' column in the database
        public bool Flag { get; set; }

        [Column("wikiDataId")]  // Maps to the 'wiki_data_id' column in the database
        public string? WikiDataId { get; set; }
    }
}
