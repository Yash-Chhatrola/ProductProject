using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductMVCProject.Models.DropDown
{
    [Table("countries")]
    public class Country
    {
        [Key]
        [Column("id")]  // Maps to the 'id' column in the database
        public int Id { get; set; }

        [Column("name")]  // Maps to the 'name' column in the database
        public string Name { get; set; }

        [Column("iso3")]  // Maps to the 'iso3' column in the database
        public string? Iso3 { get; set; }

        [Column("numeric_code")]  // Maps to the 'numeric_code' column in the database
        public string? NumericCode { get; set; }

        [Column("iso2")]  // Maps to the 'iso2' column in the database
        public string? Iso2 { get; set; }

        [Column("phonecode")]  // Maps to the 'phonecode' column in the database
        public string? Phonecode { get; set; }

        [Column("capital")]  // Maps to the 'capital' column in the database
        public string? Capital { get; set; }

        [Column("currency")]  // Maps to the 'currency' column in the database
        public string? Currency { get; set; }

        [Column("currency_name")]  // Maps to the 'currency_name' column in the database
        public string? CurrencyName { get; set; }

        [Column("currency_symbol")]  // Maps to the 'currency_symbol' column in the database
        public string? CurrencySymbol { get; set; }

        [Column("tld")]  // Maps to the 'tld' column in the database
        public string? Tld { get; set; }

        [Column("native")]  // Maps to the 'native' column in the database
        public string? Native { get; set; }

        [Column("region")]  // Maps to the 'region' column in the database
        public string Region { get; set; }

        [Column("region_id")]  // Maps to the 'region_id' column in the database
        public int? RegionId { get; set; }

        [Column("subregion")]  // Maps to the 'subregion' column in the database
        public string? Subregion { get; set; }

        [Column("subregion_id")]  // Maps to the 'subregion_id' column in the database
        public int? SubregionId { get; set; }

        [Column("nationality")]  // Maps to the 'nationality' column in the database
        public string? Nationality { get; set; }

        [Column("timezones")]  // Maps to the 'timezones' column in the database
        public string? Timezones { get; set; }

        [Column("translations")]  // Maps to the 'translations' column in the database
        public string? Translations { get; set; }

        [Column("latitude")]  // Maps to the 'latitude' column in the database
        public decimal? Latitude { get; set; }

        [Column("longitude")]  // Maps to the 'longitude' column in the database
        public decimal? Longitude { get; set; }

        [Column("emoji")]  // Maps to the 'emoji' column in the database
        public string? Emoji { get; set; }

        [Column("emojiU")]  // Maps to the 'emojiU' column in the database
        public string? EmojiU { get; set; }

        [Column("created_at")]  // Maps to the 'created_at' column in the database
        public DateTime CreatedAt { get; set; }

        [Column("updated_at")]  // Maps to the 'updated_at' column in the database
        public DateTime UpdatedAt { get; set; }

        [Column("flag")]  // Maps to the 'flag' column in the database
        public int Flag { get; set; }

        [Column("wikiDataId")]  // Maps to the 'wikiDataId' column in the database
        public string? WikiDataId { get; set; }
    }
}
