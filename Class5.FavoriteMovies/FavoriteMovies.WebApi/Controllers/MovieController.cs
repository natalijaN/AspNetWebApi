using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FavoriteMovies.WebApi.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FavoriteMovies.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public static List<Movie> movies = new List<Movie>()
        {
            new Movie(){
                Id = 1,
                Title = "The Godfather",
                Description = "The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.",
                Year = 1972,
                Genre = GenreType.Drama
            },
            new Movie()
            {
                Id = 2,
                Title = "Life Is Beautiful",
                Description = "When an open-minded Jewish librarian and his son become victims of the Holocaust, he uses a perfect mixture of will, humor, and imagination to protect his son from the dangers around their camp.",
                Year = 1997,
                Genre = GenreType.Drama
            },
            new Movie()
            {
                Id = 3,
                Title = "The Intouchables",
                Description = "After he becomes a quadriplegic from a paragliding accident, an aristocrat hires a young man from the projects to be his caregiver.",
                Year = 2011,
                Genre = GenreType.Comedy
            }
        };

        [HttpGet]
        public ActionResult<List<Movie>> Get()
        {
            return movies;
        }

        [HttpGet("{id}")]
        public ActionResult<Movie> GetById(int id)
        {
            try
            {
                return movies[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound("No movie with that id!");
            }
            catch(Exception ex)
            {
                return BadRequest($"Broken request: {ex.Message}");
            }
        }
        [HttpGet("genre/{genreNumber}")]
        public ActionResult<Movie> GetMovieByGenre(int genreNumber)
        {
            try
            {
                var movie = movies.Find(m => (int)m.Genre == genreNumber);
                return movie;
            }
            catch (ArgumentNullException)
            {
                return NotFound("No movie with that genre!");
            }
            catch(Exception ex)
            {
                return BadRequest($"Broken requset: {ex.Message}");
            }
        }
        [HttpPost("body")]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            movies.Add(movie);
            return Ok("A movie has been created!");
        }
    }
}