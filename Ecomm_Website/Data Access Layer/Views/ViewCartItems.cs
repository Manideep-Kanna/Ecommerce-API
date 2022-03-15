namespace Data_Access_Layer.Views
{
    /// <summary>
    /// View used to show products which are in Cart of the specific User
    /// </summary>
    public class ViewCartItems
    {
        public string Name { get; set; }=String.Empty;
        public int count { get; set; }
        public int Cost { get; set; }
    }
}
