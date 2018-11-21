using Sveit.API.Context;
using Sveit.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace Sveit.AppServices.Controllers
{
    /// <summary>
    /// Manipula os dados de contato.
    /// </summary>
    public class ContactsController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public ContactsController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Contato com base da identificação.
        /// </summary>
        /// <param name="contactId">Identificação do contato</param>
        /// <returns>Tipo de contato</returns>
        [HttpGet]
        [Route("Contact/{contactId:int}")]
        public Contact GetContactById(int contactId)
        {
            return (from g in _context.Contacts
                    where g.ContactId == contactId &&
                    g.Deleted == false
                    select g).SingleOrDefault();
        }

        /// <summary>
        /// Inativa contato identificado pelo parâmetro.
        /// </summary>
        /// <param name="contactId">Identificação do contato</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Contact/{contactId:int}")]
        public IHttpActionResult DeleteContact(int contactId)
        {
            try
            {
                var contact = AppServices.Utils.ModelsDefault.GetDefaultContact();
                contact.ContactId = contactId;
                contact.Deleted = true;
                _context.Contacts.Attach(contact);
                _context.Entry(contact).Property(x => x.Deleted).IsModified = true;

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
