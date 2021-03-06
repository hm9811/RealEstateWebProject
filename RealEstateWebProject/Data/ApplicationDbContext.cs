using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstateWebProject.Models;

namespace RealEstateWebProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<RealEstateWebProject.Models.AgentModel> AgentModel { get; set; }
        public DbSet<RealEstateWebProject.Models.PropertyModel> PropertyModel { get; set; }
        public DbSet<RealEstateWebProject.Models.RentPropertyModel> RentPropertyModel { get; set; }
        public DbSet<RealEstateWebProject.Models.CommentModel> CommentModel { get; set; }
        public DbSet<RealEstateWebProject.Models.BidModel> BidModel { get; set; }
        public DbSet<RealEstateWebProject.Models.AuctionModel> AuctionModel { get; set; }
        public DbSet<RealEstateWebProject.Models.SellPropertyModel> SellPropertyModel { get; set; }
    }
}
