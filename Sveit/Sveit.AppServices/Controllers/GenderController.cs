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
    /// Controla o acesso a gêneros registrados na base de dados.
    /// </summary>
    public class GenderController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public GenderController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista completa de gêneros registrados.
        /// </summary>
        /// <returns>Lista de gêneros</returns>
        [HttpGet]
        [Route("Gender")]
        public IEnumerable<Gender> Get()
        {
            return (from g in _context.Genders
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Gênero com base da identificação.
        /// </summary>
        /// <param name="genderId">Identificação do gênero</param>
        /// <returns>Gênero</returns>
        [HttpGet]
        [Route("Gender/{genderId:int}")]
        public Gender GetGenderById(int genderId)
        {
            return (from g in _context.Genders
                    where g.GenderId == genderId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Gêneros com texto informado no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de gêneros</returns>
        [HttpGet]
        [Route("Gender/Name/{name}")]
        public IEnumerable<Gender> GetGenderByName(string name)
        {
            return (from g in _context.Genders
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera gênero.
        /// </summary>
        /// <param name="gender">Dados do gênero</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Gender")]
        public IHttpActionResult PostGender([FromBody] Gender gender)
        {
            try
            {
                _context.Genders.AddOrUpdate(gender);
                _context.SaveChanges();

                return Created("Ok", gender);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa gênero identificado pelo parâmetro.
        /// </summary>
        /// <param name="genderId">Identificação do gênero</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Gender/{genderId:int}")]
        public IHttpActionResult DeleteGender(int genderId)
        {
            try
            {
                var gender = new Gender { GenderId = genderId, Deleted = true };
                _context.Genders.Attach(gender);
                _context.Entry(gender).Property(x => x.Deleted).IsModified = true;

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
