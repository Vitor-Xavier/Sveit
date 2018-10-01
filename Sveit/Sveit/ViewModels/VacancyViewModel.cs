using Sveit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Sveit.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Sveit.ViewModels
{
    public class VacancyViewModel
    {
        private readonly INavigation _navigation;

        public Vacancy Vacancy { get; set; }

        public ICommand ApplyCommand => new Command(ExecuteApplyCommand);

        public ObservableCollection<Player> Members { get; set; }

        public VacancyViewModel(INavigation navigation)
        {
            _navigation = navigation;
            LoadDataOff();
        }

        public async void ExecuteApplyCommand()
        {
            await _navigation.PushModalAsync(new VacancyRegisterPage());
        }

        private void LoadDataOff()
        {
            var gamePlatform = new GamePlatform
            {
                Game = new Game { Name = "Overwatch", ImageSource = "http://fullhdpictures.com/wp-content/uploads/2016/03/Magnificent-Overwatch-Wallpaper.png", IconSource = "http://esportsobserver.com/wp-content/uploads/2015/09/Tt-eSPORTS-logo.png" },
                Platform = new Platform { Name = "PC" }
            };

            Members = new ObservableCollection<Player>
            {
                new Player { Name = "Alguém", Nickname = "Fate", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender() { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "envy", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender() { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "Verbo", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "KariV", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "GrimReality", AvatarSource = "https://i.pinimg.com/736x/4a/3f/12/4a3f129ec33ab700ee2f7a022e545cdc--avatar.jpg", Gender = new Gender { Name = "Masculino" } },
                new Player { Name = "Alguém", Nickname = "Agilities", AvatarSource = "https://i.pinimg.com/236x/c9/0b/43/c90b43c70af2c51d92814a0b08aff527--the-phantom-avatar.jpg", Gender = new Gender { Name = "Masculino" } }
            };

            Vacancy = new Vacancy
            {
                Title = "Vaga suporte",
                Team = new Team
                {
                    Name = "Immortals",
                    GamePlatform = gamePlatform,
                    IconSource = "http://liquipedia.net/commons/images/thumb/1/1d/Immortals_org.png/600px-Immortals_org.png",
                    Owner = new Player { Name = "Owner player", Nickname = "Owner_" },
                },
                MinAge = 16,
                MaxAge = 23,
                Description = "Descrição completa da vaga que está sendo oferecida pela equipe e alguns de seus requisitos explicados pelo membro que oferta tal vaga, além de questões comportamentais que estão a procura nos cadidatos e etc.",
                VacancyId = 1,
                Skills = new List<Skill> { new Skill { Name = "tiro" }, new Skill { Name = "healing" }, new Skill { Name = "suporte" }, new Skill { Name = "sniper" }, new Skill { Name = "estratégia" } }
            };
        }
    }
}
