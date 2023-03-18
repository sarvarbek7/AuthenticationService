using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Api.Models.Users
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class User : IdentityUser<Guid>
    {
        public override Guid Id
        {
            get => base.Id;
            set => base.Id = value;
        }

        [Required]
        public override string PhoneNumber
        {
            get => base.PhoneNumber;
            set => base.PhoneNumber = value;
        }

        public override string? Email
        {
            get => base.Email;
            set => base.Email = value;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Mosque { get; set; }
        public Gender Gender { get; set; }
        public string? ProfilePhotoUrl { get; set; }
        public DateTimeOffset? BirthDate { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

    }
}
