namespace Data_Access_Layer.Views
{
    /// <summary>
    /// View Used to show when an user add a Product into his cart 
    /// </summary>
    public class ViewAddProduct
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int? count { get; set; }
    }
}
