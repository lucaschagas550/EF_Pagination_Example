namespace EF_Pagination_Example.ViewModels
{
    public class ClaimViewModel
    {
        public string Value { get; set; } = null!;
        public string Type { get; set; } = null!;

        public ClaimViewModel() { }

        public ClaimViewModel(string value, string type)
        {
            Value=value;
            Type=type;
        }
    }
}