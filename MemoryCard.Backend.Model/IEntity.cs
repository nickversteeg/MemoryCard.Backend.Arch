using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryCard.Backend.Models
{
    public interface IEntity
    {
        string Guid { get; set; }
    }
}
