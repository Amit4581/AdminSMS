using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Master
{
    public class FormMasterModel
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public int CHARACTER_MAXIMUM_LENGTH { get; set; }
       public string IS_NULLABLE { get; set; }
        public string IsIdentity { get; set; }
    }
}
