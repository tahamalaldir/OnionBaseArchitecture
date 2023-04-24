using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("LanguageText")]
    public class LanguageText : BaseEntity
    {
        public Guid LanguageId { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

    }
}
