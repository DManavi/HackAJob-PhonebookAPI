
namespace API.DTO.Field
{
    public abstract class Base
    {
        public virtual string Category { get; set; }

        public virtual string Attribute { get; set; }

        public virtual string Value { get; set; }
    }
}