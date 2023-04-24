namespace OnionBaseArchitecture.Domain.Attibutes
{
    public class ColumnName : Attribute
    {
        public ColumnName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
