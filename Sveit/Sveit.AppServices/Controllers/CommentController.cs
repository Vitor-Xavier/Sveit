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
    /// Controla o acesso aos dados de comentários.
    /// </summary>
    public class CommentController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados.
        /// </summary>
        public CommentController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Busca o comentário em específico com base na identificação.
        /// </summary>
        /// <returns>Comentário</returns>
        [HttpGet]
        [Route("Comment/{commentId:int}")]
        public Comment GetById(int commentId)
        {
            return (from c in _context.Comments
                    where c.CommentId == commentId &&
                    c.Deleted == false
                    select c).SingleOrDefault();
        }

        /// <summary>
        /// Busca todas os comentários direcionados a determinado jogador.
        /// </summary>
        /// <param name="playerId">Identificação do jogador</param>
        /// <param name="initialDate">Data de inicio para busca</param>
        /// <param name="finalDate">Data final para busca</param>
        /// <returns>Lista de comentários</returns>
        [HttpGet]
        [Route("Comment/To/{playerId:int}")]
        [Route("Comment/To/{playerId:int}/{initialDate:datetime?}/")]
        [Route("Comment/To/{playerId:int}/{initialDate:datetime?}/{finalDate:datetime?}/")]
        public IEnumerable<Comment> GetToPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return (from c in _context.Comments
                    where (initialDate != null ? initialDate <= c.CreatedAt : true) &&
                    (finalDate != null ? finalDate >= c.CreatedAt : true) && 
                    c.ToPlayer.PlayerId == playerId &&
                    c.Deleted == false
                    orderby c.CreatedAt descending
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Busca por comentários realizados por determinado jogador.
        /// </summary>
        /// <param name="playerId">Identificação do jogador.</param>
        /// <param name="initialDate">Data de inicio para busca</param>
        /// <param name="finalDate">Data final para busca</param>
        /// <returns>Lista de comentários</returns>
        [HttpGet]
        [Route("Comment/From/{playerId:int}")]
        [Route("Comment/From/{playerId:int}/{initialDate:datetime?}/")]
        [Route("Comment/From/{playerId:int}/{initialDate:datetime?}/{finalDate:datetime?}/")]
        public IEnumerable<Comment> GetFromPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return (from c in _context.Comments
                    where (initialDate != null ? initialDate <= c.CreatedAt : true) &&
                    (finalDate != null ? finalDate >= c.CreatedAt : true) && 
                    c.FromPlayer.PlayerId == playerId &&
                    c.Deleted == false
                    orderby c.CreatedAt descending
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Adiciona comentário.
        /// </summary>
        /// <param name="comment">Dados do comentário</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Comment")]
        public IHttpActionResult PostComment([FromBody] Comment comment)
        {
            try
            {
                _context.Comments.AddOrUpdate(comment);
                _context.SaveChanges();

                return Created("Ok", comment);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa comentário.
        /// </summary>
        /// <param name="commentId">Identificação do comentário</param>
        /// <returns>Sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Comment/{commentId:int}")]
        public IHttpActionResult DeleteComment(int commentId)
        {
            try
            {
                var comment = AppServices.Utils.ModelsDefault.GetDefaultComment();
                comment.CommentId = commentId;
                comment.Deleted = true;
                _context.Comments.Attach(comment);
                _context.Entry(comment).Property(x => x.Deleted).IsModified = true;

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
