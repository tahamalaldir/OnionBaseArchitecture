using OnionBaseArchitecture.Domain.Attibutes;
using OnionBaseArchitecture.Domain.Entities.Common;

namespace OnionBaseArchitecture.Domain.Entities
{
    [TableName("Language")]
    public class Language : BaseEntity
    {
        public string ShortCode { get; set; }
        public string Name { get; set; }
    }
}
