using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eic.core.Dtos
{

    public class AddWebsiteAccountModel
    {
        public Account Account { get; set; }
        public List<string> AccessLevels { get; set; }
        public string WebsiteDisplayName { get; set; }
    }

    public class RemoveWebsiteAccountModel
    {
        public Account Account { get; set; }
    }

    public class AccountQueryParams : EntityQueryParams
    {
        
    }
}
