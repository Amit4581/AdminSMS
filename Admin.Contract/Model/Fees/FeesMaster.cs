using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Contract.Model.Fees
{
    public class FeesMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string COMP_CODE { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ClassId { get; set; }

        public string ClassName { get; set; }


        [Column(TypeName = "varchar(10)")]
        public string Month { get; set; }

        [Column(TypeName = "varchar(10)")]
        public string Year { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }

        public string FeesType { get; set; }

        [Column(TypeName = "nchar(10)")]
        public string GST { get; set; }

        public DateTime EntryDate { get; set; }

        public int? IsActive { get; set; }
    }
}
