using CoreDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Services
{
    public class CinemaMemoryService: ICinemaService
    {
        private readonly List<Cinema> _cinemas = new List<Cinema>();
        public CinemaMemoryService()
        {
            _cinemas.Add(new Cinema
            {
                Id=1,
                Name="City Cinema",
                Location="Road ABC,No.123",
                Capacity=1000
            });
            _cinemas.Add(new Cinema
            {
                Id = 2,
                Name = "FlyCinema",
                Location = "Road Hello,No.1024",
                Capacity = 500
            });
        }

        public Task AddAsync(Cinema model)
        {
            var maxId = _cinemas.Max(x => x.Id);
            model.Id = maxId + 1;
            _cinemas.Add(model);
            return Task.CompletedTask;
        }

        public Task<Cinema> GetByIdAsync(int id)
        {
            return Task.Run(() => _cinemas.FirstOrDefault(x=>x.Id==id)); 
        }

        public Task<IEnumerable<Cinema>> GetllAllAsync()
        {
            return Task.Run(() => _cinemas.AsEnumerable());
        }

    }
}
