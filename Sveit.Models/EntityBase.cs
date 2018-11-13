using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sveit.Models
{
    public abstract class EntityBase
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Deleted { get; set; }
    }
}
