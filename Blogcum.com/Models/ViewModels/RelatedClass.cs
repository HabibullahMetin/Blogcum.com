using Blogcum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blogcum.ViewModels
{
    public class RelatedClass
    {

        public List<Blogcular> Blogcular { get; set; }
        public List<Bloglar> Bloglar { get; set; }
        public List<Iletisim> Iletisim { get; set; }
        public List<Yorumlar> Yorumlar { get; set; }
    }
}