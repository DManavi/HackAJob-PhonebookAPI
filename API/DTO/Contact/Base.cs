using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTO.Contact
{
    public abstract class Base
    {
        [Required]
        public virtual ICollection<Field.Read> Fields { get; set; }
    }
}