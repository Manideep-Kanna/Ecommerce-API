using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    /// <summary>
    /// Model for the Product Details
    /// </summary>
    public class Product
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        //Navigation Properties
        public int Category_Id { get; set; }

        [MaxLength(100)]
        public string? Status { get; set; } = "Active";
    }
}
