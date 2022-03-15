using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    /// <summary>
    /// Model contains all the details which user have which product 
    /// </summary>
    public class Cart_Item
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Cart_Id { get; set; }
        public int Product_Id { get; set; }
        public int? Count { get; set; } = 0;
    }
}
