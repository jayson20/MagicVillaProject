using MagicVilla_VillaAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_VillaAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Villa> Villas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                    ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                    Occupancy = 1,
                    Rate = 200,
                    Sqrft = 200,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                 new Villa()
                 {
                     Id = 2,
                     Name = "Pool Villa",
                     Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                     ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                     Occupancy = 3,
                     Rate = 800,
                     Sqrft = 700,
                     Amenity = "",
                     CreatedDate = DateTime.Now
                 },
                  new Villa()
                  {
                      Id = 3,
                      Name = "Kings Villa",
                      Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s",
                      ImageUrl = "https://media.istockphoto.com/id/1929345158/photo/modern-apartment-with-large-windows.webp?s=2048x2048&w=is&k=20&c=BVoaNU0lklmbqhVmwt7Ospi8ecdVkap-NmrvCNuRUyY=",
                      Occupancy = 5,
                      Rate = 1200,
                      Sqrft = 1000,
                      Amenity = "",
                      CreatedDate = DateTime.Now
                  }
                );
        }
    }
}
