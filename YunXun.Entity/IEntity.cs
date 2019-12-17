using System;
using System.Collections.Generic;
using System.Text;

namespace YunXun.Entity
{
    public interface IEntity<Tkey>
    {
        Tkey id { get; set; }
    }
}
