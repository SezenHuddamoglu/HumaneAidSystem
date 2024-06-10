using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Enums;
using System;

namespace HumaneAidSystem.Backend.CleanArchitecture.CleanArchitecture.Application.Entities
{
    public class User : AuditableBaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastPasswordChangedDate { get; set; }
        public string Name { get; internal set; }
        public string Token { get; internal set; }
        public Roles Role { get; set; }
    }
}
