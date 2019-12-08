using BackendCore.Domain.Forum.Query;
using BackendCore.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BackendCore.Domain.Forum.QueryHandler
{
    public class SubjectNameExistsInDatabaseQueryHandler : IRequestHandler<SubjectNameExistsInDatabaseQuery,string>
    {
        private readonly ApplicationDatabaseContext _context;
        public SubjectNameExistsInDatabaseQueryHandler(ApplicationDatabaseContext context)
        {
            _context = context;
        }
        public async Task<string> Handle(SubjectNameExistsInDatabaseQuery request, CancellationToken token)
        {
            return (await _context.Subject.FirstOrDefaultAsync(x => x.Title == request.SubjectName, token))?.Title;
        }
    }
}
