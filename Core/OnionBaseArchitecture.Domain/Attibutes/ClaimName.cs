namespace OnionBaseArchitecture.Domain.Attibutes
{
    public class ClaimName : Attribute
    {
        public ClaimName(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}
