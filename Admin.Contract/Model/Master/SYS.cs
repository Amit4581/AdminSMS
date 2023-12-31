using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Master
{
    public class SYS
    {

        public string COMP_CODE { get; set; }
        public string COMP_NAME { get; set; }
        public string Short_Name { get; set; }
        public string DATABASENAME { get; set; }
        public string USERID { get; set; }
        public string USERPASSWORD { get; set; }
        public string DSN { get; set; }
        public string CONNECTSTRING { get; set; }
        public string ISACTIVE { get; set; }
        public string Action { get; set; }
    }

}
