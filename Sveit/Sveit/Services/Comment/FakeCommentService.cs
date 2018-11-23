using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sveit.Models;

namespace Sveit.Services.Comment
{
    public class FakeCommentService : ICommentService
    {
        public IEnumerable<Models.Comment> Comments { get; set; }

        private IEnumerable<Models.Comment> GetFakeComments()
        {
            yield return new Models.Comment { CommentId = 1, Text = "Comentário de teste sobre o jogador, expressando a opinião sobre seu nível de habilidade e características pessoais referentes ao avaliado, além de mencionar as habilidades específicas do mesmo.", ToPlayer = new Models.Player { Nickname = "Jogador teste1" }, FromPlayer = new Models.Player { Nickname = "Jogador teste2" }, CreatedAt = DateTime.Now.AddHours(-4) };
            yield return new Models.Comment { CommentId = 2, Text = "Comentário de teste sobre o jogador, expressando a opinião sobre seu nível de habilidade e características pessoais referentes ao avaliado, além de mencionar as habilidades específicas do mesmo.", ToPlayer = new Models.Player { Nickname = "Jogador teste1" }, FromPlayer = new Models.Player { Nickname = "Jogador teste2" }, CreatedAt = DateTime.Now.AddHours(-4) };
            yield return new Models.Comment { CommentId = 3, Text = "Comentário de teste sobre o jogador, expressando a opinião sobre seu nível de habilidade e características pessoais referentes ao avaliado, além de mencionar as habilidades específicas do mesmo.", ToPlayer = new Models.Player { Nickname = "Jogador teste1" }, FromPlayer = new Models.Player { Nickname = "Jogador teste2" }, CreatedAt = DateTime.Now.AddHours(-4) };
            yield return new Models.Comment { CommentId = 4, Text = "Comentário de teste sobre o jogador, expressando a opinião sobre seu nível de habilidade e características pessoais referentes ao avaliado, além de mencionar as habilidades específicas do mesmo.", ToPlayer = new Models.Player { Nickname = "Jogador teste1" }, FromPlayer = new Models.Player { Nickname = "Jogador teste2" }, CreatedAt = DateTime.Now.AddHours(-4) };
        }

        public FakeCommentService()
        {
            Comments = GetFakeComments();
        }

        public Task<bool> DeleteComment(int commentId)
        {
            return Task.FromResult(false);
        }

        public Task<Models.Comment> GetById(int commentId)
        {
            return Task.FromResult(new Models.Comment { CommentId = 1, Text = "Comentário de teste sobre o jogador, expressando a opinião sobre seu nível de habilidade e características pessoais referentes ao avaliado, além de mencionar as habilidades específicas do mesmo.", ToPlayer = new Models.Player { Nickname = "Jogador teste1" }, FromPlayer = new Models.Player { Nickname = "Jogador teste2" }, CreatedAt = DateTime.Now.AddHours(-4) });
        }

        public Task<IEnumerable<Models.Comment>> GetCommentsFromPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return Task.FromResult(Comments);
        }

        public Task<IEnumerable<Models.Comment>> GetCommentsToPlayer(int playerId, DateTime? initialDate = null, DateTime? finalDate = null)
        {
            return Task.FromResult(Comments);
        }

        public Task<Models.Comment> PostComment(Models.Comment comment)
        {
            return Task.FromResult(comment);
        }
    }
}
