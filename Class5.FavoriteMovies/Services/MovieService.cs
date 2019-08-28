using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataModels;
using Models;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly IRepository<MovieDto> _movieRepository;

        public MovieService(IRepository<UserDto> userRepository,
           IRepository<MovieDto> movieRepository)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
        }

        public void AddMovie(MovieModel model)
        {
            var user = _userRepository.GetAll().FirstOrDefault(u => u.Id == model.UserId);

            var movie = new MovieDto()
            {
                Title = model.Title,
                Description = model.Description,
                Year = model.Year,
                Genre = (int)model.Genre,
                UserId = model.UserId
            };
            _movieRepository.Add(movie);
        }

        public void DeleteMovie(int id, int userId)
        {
            var movie = _movieRepository.GetAll()
                .FirstOrDefault(x => x.Id == id && x.UserId == userId);
            _movieRepository.Delete(movie);
        }

        public MovieModel GetMovie(int id, int userId)
        {
            var movie = _movieRepository.GetAll().FirstOrDefault(m => m.Id == id && m.UserId == userId);

            var movieModel = new MovieModel()
            {
                 Id = movie.Id,
                 Title = movie.Title,
                 Description = movie.Description,
                 Year = movie.Year,
                 Genre = (GenreType)movie.Genre,
                 UserId = movie.UserId
            };

            return movieModel;
        }

        public MovieModel GetMovieByGenre(int genre, int userId)
        {
            var movie = _movieRepository.GetAll().FirstOrDefault(m => m.Genre == genre && m.UserId == userId);

            var movieModel = new MovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                Genre = (GenreType)movie.Genre,
                UserId = movie.UserId
            };

            return movieModel;
        }

        public IEnumerable<MovieModel> GetUserMovies(int userId)
        {
            return _movieRepository.GetAll()
               .Where(x => x.UserId == userId).Select(x =>
               new MovieModel()
               {
                   Id = x.Id,
                   Title = x.Title,
                   Description = x.Description,
                   Year = x.Year,
                   Genre = (GenreType)x.Genre,
                   UserId = x.UserId
               });
        }
    }
}
