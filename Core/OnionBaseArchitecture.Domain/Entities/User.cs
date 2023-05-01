using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("Users")]
    public class User : BaseEntity
    {
        public int UserTypeCode { get; set; } = 101;

        public string Email { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Password { get; set; }

        public DateTime PasswordExpireDate { get; set; }

        public string Token { get; set; }

        public DateTime? TokenExpireDate { get; set; }

        public bool EmailApproved { get; set; } = false;

        public string EmailToken { get; set; }

        public DateTime? EmailTokenExpireDate { get; set; }

        public bool PhoneApproved { get; set; } = false;

        public string PhoneToken { get; set; }

        public DateTime? PhoneTokenExpireDate { get; set; }

        public bool IsApproved { get; set; } = true;

    }
}
