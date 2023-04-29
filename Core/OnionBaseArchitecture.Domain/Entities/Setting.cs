using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("Setting")]
    public class Setting : BaseEntity
    {
        public string SystemName { get; set; }

        public string Value { get; set; }
    }
}
