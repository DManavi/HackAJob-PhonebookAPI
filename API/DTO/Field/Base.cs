using System.ComponentModel.DataAnnotations;

namespace API.DTO.Field
{
    public abstract class Base
    {
        [Required]
        public virtual string Category { get; set; }

        [Required]
        public virtual string Attribute { get; set; }

        [Required]
        public virtual string Value { get; set; }
    }
}