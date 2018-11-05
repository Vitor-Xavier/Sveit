using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Sveit.Models;

namespace Sveit.API.Context
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        /// <summary>
        /// Insere dados iniciais na base de dados.
        /// </summary>
        /// <param name="context">Contexto do banco de dados</param>
        protected override void Seed(DatabaseContext context)
        {
            context.Players.Add(
            new Player
            {
                PlayerId = 1,
                Email = "admin",
                Password = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918",
                AvatarSource = "https://i.pinimg.com/originals/c8/0a/09/c80a0933df51f9f5be92d033c6db65b2.jpg",
                Nickname = "Vitorxs",
                Name = "Vitor Xavier de Souza",
                DateOfBirth = new System.DateTime(1997, 01, 06)
            });
            

            context.ContactTypes.Add(new ContactType { ContactTypeId = 1, Name = "Email" });
            context.ContactTypes.Add(new ContactType { ContactTypeId = 2, Name = "Telefone" });
            context.ContactTypes.Add(new ContactType { ContactTypeId = 3, Name = "Discord" });
            context.ContactTypes.Add(new ContactType { ContactTypeId = 4, Name = "Teamspeak" });
            context.ContactTypes.Add(new ContactType { ContactTypeId = 5, Name = "Raidcall" });
            context.ContactTypes.Add(new ContactType { ContactTypeId = 6, Name = "Skype" });

            context.Genres.Add(new Genre { GenreId = 01, Name = "FPS" });
            context.Genres.Add(new Genre { GenreId = 02, Name = "TPS" });
            context.Genres.Add(new Genre { GenreId = 03, Name = "Stealth" });
            context.Genres.Add(new Genre { GenreId = 04, Name = "Horror" });
            context.Genres.Add(new Genre { GenreId = 05, Name = "Battle Royale" });
            context.Genres.Add(new Genre { GenreId = 06, Name = "Run and Gun" });
            context.Genres.Add(new Genre { GenreId = 07, Name = "Mundo aberto" });
            context.Genres.Add(new Genre { GenreId = 08, Name = "Estratégia" });
            context.Genres.Add(new Genre { GenreId = 09, Name = "RPG de ação" });
            context.Genres.Add(new Genre { GenreId = 10, Name = "RPG tático" });
            context.Genres.Add(new Genre { GenreId = 11, Name = "MOBA" });
            context.Genres.Add(new Genre { GenreId = 12, Name = "Esporte" });
            context.Genres.Add(new Genre { GenreId = 13, Name = "Luta" });
            context.Genres.Add(new Genre { GenreId = 14, Name = "Corrida" });
            context.Genres.Add(new Genre { GenreId = 15, Name = "MMORPG" });

            context.Platforms.Add(new Platform { PlatformId = 1, Name = "PC - Windows" });
            context.Platforms.Add(new Platform { PlatformId = 2, Name = "XBOX ONE" });
            context.Platforms.Add(new Platform { PlatformId = 3, Name = "Playstation 4" });
            context.Platforms.Add(new Platform { PlatformId = 4, Name = "Switch" });
            context.Platforms.Add(new Platform { PlatformId = 5, Name = "XBOX 360" });
            context.Platforms.Add(new Platform { PlatformId = 6, Name = "Playstation 3" });
            context.Platforms.Add(new Platform { PlatformId = 7, Name = "Linux" });
            context.Platforms.Add(new Platform { PlatformId = 8, Name = "Mac OS" });
            context.Platforms.Add(new Platform { PlatformId = 9, Name = "Android" });
            context.Platforms.Add(new Platform { PlatformId = 10, Name = "iOS" });

            context.Genders.Add(new Gender { GenderId = 1, Name = "Masculino" });
            context.Genders.Add(new Gender { GenderId = 2, Name = "Feminino" });
            context.Genders.Add(new Gender { GenderId = 3, Name = "Outros" });

            context.Tags.Add(new Tag { TagId = 01, Name = "campeonato" });
            context.Tags.Add(new Tag { TagId = 02, Name = "torneio" });
            context.Tags.Add(new Tag { TagId = 03, Name = "esl" });
            context.Tags.Add(new Tag { TagId = 04, Name = "overwatch league" });
            context.Tags.Add(new Tag { TagId = 05, Name = "invitational" });
            context.Tags.Add(new Tag { TagId = 06, Name = "amador" });
            context.Tags.Add(new Tag { TagId = 07, Name = "liga" });

            context.Skills.Add(new Skill { Name = "suporte" });
            context.Skills.Add(new Skill { Name = "tanque" });
            context.Skills.Add(new Skill { Name = "defesa" });
            context.Skills.Add(new Skill { Name = "ataque" });
            context.Skills.Add(new Skill { Name = "rush" });
            context.Skills.Add(new Skill { Name = "pontaria" });
            context.Skills.Add(new Skill { Name = "comunicação" });

            context.SaveChanges();

            context.Games.Add(new Game { GameId = 01, Name = "Counter Strike: Global Offensive",  Genres = new List<Genre> { GetGenre(context, 1) } });
            context.Games.Add(new Game { GameId = 02, Name = "Playerunknow's Battlegorunds", Genres = new List<Genre> { GetGenre(context, 5), GetGenre(context, 1), GetGenre(context, 2) } });
            context.Games.Add(new Game { GameId = 03, Name = "League of Legends", Genres = new List<Genre> { GetGenre(context, 11) } });
            context.Games.Add(new Game { GameId = 04, Name = "DOTA 2", Genres = new List<Genre> { GetGenre(context, 11) } });
            context.Games.Add(new Game { GameId = 05, Name = "Overwatch", Genres = new List<Genre> { GetGenre(context, 1) } });
            context.Games.Add(new Game { GameId = 06, Name = "Battlefield 1", Genres = new List<Genre> { GetGenre(context, 1) } });
            context.Games.Add(new Game { GameId = 07, Name = "Call of Duty: World War 2", Genres = new List<Genre> { GetGenre(context, 1) } });
            context.Games.Add(new Game { GameId = 08, Name = "FIFA 18", Genres = new List<Genre> { GetGenre(context, 12) } });
            context.Games.Add(new Game { GameId = 09, Name = "Mortal Kombat X", Genres = new List<Genre> { GetGenre(context, 13) } });
            context.Games.Add(new Game { GameId = 10, Name = "Injustice 2", Genres = new List<Genre> { GetGenre(context, 13) } });
            context.Games.Add(new Game { GameId = 11, Name = "Forza Motorsport 7", Genres = new List<Genre> { GetGenre(context, 14) } });
            context.Games.Add(new Game { GameId = 12, Name = "Gran Turismo", Genres = new List<Genre> { GetGenre(context, 14) } });
            context.Games.Add(new Game { GameId = 13, Name = "Dead by Daylight", Genres = new List<Genre> { GetGenre(context, 4) } });
            context.Games.Add(new Game { GameId = 14, Name = "Grand Theft Auto V", Genres = new List<Genre> { GetGenre(context, 7), GetGenre(context, 1), GetGenre(context, 2), GetGenre(context, 14), GetGenre(context, 5) } });
            context.Games.Add(new Game { GameId = 15, Name = "Rainbow Six Siege", Genres = new List<Genre> { GetGenre(context, 1) } });
            context.Games.Add(new Game { GameId = 16, Name = "Fortnite", Genres = new List<Genre> { GetGenre(context, 5), GetGenre(context, 1), GetGenre(context, 2) } });

            context.GamePlatforms.Add(new GamePlatform { GameId = 1, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 1, PlatformId = 5 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 1, PlatformId = 6 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 1, PlatformId = 8 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 2, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 2, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 3, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 4, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 5, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 5, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 5, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 6, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 6, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 6, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 7, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 7, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 7, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 8, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 8, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 8, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 9, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 9, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 9, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 9, PlatformId = 9 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 9, PlatformId = 10 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 10, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 10, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 10, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 10, PlatformId = 9 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 10, PlatformId = 10 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 11, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 11, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 12, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 13, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 13, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 13, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 3 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 4 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 5 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 14, PlatformId = 6 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 15, PlatformId = 1 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 15, PlatformId = 2 });
            context.GamePlatforms.Add(new GamePlatform { GameId = 15, PlatformId = 3 });

            context.RoleTypes.Add(new RoleType { RoleTypeId = 1, Name = "Primary Role", GameId = 5 });
            context.RoleTypes.Add(new RoleType { RoleTypeId = 2, Name = "Primary Role", GameId = 13 });
            context.RoleTypes.Add(new RoleType { RoleTypeId = 3, Name = "Primary Role", GameId = 15 });

            context.Roles.Add(new Role { RoleTypeId = 1, Name = "DPS"});
            context.Roles.Add(new Role { RoleTypeId = 1, Name = "Tanque" });
            context.Roles.Add(new Role { RoleTypeId = 1, Name = "Suporte" });

            context.Roles.Add(new Role { RoleTypeId = 2, Name = "Agressivo" });
            context.Roles.Add(new Role { RoleTypeId = 2, Name = "Suporte" });

            context.Roles.Add(new Role { RoleTypeId = 3, Name = "Rush" });
            context.Roles.Add(new Role { RoleTypeId = 3, Name = "Objetivo" });
            
            context.SaveChanges();

            context.Contents.Add(new Content { ContentId = 1, Title = "FORTNITE: BATTLE ROYALE É O GAME GRATUITO DE MAIOR SUCESSO NOS CONSOLES", Description = "Em março, o jogo foi o mais lucrativo nos consoles, e o quinto de maior renda no PC. Durante o mês, Fortnite também teve US$223 milhões de lucro em todas as plataformas (consoles, PC e mobile)...", ContentUrl = "http://br.ign.com/fortnite/61297/news/fortnite-battle-royale-e-o-game-gratuito-de-maior-sucesso-no", Game = GetGame(context, 16), ImageSource = "http://sm.ign.com/ign_br/screenshot/default/screen-shot-2018-04-25-at-52622-pm-1024x606_5jnu.jpg", Source = "IGN Brasil" });
            context.Contents.Add(new Content { ContentId = 2, Title = "Battlefield 5 também pode ter um modo Battle Royale, diz site", Description = "De acordo com o GamesBeat, que conversou com uma fonte anônima de Battlefield 5, a EA Dice está desenvolvendo um protótipo de um modo Battle Royale para o jogo com mecânicas parecidas com Fortnite e PlayerUnknown’s Battlegrounds.", ContentUrl = "https://jovemnerd.com.br/nerdbunker/battlefield-5-tambem-pode-ter-um-modo-battle-royale-diz-site/", ImageSource = "https://jovemnerd.com.br/wp-content/uploads/2018/04/battlefield-5-battle-royale-760x428.png", Source = "Jovem Nerd News" });
        }

        private Genre GetGenre(DatabaseContext context, int genreId)
        {
            return (from g in context.Genres
                    where g.GenreId == genreId
                    select g).SingleOrDefault();
        }

        private Game GetGame(DatabaseContext context, int gameId)
        {
            return (from g in context.Games
                    where g.GameId == gameId
                    select g).SingleOrDefault();
        }
    }
}
