using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FavoriteMovies.WebApi.Domain
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreType Genre { get; set; }
    }

    public enum GenreType
    {
        Comedy = 1,
        Drama = 2,
        Romance = 3,
        Horror = 4
    }
}
