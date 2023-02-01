namespace MoviesMVC.ViewModels
{
    public class ItemVM
    {
        public int ItemId { get; set; }

        [Required]
        public string ItemName { get; set; } = string.Empty;
    }

    public class CheckboxVM : ItemVM
    {
        public string ItemImage { get; set; } = string.Empty;
        public bool IsSelected { get; set; }
    }
}