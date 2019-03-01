using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }
}