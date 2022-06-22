using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTS.IR.Interfaces
{
    public interface ICacheService
    {
        T GetFromCache<T>(string key, Func<T> query);
    }
}
