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
    /// Manipula os estilos de jogos no banco de dados.
    /// </summary>
    public class GenreController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public GenreController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista completa de estilos de jogo registrados.
        /// </summary>
        /// <returns>Lista de estilos de jogo</returns>
        [HttpGet]
        [Route("Genre")]
        public IEnumerable<Genre> Get()
        {
            return (from g in _context.Genres
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Estilo de jogo com base da identificação.
        /// </summary>
        /// <param name="genreId">Identificação do estilo de jogo</param>
        /// <returns>Estilo de jogo</returns>
        [HttpGet]
        [Route("Genre/{genreId:int}")]
        public Genre GetById(int genreId)
        {
            return (from g in _context.Genres
                    where g.GenreId == genreId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Estilos de jogo com texto informado no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de estilos de jogo</returns>
        [HttpGet]
        [Route("Genre/name/{name}")]
        public IEnumerable<Genre> GetByName(string name)
        {
            return (from g in _context.Genres
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera estilo de jogo.
        /// </summary>
        /// <param name="genre">Dados do estilo de jogo</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Genre")]
        public IHttpActionResult PostGenre([FromBody] Genre genre)
        {
            try
            {
                _context.Genres.AddOrUpdate(genre);
                _context.SaveChanges();

                return Created("Ok", genre);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa estilo de jogo identificado pelo parâmetro.
        /// </summary>
        /// <param name="genreId">Identificação do estilo de jogo</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Genre/{genreId:int}")]
        public IHttpActionResult DeleteGenre(int genreId)
        {
            try
            {
                var genre = AppServices.Utils.ModelsDefault.GetDefaultGenre();
                genre.GenreId = genreId;
                genre.Deleted = true;
                _context.Genres.Attach(genre);
                _context.Entry(genre).Property(x => x.Deleted).IsModified = true;

                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
