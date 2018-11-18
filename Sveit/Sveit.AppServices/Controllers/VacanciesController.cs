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
    /// Gerencia a manipulação de dados relacionados a vagas.
    /// </summary>
    public class VacanciesController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public VacanciesController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna vaga identificada pelo Id informado.
        /// </summary>
        /// <param name="vacancyId">Identificação da vaga</param>
        /// <returns>Vaga</returns>
        [HttpGet]
        [Route("Vacancy/{vacancyId:int}")]
        public Vacancy GetVacancy(int vacancyId)
        {
            return (from v in _context.Vacancies
                    where v.VacancyId == vacancyId &&
                    v.Deleted == false
                    select v).FirstOrDefault();
        }

        /// <summary>
        /// Retorna vagas de determinada equipe.
        /// </summary>
        /// <param name="teamId">Identificação da equipe</param>
        /// <returns>Lista de Vagas</returns>
        [HttpGet]
        [Route("Vacancy/Team/{teamId:int}")]
        public IEnumerable<Vacancy> GetVacanciesByTeam(int teamId)
        {
            return (from v in _context.Vacancies
                    where v.TeamId == teamId &&
                    v.Deleted == false
                    select v).AsEnumerable();
        }

        /// <summary>
        /// Retorna vagas do jogo informado.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Lista de Vagas</returns>
        [HttpGet]
        [Route("Vacancy/Game/{gameId:int}")]
        public IEnumerable<Vacancy> GetVacanciesByGame(int gameId)
        {
            return (from v in _context.Vacancies
                    where v.Team.GamePlatform_GameId == gameId &&
                    v.Deleted == false
                    select v).AsEnumerable();
        }

        /// <summary>
        /// Registra vaga.
        /// </summary>
        /// <param name="vacancy">Dados da vaga</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Vacancy")]
        public IHttpActionResult PostVacancy([FromBody] Vacancy vacancy)
        {
            try
            {
                var skills = new List<Skill>();
                foreach (Skill s in vacancy.Skills)
                {
                    var skill = (from sk in _context.Skills
                                 where sk.Name.Equals(s.Name)
                                 select sk).FirstOrDefault();
                    skills.Add(skill ?? s);
                }
                vacancy.Skills = skills;
                foreach (Gender g in vacancy.Genders)
                    _context.Entry(g).State = System.Data.Entity.EntityState.Unchanged;
                foreach (Role r in vacancy.Roles)
                    _context.Entry(r).State = System.Data.Entity.EntityState.Unchanged;

                _context.Vacancies.AddOrUpdate(vacancy);
                _context.SaveChanges();

                return Created("Ok", vacancy);
            }
            catch (Exception error)
            {
                return InternalServerError(error);
            }
        }

        /// <summary>
        /// Inativa oferta de vaga.
        /// </summary>
        /// <param name="vacancyId">Identificação da vaga</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Vacancy/{vacancyId:int}")]
        public IHttpActionResult DeleteVacancy(int vacancyId)
        {
            try
            {
                var vacancy = new Vacancy { VacancyId = vacancyId, Deleted = true };
                _context.Vacancies.Attach(vacancy);
                _context.Entry(vacancy).Property(x => x.Deleted).IsModified = true;

                _context.SaveChanges();
                return Ok(vacancy);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

    }
}