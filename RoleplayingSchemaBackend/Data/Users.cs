﻿using Microsoft.AspNetCore.Identity;

namespace RoleplayingSchemaBackend.Data
{
    public class Users : IdentityUser
    {
        public string Firstname { get; set; }
        public ICollection<Sheet> Sheets { get; set; }
    }
}
