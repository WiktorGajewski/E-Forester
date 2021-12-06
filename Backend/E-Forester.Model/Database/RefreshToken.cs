﻿using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace E_Forester.Model.Database
{
    [Owned]
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }

        public string Token { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expires { get; set; }

        public DateTime? Revoked { get; set; }

        public bool IsExpired => DateTime.UtcNow > Expires;
        
        public bool IsRevoked => Revoked.HasValue;

        public bool IsActive => !IsExpired && !IsRevoked;
    }
}
