using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("ApiUser")]
    public class ApiUser : BaseEntity
    {

        public string Username { get; set; }

        public string Password { get; set; }

    }
}
