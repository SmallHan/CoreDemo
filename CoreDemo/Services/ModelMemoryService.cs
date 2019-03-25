using CoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Services
{
    public class ModelMemoryService: IMovieService
    {
        private readonly List<Movie> _movies = new List<Movie>();
        public ModelMemoryService()
        {
            _movies.Add(new Movie
            {
                CinemaId=1,
                Id=1,
                Name="Superman",
                ReleaseDate=new DateTime(2019,3,24),
                Starring="Nick"
            });
            _movies.Add(new Movie
            {
                CinemaId = 1,
                Id = 2,
                Name = "Ghost",
                ReleaseDate = new DateTime(2019, 3, 24),
                Starring = "Micheal Jackson"
            });
        }

        public Task AddAsync(Movie model)
        {
            var maxId = _movies.Max(x => x.Id);
            model.Id = maxId+1;
            _movies.Add(model);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Movie>> GetByCinemaAsync(int cinemaId)
        {
            return Task.Run(() => _movies.Where(p=>p.CinemaId==cinemaId));
        }
    }
}
