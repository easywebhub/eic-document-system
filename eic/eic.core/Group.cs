using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core
{
    public class Group: EwDocument
    {
        public Group() : base("Group")
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Group ParentGroup { get; set; }
    }
}
