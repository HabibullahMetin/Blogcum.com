using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Blogcum.Models
{
    [Table("Iletisim")]
    public class Kayit : Blogcular
    {
        [StringLength(30, ErrorMessage = "10 karakterden fazla mail  girilemez"), Required]
        public string Email { get; set; }
         [Required]
        public int Sifre { get; set; }

    }
}