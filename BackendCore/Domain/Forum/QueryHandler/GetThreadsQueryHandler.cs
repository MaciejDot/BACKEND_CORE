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
    public class GetThreadsCountQueryHandler :IRequestHandler<GetThreadsCountQuery,int>
    {
        private readonly ApplicationDatabaseContext _context;
        public GetThreadsCountQueryHandler(ApplicationDatabaseContext applicationDatabaseContext)
        {
            _context = applicationDatabaseContext;
        }
        public async Task<int> Handle(GetThreadsCountQuery request, CancellationToken token)
        {
            return await _context.Thread.CountAsync(t=> t.Subject.Title == request.SubjectName, token);
        }
    }
}
