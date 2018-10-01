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
    /// Manipula dados de jogadores na base de dados.
    /// </summary>
    public class PlayerController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados.
        /// </summary>
        public PlayerController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Retorna todos os jogadores registrados.
        /// </summary>
        /// <returns>Lista de Jogadores</returns>
        [HttpGet]
        [Route("Player")]
        public IEnumerable<Player> Get()
        {
            return (from p in _context.Players
                    where p.Deleted == false
                    select p).AsEnumerable();
        }

        /// <summary>
        /// Busca jogador pelo id informado.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Jogador</returns>
        [HttpGet]
        [Route("Player/{playerId:int}")]
        public Player GetById(int playerId)
        {
            return (from p in _context.Players
                    where p.PlayerId == playerId &&
                    p.Deleted == false
                    select p).SingleOrDefault();
        }

        /// <summary>
        /// Busca por jogadores com o texto informado em seu nome ou nickname.
        /// </summary>
        /// <param name="name">Texto para busca por nome ou nickname</param>
        /// <returns>Lista de Jogadores</returns>
        [HttpGet]
        [Route("Player/name/{name}")]
        public IEnumerable<Player> GetByName(string name)
        {
            return (from p in _context.Players
                    where (p.Name.ToLower().Contains(name.ToLower()) ||
                    p.Nickname.ToLower().Contains(name.ToLower())) &&
                    p.Deleted == false
                    select p).AsEnumerable();
        }

        /// <summary>
        /// Retorna as equipes em que o jogador participa.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Lista de equipes</returns>
        [HttpGet]
        [Route("Player/Teams/{playerId:int}")]
        public IEnumerable<Team> GetTeams(int playerId)
        {
            return (from ps in _context.TeamPlayers
                    where ps.PlayerId == playerId &&
                    ps.Deleted == false
                    select ps.Team).AsEnumerable();
        }

        /// <summary>
        /// Retorna as habilidades associadas ao jogador
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Lista de Habilidades</returns>
        [HttpGet]
        [Route("Player/Skills/{playerId:int}")]
        public IEnumerable<Skill> GetPlayerSkills(int playerId)
        {
            return (from ps in _context.PlayerSkills
                    where ps.PlayerId == playerId &&
                    ps.Deleted == false
                    select ps.Skill).AsEnumerable();
        }

        /// <summary>
        /// Associa habilidade a jogador.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <param name="skillId">Identificação da habilidade</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Player/Skill")]
        public IHttpActionResult PostPlayerSkill(int playerId, int skillId)
        {
            try
            {
                var playerSkill = new PlayerSkill
                {
                    PlayerId = playerId,
                    SkillId = skillId
                };
                _context.PlayerSkills.AddOrUpdate(playerSkill);
                _context.SaveChanges();

                return Created("Ok", playerSkill);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Adiciona ou altera jogador na base de dados.
        /// </summary>
        /// <param name="player">Dados do jogador</param>
        /// <returns>Sucesso da operação</returns>
        [HttpPost]
        [Route("Player")]
        public IHttpActionResult PostPlayer([FromBody] Player player)
        {
            try
            {
                if (player.Gender != null)
                {
                    var gender = _context.Genders.Where(g => g.GenderId == player.Gender.GenderId).FirstOrDefault();
                    player.Gender = gender;
                }

                _context.Players.AddOrUpdate(player);
                _context.SaveChanges();

                return Created("Ok", player);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inativa jogador na base de dados.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <returns>Sucesso da operação</returns>
        [HttpDelete]
        [Route("Player/{playerId:int}")]
        public IHttpActionResult DeletePlayer(int playerId)
        {
            try
            {
                var player = new Player { PlayerId = playerId, Deleted = true };
                _context.Players.Attach(player);
                _context.Entry(player).Property(x => x.Deleted).IsModified = true;

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