using BackendCore.Domain.Article.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Article.Command
{
    public class CreateArticleCommand : IRequest<CreateArticleDTO>
    {
        public string AuthorId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] Thumbnail { get; set; }
        public string Content { get; set; }

        public DateTime Created { get; set; }
    }
}
