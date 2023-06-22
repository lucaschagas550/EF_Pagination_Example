using Microsoft.AspNetCore.Identity;

namespace EF_Pagination_Example.ViewModels
{
    public class ClaimCreateViewModel : ClaimViewModel
    {
        public IdentityRole Role{ get; set; }

        public ClaimCreateViewModel() { }

        public ClaimCreateViewModel(IdentityRole role, string value, string type) : base(value, type)
        {
            Role = role;
        }
    }
}