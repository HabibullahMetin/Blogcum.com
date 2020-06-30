using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogcum.Models
{
    [Table ("Hakkimizda")]
    public class Hakkimizda
    {
        [StringLength(150,ErrorMessage ="150 karakterden fazla yorum yapilamaz"),Required]
        public string Yorum { get; set; }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YorumID { get; set; }
        public int BlogYazisiID { get; set; }
        public int BlogcuID { get; set; }
        public virtual Blogcular Blogcular { get; set; }
        public virtual BlogYaz BlogYaz { get; set; }
    }
}