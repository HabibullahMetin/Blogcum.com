﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    public partial class Blogcum4Entities : DbContext
    {
        public Blogcum4Entities()
            : base("name=Blogcum4Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Blogcular> Blogcular { get; set; }
        public virtual DbSet<Bloglar> Bloglar { get; set; }
        public virtual DbSet<Iletisim> Iletisim { get; set; }
        public virtual DbSet<Yorumlar> Yorumlar { get; set; }
    }
}