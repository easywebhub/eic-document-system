using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.core
{
    public interface IPagination
    {
        int TotalItems { get; set; }
        int Limit { get; set; }
        int Offset { get; set; }
    }
}
