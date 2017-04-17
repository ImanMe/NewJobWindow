using JobWindowNew.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class AssignRoleViewModel
    {
        [Required]
        [DisplayName("Role")]
        public string RoleName { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        [Required]
        [DisplayName("User Name")]
        public string UserId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
