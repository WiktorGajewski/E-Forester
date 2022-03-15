using E_Forester.Model.Enums;
using System;
using System.Collections.Generic;

namespace E_Forester.Model.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }
        public ICollection<ForestUnit> AssignedForestUnits { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }

        public User()
        {
            AssignedForestUnits = new List<ForestUnit>();
            RefreshTokens = new List<RefreshToken>();
        }
    }
}
