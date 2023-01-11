using System.Collections.Generic;

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
    }
}
