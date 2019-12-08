using BackendCore.Domain.Wiki.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendCore.Domain.Wiki.Command
{

    public class CreateWikiArticleCommand : IRequest<string>
    {
        public string Name { get; set; }
        public IEnumerable<Section> Sections { get; set; }
    }
}
