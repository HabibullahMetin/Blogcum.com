using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blogcum.Models.Managers
{
    public class DatabaseContext : DbContext
    {
          public DbSet<Blogcular> Blogcular { get; set; }
          public DbSet<BlogYaz> BlogYaz { get; set; }
          public DbSet<Hakkimizda> Hakkimizda { get; set; }
          public DbSet<Iletisim>  Iletisim { get; set; }
          public DbSet<Kayit> Kayit { get; set; }


        public DatabaseContext()
        {
            Database.SetInitializer(new VeritabaniOlusturucu());

        }
    }

    public class VeritabaniOlusturucu : CreateDatabaseIfNotExists<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {

            Blogcular Blogcu = new Blogcular();
            Blogcular YardimciBlogcu = new Blogcular();
            Blogcu.BlogcuAdi = "Habibullah";
            Blogcu.BlogcuSoyadi = "Metin";
            YardimciBlogcu.BlogcuAdi = "Gökhan";
            YardimciBlogcu.BlogcuSoyadi = "Bayram";

            context.Blogcular.Add(Blogcu);

            context.SaveChanges();

            List<Blogcular> tumBlogcular = context.Blogcular.ToList();
        }



    }
}