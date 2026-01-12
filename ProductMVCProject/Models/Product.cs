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
        [Required]
        public int Price {  get; set; }
        public string? Description { get; set; }
        [Range(0,int.MaxValue, ErrorMessage = "Stock Must Be Positive")]
        public int Stock { get; set; }

    }
}
