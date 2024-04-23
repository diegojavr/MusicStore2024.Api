using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.Dto.Request
{
    public class RegisterDtoRequest
    {
        [Required]
        [StringLength(200)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(200)]
        public string LastName { get; set; } = default!;

        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(20)]
        public string DocumentNumber { get; set; } = default!;
        public int DocumentType { get; set; }
        public int Age { get; set; }

        [Required]
        public string Password { get; set; } = default!;

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;
    }
}
