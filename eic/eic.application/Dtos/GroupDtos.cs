using eic.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eic.application.Dtos
{
    public class CreateGroupDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Group ParentGroup { get; set; }
    }
}
