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
    /// Controla as plataformas no banco de dados.
    /// </summary>
    public class PlatformController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public PlatformController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista completa de plataformas registrados.
        /// </summary>
        /// <returns>Lista de plataformas</returns>
        [HttpGet]
        [Route("Platform")]
        public IEnumerable<Platform> Get()
        {
            return (from g in _context.Platforms
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Plataforma com base da identificação.
        /// </summary>
        /// <param name="platformId">Identificação da plataforma</param>
        /// <returns>Plataforma</returns>
        [HttpGet]
        [Route("Platform/{platformId:int}")]
        public Platform GetById(int platformId)
        {
            return (from g in _context.Platforms
                    where g.PlatformId == platformId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Plataformas com texto informado no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de plataformas</returns>
        [HttpGet]
        [Route("Platform/Name/{name}")]
        public IEnumerable<Platform> GetByName(string name)
        {
            return (from g in _context.Platforms
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Plataformas disponíveis do jogo informado.
        /// </summary>
        /// <param name="gameId">Identificação do jogo.</param>
        /// <returns>Lista de plataformas</returns>
        [HttpGet]
        [Route("Platform/Game/{gameId:int}")]
        public IEnumerable<Platform> GetByGame(int gameId)
        {
            return (from gp in _context.GamePlatforms
                    where gp.GameId == gameId &&
                    gp.Deleted == false
                    select gp.Platform).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera plataforma.
        /// </summary>
        /// <param name="platform">Dados da plataforma</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Platform")]
        public IHttpActionResult PostPlatform([FromBody] Platform platform)
        {
            try
            {
                _context.Platforms.AddOrUpdate(platform);
                _context.SaveChanges();

                return Created("Ok", platform);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa plataforma identificado pelo parâmetro.
        /// </summary>
        /// <param name="platformId">Identificação da plataforma</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Platform/{platformId:int}")]
        public IHttpActionResult DeletePlatform(int platformId)
        {
            try
            {
                var platform = AppServices.Utils.ModelsDefault.GetDefaultPlatform();
                platform.PlatformId = platformId;
                platform.Deleted = true;
                _context.Platforms.Attach(platform);
                _context.Entry(platform).Property(x => x.Deleted).IsModified = true;

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
