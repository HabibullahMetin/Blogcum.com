using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogcum.Models
{
    [Table ("BlogYaz")]
    public class BlogYaz 
    {

        public string BlogYazisi { get; set; }
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int  BlogYazisiID { get; set; }
        public int  BlogcuID { get; set; }
        public int  YorumID { get; set; }
        [StringLength(15,ErrorMessage ="Kategori en fazla 15 harften oluşmalı"),Required]
        public string  Kategori { get; set; }
        public virtual ICollection<Hakkimizda> Hakkimizda { get; set; }
        public virtual Blogcular Blogcular { get; set; }

       

    }
}