namespace OnionBaseArchitecture.Domain.Attibutes
{
    public class TableName : Attribute
    {
        public TableName(string name, string schemeName = "dbo")
        {
            SchemeName = schemeName;
            Name = name;
        }

        public string SchemeName { get; set; }
        public string Name { get; set; }

    }
}
