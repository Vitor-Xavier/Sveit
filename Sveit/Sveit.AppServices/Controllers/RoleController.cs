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
    /// Gerencia o acesso aos dados sobre Funções.
    /// </summary>
    public class RoleController : ApiController
    {
        private DatabaseContext _context = new DatabaseContext();

        /// <summary>
        /// Retorna função com base na identificação.
        /// </summary>
        /// <param name="roleId">Identificação da função</param>
        /// <returns>Função</returns>
        [HttpGet]
        [Route("Role/{roleId:int}")]
        public Role GetRole(int roleId)
        {
            return (from r in _context.Roles
                    where r.RoleId == roleId &&
                    r.Deleted == false
                    select r).FirstOrDefault();
        }

        /// <summary>
        /// Retorna lista de funções.
        /// </summary>
        /// <returns>Lista de Funções</returns>
        [HttpGet]
        [Route("Role")]
        public IEnumerable<Role> GetRoles()
        {
            return (from r in _context.Roles
                    where r.Deleted == false
                    select r).AsEnumerable();
        }

        /// <summary>
        /// Retorna lista de funções de determinado tipo.
        /// </summary>
        /// <param name="roleTypeId">Identificação do tipo</param>
        /// <returns>Lista de Funções</returns>
        [HttpGet]
        [Route("Role/Type/{roleTypeId:int}")]
        public IEnumerable<Role> GetByRoleType(int roleTypeId)
        {
            return (from r in _context.Roles
                    where r.RoleTypeId == roleTypeId &&
                    r.Deleted == false
                    select r).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera função.
        /// </summary>
        /// <param name="role">Dados da função</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Role")]
        public IHttpActionResult PostRole([FromBody] Role role)
        {
            try
            {
                _context.Roles.AddOrUpdate(role);
                if (_context.SaveChanges() != 0)
                    return Created("Ok", role);
                else
                    return InternalServerError();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa função registrada.
        /// </summary>
        /// <param name="roleId">Identificação da função</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Role/{roleId:int}")]
        public IHttpActionResult DeleteRole(int roleId)
        {
            try
            {
                var role = AppServices.Utils.ModelsDefault.GetDefaultRole();
                role.RoleId = roleId;
                role.Deleted = true;
                _context.Roles.Attach(role);
                _context.Entry(role).Property(x => x.Deleted).IsModified = true;

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
