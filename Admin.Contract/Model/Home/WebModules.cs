using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Home
{
    public class WebModules
    {
        public int ID { get; set; }
        public string nameOption { get; set; }
        public string Area { get; set; }
        public string controller { get; set; }
        public string action { get; set; }
        public int parentId { get; set; }
        public bool Active { get; set; }
        public bool isParent { get; set; }
        public bool status { get; set; }
        public string imageClass { get; set; }
        public string Param1 { get; set; }
        public int subparentId { get; set; }
        public bool isSubparentId { get; set; }
        public int ORDERNO { get; set; }
        public int SuperParentId { get; set; }
        public bool IsSuperParent { get; set; }
    }
}
