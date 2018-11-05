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
    /// Gerencia a acesso a tags no banco de dados.
    /// </summary>
    public class TagController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public TagController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista completa de tags registrados.
        /// </summary>
        /// <returns>Lista de tags</returns>
        [HttpGet]
        [Route("Tag")]
        public IEnumerable<Tag> Get()
        {
            return (from g in _context.Tags
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Tag com base da identificação.
        /// </summary>
        /// <param name="tagId">Identificação da tag</param>
        /// <returns>Tag</returns>
        [HttpGet]
        [Route("Tag/{tagId:int}")]
        public Tag GetById(int tagId)
        {
            return (from g in _context.Tags
                    where g.TagId == tagId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Tags com texto informado no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de tags</returns>
        [HttpGet]
        [Route("Tag/name/{name}")]
        public IEnumerable<Tag> GetByName(string name)
        {
            return (from g in _context.Tags
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera tag.
        /// </summary>
        /// <param name="tag">Dados da tag</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Tag")]
        public IHttpActionResult PostTag([FromBody] Tag tag)
        {
            try
            {
                _context.Tags.AddOrUpdate(tag);
                _context.SaveChanges();

                return Created("Ok", tag);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa tag identificada pelo parâmetro.
        /// </summary>
        /// <param name="tagId">Identificação da tag</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Tag/{tagId:int}")]
        public IHttpActionResult DeleteTag(int tagId)
        {
            try
            {
                var tag = new Tag { TagId = tagId, Deleted = true };
                _context.Tags.Attach(tag);
                _context.Entry(tag).Property(x => x.Deleted).IsModified = true;

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
