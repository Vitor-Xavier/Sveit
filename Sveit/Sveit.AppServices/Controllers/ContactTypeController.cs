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
    /// Manipula os dados de tipos de contato.
    /// </summary>
    public class ContactTypeController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public ContactTypeController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Lista completa de tipos de contato registrados.
        /// </summary>
        /// <returns>Lista de tipos de contato</returns>
        [HttpGet]
        [Route("ContactType")]
        public IEnumerable<ContactType> Get()
        {
            return (from g in _context.ContactTypes
                    where g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Tipo de contato com base da identificação.
        /// </summary>
        /// <param name="contactTypeId">Identificação do tipo de contato</param>
        /// <returns>Tipo de contato</returns>
        [HttpGet]
        [Route("ContactType/{contactTypeId:int}")]
        public ContactType GetById(int contactTypeId)
        {
            return (from g in _context.ContactTypes
                    where g.ContactTypeId == contactTypeId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Tipos de contato com texto informado no nome.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de tipos de contato</returns>
        [HttpGet]
        [Route("ContactType/name/{name}")]
        public IEnumerable<ContactType> GetByName(string name)
        {
            return (from g in _context.ContactTypes
                    where g.Name.ToLower().Contains(name.ToLower()) &&
                    g.Deleted == false
                    select g).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera tipo de contato.
        /// </summary>
        /// <param name="contactType">Dados do tipo de contato</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("ContactType")]
        public IHttpActionResult PostContactType([FromBody] ContactType contactType)
        {
            try
            {
                _context.ContactTypes.AddOrUpdate(contactType);
                _context.SaveChanges();

                return Created("Ok", contactType);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa tipo de contato identificado pelo parâmetro.
        /// </summary>
        /// <param name="contactTypeId">Identificação do tipo de contato</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("ContactType/{contactTypeId:int}")]
        public IHttpActionResult DeleteContactType(int contactTypeId)
        {
            try
            {
                var contactType = new ContactType { ContactTypeId = contactTypeId, Deleted = true };
                _context.ContactTypes.Attach(contactType);
                _context.Entry(contactType).Property(x => x.Deleted).IsModified = true;

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
