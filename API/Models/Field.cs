using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Field
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Categories would be email, personal info or etc
        /// </summary>
        [Required]
        public string Category { get; set; }

        [Required]
        public string Attribute { get; set; }

        [Required]
        public string Value { get; set; }


        [Required]
        public virtual Contact Contact { get; set; }
    }
}