﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Persistence
{
    public class MusicStoreUserIdentity : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; } = default!;

        [StringLength(100)]
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public DocumentTypeEnum DocumentType { get; set; }

        [StringLength(100)]
        public string DocumentNumber { get; set; } = default!;


    }

    public enum DocumentTypeEnum
    {
        Dni,
        Passport
    }
}
