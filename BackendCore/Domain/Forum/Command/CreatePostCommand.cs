using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Command
{
    public class CreatePostCommand : IRequest<CreatePostResponseDTO>
    {
        public int ThreadId { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
    }
}
