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
    /// Gerencia o acesso as notícias na base de dados.
    /// </summary>
    public class ContentController : ApiController
    {
        private DatabaseContext _context;

        /// <summary>
        /// Inicializa conexao de dados.
        /// </summary>
        public ContentController()
        {
            _context = new DatabaseContext();
        }

        /// <summary>
        /// Busca as notícias adicionadas no intervalo especificado.
        /// </summary>
        /// <param name="initialDate">Data de inicio para busca</param>
        /// <param name="finalDate">Data final para busca</param>
        /// <returns>Lista de Notícias</returns>
        [HttpGet]
        [Route("Content")]
        [Route("Content/{initialDate:datetime?}/")]
        [Route("Content/{initialDate:datetime?}/{finalDate:datetime?}/")]
        public IEnumerable<Content> Get(DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return (from c in _context.Contents
                    where (initialDate != null ? initialDate <= c.CreatedAt : true) &&
                    (finalDate != null ? finalDate >= c.CreatedAt : true) &&
                    c.Deleted == false
                    orderby c.CreatedAt descending
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Busca as notícia em específico com base no id.
        /// </summary>
        /// <returns>Notícia</returns>
        [HttpGet]
        [Route("Content/{contentId}")]
        public Content GetById(int contentId)
        {
            return (from c in _context.Contents
                    where c.ContentId == contentId &&
                    c.Deleted == false
                    select c).SingleOrDefault();
        }

        /// <summary>
        /// Procura por notícias contendo o texto informado no título, ou em suas tags.
        /// </summary>
        /// <param name="text">Texto para busca</param>
        /// <returns>Lista de Notícias</returns>
        [HttpGet]
        [Route("Content/text/{text}")]
        public IEnumerable<Content> GetByText(string text)
        {
            return (from c in _context.Contents
                    where (c.Title.ToLower().Contains(text.ToLower()) ||
                    c.Tags.Any(t => t.Name.ToLower().Contains(text.ToLower()))) &&
                    c.Deleted == false
                    orderby c.CreatedAt descending
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Procura por notícias a partir de determinada fonte.
        /// </summary>
        /// <param name="name">Texto para busca da fonte</param>
        /// <returns>Lista de Notícias</returns>
        [HttpGet]
        [Route("Content/source/{name}")]
        public IEnumerable<Content> GetBySource(string name)
        {
            return (from c in _context.Contents
                    where c.Source.ToLower().Contains(name.ToLower()) &&
                    c.Deleted == false
                    orderby c.CreatedAt descending
                    select c).AsEnumerable();
        }

        /// <summary>
        /// Adiciona notícia.
        /// </summary>
        /// <param name="content">Dados da notícia</param>
        /// <returns>sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Content")]
        public IHttpActionResult PostContent([FromBody] Content content)
        {
            try
            {
                foreach (Tag t in content.Tags)
                    if (t.TagId == 0)
                        _context.Entry(t).State = System.Data.Entity.EntityState.Unchanged;
                _context.Contents.AddOrUpdate(content);
                _context.SaveChanges();

                return Created("Ok", content);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Associa tags a notícia.
        /// </summary>
        /// <param name="contentId">Identificação da notícia</param>
        /// <param name="tags">Lista de tags</param>
        /// <returns>sucesso da operação</returns>
        [Authorize]
        [HttpPost]
        [Route("Content/{contentId:int}")]
        public IHttpActionResult PostContentTags(int contentId, [FromBody] IEnumerable<Tag> tags)
        {
            try
            {
                var content = (from c in _context.Contents
                               where c.ContentId == contentId &&
                               c.Deleted == false
                               select c).SingleOrDefault();

                foreach (var tag in tags)
                {
                    var _tag = (from t in _context.Tags
                                where t.TagId == tag.TagId &&
                                t.Deleted == false
                                select t).SingleOrDefault();
                    content.Tags.Add(_tag);
                }
                _context.Contents.AddOrUpdate(content);
                _context.SaveChanges();

                return Created("Ok", content);
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        /// <summary>
        /// Inativa notícia.
        /// </summary>
        /// <param name="contentId">Identificação da notícia</param>
        /// <returns>sucesso da operação</returns>
        [Authorize]
        [HttpDelete]
        [Route("Content/{contentId:int}")]
        public IHttpActionResult DeleteContent(int contentId)
        {
            try
            {
                var content = AppServices.Utils.ModelsDefault.GetDefaultContent();
                content.ContentId = contentId;
                content.Deleted = true;
                _context.Contents.Attach(content);
                _context.Entry(content).Property(x => x.Deleted).IsModified = true;

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
