namespace Data_Access_Layer.Views
{
    /// <summary>
    /// View Used to Display The Product information to the Users
    /// </summary>
    public class ViewProductDetails
    {
        public int Id { get; set; }
        public string Name { get; set; }=String.Empty;
        public int Price { get;set; }
        public string Category_Name { get; set; }=String.Empty ;
    }
}
