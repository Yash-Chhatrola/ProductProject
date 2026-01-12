using System.ComponentModel.DataAnnotations;

namespace ProductMVCProject.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        public string? Description { get; set; }
        [Range(0,10, ErrorMessage = "Value Must not be Greater then 10")]
        public int Stock { get; set; }

    }
}
