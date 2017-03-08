using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eic.core
{
    public class EwDocument
    {
        [Key]
        public string Id { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifyDate { get; set; }

        public EwDocument(string type)
        {
            Type = type;
            CreatedDate = DateTime.Now;
            LastModifyDate = DateTime.Now;
        }
    }
}
