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
    /// Gerencia o acesso a dados de RoleType
    /// </summary>
    public class RoleTypeController : ApiController
    {
        private DatabaseContext _context = new DatabaseContext();

        /// <summary>
        /// Retorna tipo de função correspondente a identificação.
        /// </summary>
        /// <param name="roleTypeId">Identificação do tipo de função</param>
        /// <returns>Tipo de Função</returns>
        [HttpGet]
        [Route("RoleType/{roleTypeId:int}")]
        public RoleType GetRoleType(int roleTypeId)
        {
            return (from rt in _context.RoleTypes
                    where rt.RoleTypeId == roleTypeId &&
                    rt.Deleted == false
                    select rt).FirstOrDefault();
        }

        /// <summary>
        /// Retorna tipos de função para determinada jogo.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Lista de tipos de função</returns>
        [HttpGet]
        [Route("RoleType/Game/{gameId:int}")]
        public IEnumerable<RoleType> GetRoleTypesByGame(int gameId)
        {
            return (from rt in _context.RoleTypes
                    where rt.GameId == gameId &&
                    rt.Deleted == false
                    select rt).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera tipo de função.
        /// </summary>
        /// <param name="roleType">Dados do tipo de função</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("RoleType")]
        public IHttpActionResult PostRoleType([FromBody] RoleType roleType)
        {
            try
            {
                _context.RoleTypes.AddOrUpdate(roleType);
                if (_context.SaveChanges() != 0)
                    return Created("Ok", roleType);
                else
                    return InternalServerError();

            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa tipo de função da base de dados.
        /// </summary>
        /// <param name="roleTypeId">Identificação do tipo de função</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("RoleType/{roleTypeId:int}")]
        public IHttpActionResult DeleteRoleType(int roleTypeId)
        {
            try
            {
                var roleType = new RoleType { RoleTypeId = roleTypeId, Deleted = true };
                _context.RoleTypes.Attach(roleType);
                _context.Entry(roleType).Property(x => x.Deleted).IsModified = true;

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
