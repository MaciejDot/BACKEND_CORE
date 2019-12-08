using BackendCore.Domain.Forum.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.Command
{
    public class CreateForumSubjectCommand :IRequest<CreateForumSubjectResponseDTO>
    {
        public byte[] Content { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
