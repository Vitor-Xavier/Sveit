using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Sveit.API.Context;
using Sveit.Models;

namespace Sveit.API.Controllers
{
    /// <summary>
    /// Gerencia o acesso aos dados de aplicações.
    /// </summary>
    public class AppliesController : ApiController
    {
        private DatabaseContext _context = new DatabaseContext();
        
        /// <summary>
        /// Retorna aplicação com base na identificação fornecida.
        /// </summary>
        /// <param name="applyId">Identificação da aplicação</param>
        /// <returns>Aplicação</returns>
        [HttpGet]
        [Route("Apply/{applyId:int}")]
        public Apply GetApply(int applyId)
        {
            return (from a in _context.Applies
                    where a.ApplyId == applyId &&
                    a.Deleted == false
                    select a).FirstOrDefault();
        }

        /// <summary>
        /// Busca aplicações realizadas por determinado jogador.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Lista de aplicações</returns>
        [HttpGet]
        [Route("Apply")]
        public IEnumerable<Apply> GetByPlayer(int playerId)
        {
            return (from a in _context.Applies
                    where a.PlayerId == playerId &&
                    a.Deleted == false
                    select a).AsEnumerable();
        }

        /// <summary>
        /// Retorna as aplicações a determinada vaga.
        /// </summary>
        /// <param name="vacancyId">Identificação da vaga</param>
        /// <returns>Lista de Aplicações</returns>
        [HttpGet]
        [Route("Apply/Vacancy/{vacancyId:int}")]
        public IEnumerable<Apply> GetAppliesByVacancy(int vacancyId)
        {
            return (from a in _context.Applies
                    where a.VacancyId == vacancyId &&
                    a.Deleted == false
                    select a).AsEnumerable();
        }

        /// <summary>
        /// Adiciona ou altera aplicação.
        /// </summary>
        /// <param name="apply">Dados da aplicação</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Apply")]
        public IHttpActionResult PostApply([FromBody] Apply apply)
        {
            try
            {
                _context.Applies.AddOrUpdate(apply);
                _context.SaveChanges();

                return Created("Ok", apply);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa aplicação na base de dados.
        /// </summary>
        /// <param name="applyId">Identificação da aplicação.</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Apply/{applyId:int}")]
        public IHttpActionResult DeleteApply(int applyId)
        {
            try
            {
                var apply = new Apply { ApplyId = applyId, Deleted = true };
                _context.Applies.Attach(apply);
                _context.Entry(apply).Property(x => x.Deleted).IsModified = true;

                return Ok();
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}