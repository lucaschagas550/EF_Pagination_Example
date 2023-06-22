using Microsoft.AspNetCore.Identity;

namespace EF_Pagination_Example.ViewModels
{
    public class ClaimDeleteViewModel : ClaimViewModel
    {
        public string RoleId { get; set; }
        public ClaimDeleteViewModel() { }
        public ClaimDeleteViewModel(string roleId, string value, string type) : base(value, type) 
        { 
            RoleId = roleId;
        }
    }
}
