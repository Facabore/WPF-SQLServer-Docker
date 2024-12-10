using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_SQLSERVER.Abstractions
{
    public class Entity
    {
        public Guid Id { get; init; }
        protected Entity(Guid id)
        {
            Id = id;
        }
    }
}
