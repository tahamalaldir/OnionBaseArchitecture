using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("EmailAccount")]
    public class EmailAccount : BaseEntity
    {
        public string DisplayName { get; set; }

        public string SystemName { get; set; }

        public string Subject { get; set; }

        public string FromAddress { get; set; }

        public string ToAddress { get; set; }

        public bool SSLTLS { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpAddress { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpPassword { get; set; }

        public string MessageFormat { get; set; }

        public bool UseDefaultCredentials { get; set; }

    }
}
