using System.Collections.Generic;
using System.Linq;

namespace Demosite.Interfaces.Models
{
    public class ArrayFilter<T>
    {
        public bool Inverted { get; set; }
        public T[] Values { get; set; }

        public ArrayFilter(T[] values, bool inverted = false)
        {
            Inverted = inverted;
            Values = values;
        }

        public static ArrayFilter<T> Create(T[] values, bool inverted = false)
        {
            if(values == null || !values.Any())
            {
                return null;
            }
            return new ArrayFilter<T>(values, inverted);
        }
    }
}
