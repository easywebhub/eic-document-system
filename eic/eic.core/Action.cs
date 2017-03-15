using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core
{
    public class Action : EwDocument
    {
        public Action() : base("Action")
        {
            QueryParams = new List<QueryParam>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string AreaName { get; set; }
        public string CtrlName { get; set; }
        public string ActName { get; set; }

        public List<QueryParam> QueryParams { get; set; }
    }

    public class QueryParam
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
