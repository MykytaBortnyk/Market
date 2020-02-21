using System;
using System.ComponentModel.DataAnnotations;

namespace AppCore.Models
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
    }
}
