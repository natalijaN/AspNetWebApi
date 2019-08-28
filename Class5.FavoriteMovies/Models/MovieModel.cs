using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class MovieModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public GenreType Genre { get; set; }
        public int UserId { get; set; }
    }

    public enum GenreType
    {
        Comedy = 1,
        Drama = 2,
        Romance = 3,
        Horror = 4
    }
}
