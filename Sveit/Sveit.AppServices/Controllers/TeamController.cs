﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Http;
using Sveit.API.Context;
using Sveit.Models;

namespace Sveit.API.Controllers
{
    /// <summary>
    /// Controla dados relacionados a equipes na base de dados.
    /// </summary>
    public class TeamController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados;
        /// </summary>
        public TeamController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna todas as equipes registradas.
        /// </summary>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team")]
        public IEnumerable<Team> Get()
        {
            return (from t in _context.Teams
                    where t.Deleted == false
                    select t).AsEnumerable();
        }

        /// <summary>
        /// Retorna equipe pelo id.
        /// </summary>
        /// <param name="teamId">Identificação da equipe</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/{teamId:int}")]
        public Team GetById(int teamId)
        {
            return (from t in _context.Teams
                    where t.TeamId == teamId &&
                    t.Deleted == false
                    select t).SingleOrDefault();
        }

        /// <summary>
        /// Retorna equipes com o nome correspondente ao texto informado.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/name/{name}")]
        public IEnumerable<Team> GetByName(string name)
        {
            return (from t in _context.Teams
                    where t.Name.ToLower().Contains(name.ToLower()) &&
                    t.Deleted == false
                    select t).AsEnumerable();
        }

        /// <summary>
        /// Retorna equipes com o nome correspondente ao texto, e o id ao jogo informado.
        /// </summary>
        /// <param name="name">Texto para busca</param>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/name/{name}/{gameId:int}")]
        public IEnumerable<Team> GetByNameAndGame(string name, int gameId)
        {
            return (from t in _context.Teams
                    where t.Name.ToLower().Contains(name.ToLower()) &&
                    t.GamePlatform.GameId == gameId &&
                    t.Deleted == false
                    select t).AsEnumerable();
        }

        /// <summary>
        /// Retorna equipes do jogo informado.
        /// </summary>
        /// <param name="gameId">Identificação do jogo</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/Game/{gameId:int}")]
        public IEnumerable<Team> GetByGame(int gameId)
        {
            return (from t in _context.Teams
                    where t.GamePlatform.GameId == gameId &&
                    t.Deleted == false
                    select t).AsEnumerable();
        }

        /// <summary>
        /// Retorna equipes onde o nome do jogo corresponda ao texto informado.
        /// </summary>
        /// <param name="name">Texto do jogo para busca</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/Game/name/{name}")]
        public IEnumerable<Team> GetByGameName(string name)
        {
            return (from tp in _context.Teams
                    where tp.GamePlatform.Game.Name.ToLower().Contains(name.ToLower()) &&
                    tp.Deleted == false
                    select tp).AsEnumerable();
        }

        /// <summary>
        /// Retorna equipes onde o jogador informado participa.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/Player/{playerId:int}")]
        public IEnumerable<Team> GetByPlayer(int playerId)
        {
            return (from tp in _context.TeamPlayers
                    where tp.PlayerId == playerId &&
                    tp.Deleted == false
                    select tp.Team).AsEnumerable();
        }

        /// <summary>
        /// Retorna membros da equipe específica.
        /// </summary>
        /// <param name="teamId">Identificação da equipe</param>
        /// <returns>Lista de Equipes</returns>
        [HttpGet]
        [Route("Team/Players/{teamId:int}")]
        public IEnumerable<Player> GetPlayers(int teamId)
        {
            return (from tp in _context.TeamPlayers
                    where tp.TeamId == teamId &&
                    tp.Deleted == false
                    select tp.Player).AsEnumerable();
        }

        /// <summary>
        /// Associa jogador a equipe.
        /// </summary>
        /// <param name="teamPlayer">Identificação do jogador e equipe</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Team/Player")]
        public IHttpActionResult PostTeamPlayer([FromBody] TeamPlayer teamPlayer)
        {
            try
            {
                _context.TeamPlayers.AddOrUpdate(teamPlayer);
                _context.SaveChanges();

                return Created("Ok", teamPlayer);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Adiciona equipe.
        /// </summary>
        /// <param name="team">Dados da equipe</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Team")]
        public IHttpActionResult PostTeam([FromBody] Team team)
        {
            try
            {
                _context.Teams.AddOrUpdate(team);
                _context.SaveChanges();

                return Created("Ok", team);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa equipe.
        /// </summary>
        /// <param name="teamId">Identificação da equipe</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Team/{teamId:int}")]
        public IHttpActionResult DeleteTeam(int teamId)
        {
            try
            {
                var team = new Team { TeamId = teamId, Deleted = true };
                _context.Teams.Attach(team);
                _context.Entry(team).Property(x => x.Deleted).IsModified = true;

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