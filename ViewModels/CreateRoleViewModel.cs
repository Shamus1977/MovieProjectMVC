using System.ComponentModel.DataAnnotations;

namespace L8HandsOn.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set;}
    }
}
