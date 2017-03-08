using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.core
{
    public class EntityQueryParams : IPagination
    {
        public int Limit { get; set; }

        public int Offset { get; set; }

        public int TotalItems { get; set; }

        public EntityQueryParams()
        {
            Limit = 20;
            Offset = 0;
            TotalItems = 0;
        }
    }
}
