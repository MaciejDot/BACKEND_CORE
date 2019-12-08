using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackendCore.Domain.Article.Command;
using BackendCore.Domain.Article.DTO;
using BackendCore.Data;
using MediatR;
namespace BackendCore.Domain.Article.CommandHandler
{
    public class CreateArticleCommandHandler :IRequestHandler<CreateArticleCommand, CreateArticleDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public CreateArticleCommandHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<CreateArticleDTO> Handle(CreateArticleCommand command, CancellationToken token)
        {
            var thumbnail = new Thumbnails { Content = command.Thumbnail };
            await _context.Thumbnails
                .AddAsync(thumbnail, token);
            var article = new Data.Article
            {
                Thumbnail = thumbnail,
                Content = command.Content,
                Author = await _context.AspNetUsers
                    .FindAsync(command.AuthorId),
                Title = command.Title,
                Created = command.Created,
                Description = command.Description
            };
            await _context.Article
                .AddAsync(article, token);
            await _context.SaveChangesAsync(token);
            return new CreateArticleDTO { ArticleId = article.Id };
        }
    }
}
