using eic.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application.Dtos
{
    public class CreateActionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AreaName { get; set; }
        public string CtrlName { get; set; }
        public string ActName { get; set; }

        public List<QueryParam> QueryParams { get; set; }
    }
}
