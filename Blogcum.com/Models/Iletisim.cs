//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Blogcum.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Iletisim
    {
        public int IletiID { get; set; }
        public string Ileti { get; set; }
        public string Konu { get; set; }
        public int BlogcuID { get; set; }
    
        public virtual Blogcular Blogcular { get; set; }
    }
}