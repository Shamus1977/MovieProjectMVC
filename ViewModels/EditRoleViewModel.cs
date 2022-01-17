using System.ComponentModel.DataAnnotations;

namespace L8HandsOn.ViewModels
{
    public class EditRoleViewModel
    {
        public string? Id { get; set; }
        [Required(ErrorMessage ="Role Name Required")]
        public string? RoleName { get; set; }
        public List<String>? UsersInRoles { get; set; }

        public EditRoleViewModel()
        {
            UsersInRoles = new List<String>();
        }
    }
}
