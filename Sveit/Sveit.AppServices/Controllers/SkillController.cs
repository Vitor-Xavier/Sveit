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
    /// Gerencia dados sobre habilidades da base de dados.
    /// </summary>
    public class SkillController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public SkillController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna habilidades na base de dados.
        /// </summary>
        /// <returns>Lista de Habilidades</returns>
        [HttpGet]
        [Route("Skill")]
        public IEnumerable<Skill> Get()
        {
            return (from s in _context.Skills
                    where s.Deleted == false
                    select s).AsEnumerable();
        }

        /// <summary>
        /// Retorna dados de habilidade em específico.
        /// </summary>
        /// <param name="skillId">Identificação da habilidade</param>
        /// <returns>Habilidade</returns>
        [HttpGet]
        [Route("Skill/{skillId:int}")]
        public Skill GetById(int skillId)
        {
            return (from s in _context.Skills
                    where s.SkillId == skillId &&
                    s.Deleted == false
                    select s).SingleOrDefault();
        }

        /// <summary>
        /// Pesquisa habilidades com base no texto.
        /// </summary>
        /// <param name="name">Texto para busca no nome</param>
        /// <returns>Lista de Habilidades</returns>
        [HttpGet]
        [Route("Skill/{name}")]
        public IEnumerable<Skill> GetByName(string name)
        {
            return (from s in _context.Skills
                    where s.Name.ToLower().Contains(name.ToLower()) &&
                    s.Deleted == false
                    select s).AsEnumerable();
        }

        /// <summary>
        /// Adiciona habilidade aos registros.
        /// </summary>
        /// <param name="skill">Dados da habilidade</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Skill")]
        public IHttpActionResult PostSkill([FromBody] Skill skill)
        {
            try
            {
                _context.Skills.AddOrUpdate(skill);
                _context.SaveChanges();

                return Created("Ok", skill);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa habilidade.
        /// </summary>
        /// <param name="skillId">Identificação da habilidade</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Skill/{skillId:int}")]
        public IHttpActionResult DeleteSkill(int skillId)
        {
            try
            {
                var skill = AppServices.Utils.ModelsDefault.GetDefaultSkill();
                skill.SkillId = skillId;
                skill.Deleted = true;
                _context.Skills.Attach(skill);
                _context.Entry(skill).Property(x => x.Deleted).IsModified = true;

                _context.SaveChanges();
                return Created("Ok", skill);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }
    }
}
