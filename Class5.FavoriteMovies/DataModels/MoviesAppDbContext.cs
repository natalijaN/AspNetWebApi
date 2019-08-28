using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace DataModels
{
    public class MoviesAppDbContext : DbContext
    {
        public MoviesAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<UserDto> Users { get; set; }
        public DbSet<MovieDto> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MovieDto>()
                .HasOne(x => x.User)
                .WithMany(x => x.MovieList)
                .HasForeignKey(x => x.UserId);

            var md5 = new MD5CryptoServiceProvider();
            var md5data = md5.ComputeHash(Encoding.ASCII.GetBytes("john123456"));
            var hashedPassword = Encoding.ASCII.GetString(md5data);

            modelBuilder.Entity<UserDto>()
               .HasData(
               new UserDto()
               {
                   Id = 1,
                   FirstName = "John",
                   LastName = "Doe",
                   Username = "johnDoe",
                   Password = hashedPassword
               });
            modelBuilder.Entity<MovieDto>()
               .HasData(
               new MovieDto()
               {
                   Id = 1,
                   Title = "The Godfather",
                   Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                   Year = 1972,
                   Genre = 1,
                   UserId = 1
               },
               new MovieDto()
               {
                   Id = 2,
                   Title = "Life Is Beautiful",
                   Description = "When an open-minded Jewish librarian and his son become victims of the Holocaust, he uses a perfect mixture of will, humor, and imagination to protect his son from the dangers around their camp.",
                   Year = 1997,
                   Genre = 2,
                   UserId = 1
               }
              );
        }
    }
}
