using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Entity
{
    public class BaseEntity<Tkey> : IEntity<Tkey>
    {
        public Tkey id { get; set; }
    }
}
