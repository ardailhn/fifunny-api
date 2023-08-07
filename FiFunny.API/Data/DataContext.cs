using FiFunny.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiFunny.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            
        }

        public DbSet<Value> Values { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Place> Places { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Province> iller { get; set; }
        public DbSet<District> ilceler { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<DefaultUser> DefaultUser { get; set; }
        public DbSet<Cinsiyet> Cinsiyet { get; set; }
        public DbSet<about> about { get; set; }
        public DbSet<MainPlace> MainPlace { get; set; }
        public DbSet<Comment> Comments { get; set; }

        //filter
        public DbSet<Filter> Filters { get; set; }

    }
}
