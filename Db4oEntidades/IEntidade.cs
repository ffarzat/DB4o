using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Db4oEntidades
{
    public class IEntidade<T>
    {
        T Id { get; set; }
    }
}
