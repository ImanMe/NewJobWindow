using JobWindowNew.Domain.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace JobWindowNew.Domain.ViewModels
{
    public class AssignRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

        public IEnumerable<IdentityRole> Roles { get; set; }

        [Required]
        public string UserId { get; set; }

        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}
