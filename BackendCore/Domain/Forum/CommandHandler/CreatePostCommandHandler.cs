using BackendCore.Domain.Forum.Command;
using BackendCore.Domain.Forum.DTO;
using BackendCore.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.CommandHandler
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, CreatePostResponseDTO>
    {
        private readonly ApplicationDatabaseContext _context;
        public CreatePostCommandHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<CreatePostResponseDTO> Handle(CreatePostCommand command, CancellationToken token)
        {
            var post = new Post
            {
                Author = _context.AspNetUsers.Find(command.UserId),
                Created = DateTime.Now.ToUniversalTime(),
                Answear = command.Content
            };
            _context.Thread.Find(command.ThreadId).Post.Add(post);
            await _context.SaveChangesAsync(token);
            return new CreatePostResponseDTO { PostPlace = _context.Thread.Find(command.ThreadId).Post.Count(x => x.Created < post.Created) + 1 };
        }
    }
}
