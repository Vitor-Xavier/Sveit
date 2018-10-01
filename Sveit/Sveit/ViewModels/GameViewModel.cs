using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Sveit.Models;

namespace Sveit.ViewModels
{
    public class GameViewModel
    {
        public Game Game { get; set; }

        public ObservableCollection<Platform> Platforms { get; set; }

        public ObservableCollection<Vacancy> Vacancies { get; set; }

        public string PlatformsStr
        {
            get
            {
                return Platforms.Count != 0 ? string.Join(", ", Platforms) : "Indisponível";
            }
        }

        public GameViewModel()
        {
            Platforms = new ObservableCollection<Platform>();
            LoadData();
        }

        private void LoadData()
        {
            Genre genreFPS = new Genre() { GenreId = 1, Name = "FPS" };
            Game = new Game
            {
                GameId = 2,
                Name = "Overwatch",
                Description = "FPS, objetivo, 5 membros",
                Genres = new List<Genre> { genreFPS },
                IconSource = "https://i.imgur.com/0RIw2RB.png",
                BackgroundSource = "https://s3.amazonaws.com/comparegame/background_images/41965/original.jpg" //"http://www.base2.com.br/wp-content/uploads/2017/04/overwatch1280jpg-6daa73_1280w.jpg"
            };

            Platforms.Clear();
            Platforms.Add(new Platform
            {
                Name = "PC"
            });
            Platforms.Add(new Platform
            {
                Name = "Xbox One"
            });
            Platforms.Add(new Platform
            {
                Name = "Playstation 4"
            });

            var gamePlatform = new GamePlatform
            {
                Game = new Game { Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                Platform = new Platform { Name = "PC" }
            };

            Vacancies = new ObservableCollection<Vacancy>()
            {
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports2", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports3", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports4", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports5", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports6", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                },
                new Vacancy
                {
                    Title = "Vaga suporte",
                    Skills = new List<Skill> { new Skill { Name = "skill1" }, new Skill { Name = "skill1" }, new Skill { Name = "skill1" } },
                    Team = new Team { Name = "Commander eSports7", GamePlatform = gamePlatform, IconSource = "https://static-cdn.jtvnw.net/jtv_user_pictures/flawedbot-profile_image-815076ff68773420-300x300.jpeg" },
                    VacancyId = 1,
                    Description = "vaga...."
                }
            };
        }
    }
}
