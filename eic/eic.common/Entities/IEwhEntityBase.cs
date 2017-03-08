using eic.common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ew.common.Entities
{
    public interface IEwhEntityBase
    {
        string XErrorMessage { get; set; }
        Exception XException { get; set; }
        GlobalStatus XStatus { get; set; }

        int EwhCount { get; set; }
    }
}
