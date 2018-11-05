using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Sveit.API.Context;
using Sveit.Models;

namespace Sveit.API.Controllers
{
    /// <summary>
    /// Gerencia a manipulação de dados relacionados a jogos na base de dados.
    /// </summary>
    public class GameController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados.
        /// </summary>
        public GameController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna lista de jogos registrados na base de dados.
        /// </summary>
        /// <returns>Lista de Jogos</returns>
        [HttpGet]
        [Route("Game")]
        public IEnumerable<Game> Get()
        {
            return (from g in _context.Games
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Retorna jogo em específico com base no id.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Jogo</returns>
        [HttpGet]
        [Route("Game/{gameId:int}")]
        public Game GetById(int gameId)
        {
            return (from g in _context.Games
                    where g.GameId == gameId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Retorna jogos que contenham o texto no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Jogo</returns>
        [HttpGet]
        [Route("Game/Name/{name:int}")]
        public IEnumerable<Game> GetByName(string name)
        {
            return (from g in _context.Games
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Retorna jogos do gênero informado, e com o texto informado no nome.
        /// </summary>
        /// <param name="genreId">Identificação do gênero</param>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de Jogos</returns>
        [HttpGet]
        [Route("Game/Genre/{genreId:int}/{name}")]
        public IEnumerable<Game> GetByGenre(int genreId, string name)
        {
            return (from g in _context.Games
                    where g.Genres.Any(gr => gr.GenreId == genreId) &&
                    g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Jogos disponíveis em determinada plataforma.
        /// </summary>
        /// <param name="platformId">Identificação da plataforma.</param>
        /// <returns>Lista de jogos</returns>
        [HttpGet]
        [Route("Game/Platform/{platformId:int}")]
        public IEnumerable<Game> GetByPlatform(int platformId)
        {
            return (from gp in _context.GamePlatforms
                    where gp.PlatformId == platformId &&
                    gp.Deleted == false
                    select gp.Game).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou atualiza jogo registrado.
        /// </summary>
        /// <param name="game">Dados do jogo</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Game")]
        public IHttpActionResult PostGame([FromBody] Game game)
        {
            try
            {
                _context.Games.AddOrUpdate(game);
                _context.SaveChanges();
                return Created("Ok", game);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Associa gênero ao jogo.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <param name="genreId">Identificação do gênero</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Game/Genre")]
        public IHttpActionResult PostGameGenre(int gameId, int genreId)
        {
            try
            {
                var game = (from g in _context.Games
                           where g.GameId == gameId &&
                           g.Deleted == false
                           select g).SingleOrDefault();
                var genre = (from g in _context.Genres
                            where g.GenreId == genreId &&
                            g.Deleted == false
                            select g).SingleOrDefault();

                game?.Genres.Add(genre);
                _context.Games.AddOrUpdate(game);
                _context.SaveChanges();

                return Created("Ok", game);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa jogo pela identificação informada.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Game/{gameId:int}")]
        public IHttpActionResult DeleteGame(int gameId)
        {
            try
            {
                var game = new Game { GameId = gameId, Deleted = true };
                _context.Games.Attach(game);
                _context.Entry(game).Property(x => x.Deleted).IsModified = true;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}
