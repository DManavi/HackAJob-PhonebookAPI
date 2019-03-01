using System.Collections.Generic;


namespace API.DTO.Contact
{
    public abstract class Base
    {
        public virtual ICollection<Field.Read> Fields { get; set; }
    }
}