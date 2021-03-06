﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dark.Core.Domain.Entity;

namespace Dark.Core.Authorization.Users
{
    public class Sys_UserClaim : EntityBase
    {
        [Required]
        public virtual int UserId { get; set; }

        [Required]
        public virtual string ClaimType { get; set; }

        [Required]
        public virtual string ClaimValue { get; set; }

        public Sys_UserClaim()
        {

        }

        public Sys_UserClaim(Sys_Account user, Claim claim)
        {
            UserId = user.Id;
            ClaimType = claim.Type;
            ClaimValue = claim.Value;
        }
    }
}
