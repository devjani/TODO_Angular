using System.ComponentModel.DataAnnotations;
using static TODO.Model.Constants.Constants;

namespace TODO.Model.Account
{
    public class User : BaseEntity
    {
        public int EquoId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Occupation { get; set; }
        public string ProfileImage { get; set; }
        public string DateOfBirth { get; set; }
        [Required]
        [RegularExpression(RegexConstants.EmailFormat)]
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Password { get; set; }
        public string SkillDescription { get; set; }
        public bool IsSubscribed { get; set; }
        public int RoleId { get; set; }

        public int StepNo { get; set; }

        public string Token { get; set; }

        public string RefreshToken { get; set; }
        public bool IsEmailVerified { get; set; }

        public string Base64String { get; set; }

        public string Company { get; set; }

        public bool IsFundProvidingActive { get; set; }

        public bool IsChannelActive { get; set; }

        public string ClosingRemark { get; set; }

        public bool IsVolunteeringActive { get; set; }
        public int? BusinessProfileId { get; set; }

        public bool IsMarketplaceActive { get; set; }        
    }

    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; }
    }
}
