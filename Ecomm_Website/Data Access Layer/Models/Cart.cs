using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Access_Layer.Models
{
    /// <summary>
    /// Model Used to Map one to one relationship to Cart table and User Table
    /// </summary>
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }
        public int User_Id { get; set; }
    }
}
