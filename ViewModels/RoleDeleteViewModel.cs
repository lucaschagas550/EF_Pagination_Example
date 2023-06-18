namespace EF_Pagination_Example.ViewModels
{
    public class RoleDeleteViewModel
    {
        public string Id { get; set; }

        public RoleDeleteViewModel() { }
        public RoleDeleteViewModel(string id)
        {
            Id = id;
        }
    }
}
