using eic.common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ew.common.Entities
{
    public abstract class EwhEntityBase : IEwhEntityBase
    {
        public Exception XException { get; set; }
        public string XErrorMessage { get; set; }
        public GlobalStatus XStatus { get; set; }
        public ICollection<ValidationResult> ValidateResults = null;

        public int EwhCount { get; set; }


        protected void SyncStatus(EwhEntityBase d, EwhEntityBase s)
        {
            d.XStatus = s.XStatus;
            d.XErrorMessage = s.XErrorMessage;
            d.XException = s.XException;
            d.ValidateResults = s.ValidateResults;

        }
    }
}
