using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sveit.ViewModels
{
    public class GenreViewModel
    {
        public ObservableCollection<Genre> Genres { get; set; }

        public GenreViewModel()
        {
            Genres = new ObservableCollection<Genre>();
            LoadData();
        }

        private void LoadData()
        {
            Genres.Clear();
            Genres.Add(new Genre() { GenreId = 1, Name = "FPS" });
            Genres.Add(new Genre() { GenreId = 2, Name = "Action" });
            Genres.Add(new Genre() { GenreId = 3, Name = "RPG" });
            Genres.Add(new Genre() { GenreId = 4, Name = "Adventure" });
            Genres.Add(new Genre() { GenreId = 5, Name = "Survival" });
            Genres.Add(new Genre() { GenreId = 6, Name = "Survival Horror" });
            Genres.Add(new Genre() { GenreId = 7, Name = "TPS" });
            Genres.Add(new Genre() { GenreId = 8, Name = "Simulation" });
            Genres.Add(new Genre() { GenreId = 9, Name = "Race" });
        }
    }
}
