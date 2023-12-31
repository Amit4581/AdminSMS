
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Master
{
    public class TypeMaster
    {
        //Id, Type_Code, Type_Name, Type_For,
        public int Id { get; set; }
        public string Type_Code { get; set; }
        public string Type_Name { get; set; }
        public string Type_For { get; set; } = string.Empty;

    }
}
