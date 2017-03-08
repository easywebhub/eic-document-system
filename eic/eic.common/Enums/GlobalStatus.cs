using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.common.Enums
{
    public enum GlobalStatus
    {
        Success,
        UnSuccess,
        Failed,
        Invalid,
        InvalidData,
        Maximum_Limited, // bị giới hạn
        Access_Denied,
        NotFound,
        AlreadyExists,

    }
}
