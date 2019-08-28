using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace FavoriteMovies.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieModel>> Get()
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetUserMovies(userId));
        }

        [HttpGet("{id}")]
        public ActionResult<MovieModel> GetById(int id)
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetMovie(id, userId));
        }

        [HttpGet("genre/{genreNumber}")]
        public ActionResult<MovieModel> GetMovieByGenre(int genreNumber)
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetMovieByGenre(genreNumber, userId));
        }

        [HttpPost("body")]
        public void CreateMovie([FromBody] MovieModel movie)
        {
            movie.UserId = GetAuthorizeduserId();
            _movieService.AddMovie(movie);
        }

        private int GetAuthorizeduserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                throw new Exception("name identifier claim does not exist!");
            }
            return userId;
        }
    }
}