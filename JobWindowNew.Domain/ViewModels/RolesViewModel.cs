﻿using System.Collections.Generic;

namespace JobWindowNew.Domain.ViewModels
{
    public class RolesViewModel
    {
        public IEnumerable<string> RoleNames { get; set; }
        public string UserName { get; set; }
    }
}
