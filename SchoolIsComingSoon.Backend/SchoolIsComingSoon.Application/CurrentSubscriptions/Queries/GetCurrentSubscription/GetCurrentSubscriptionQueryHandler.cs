using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.Common.Exceptions;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Domain;

namespace SchoolIsComingSoon.Application.CurrentSubscriptions.Queries.GetCurrentSubscription
{
    public class GetCurrentSubscriptionQueryHandler : IRequestHandler<GetCurrentSubscriptionQuery, CurrentSubscriptionVm>
    {
        private readonly ISicsDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCurrentSubscriptionQueryHandler(ISicsDbContext dbContext, IMapper mapper) =>
            (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<CurrentSubscriptionVm> Handle(GetCurrentSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.CurrentSubscriptions
                .ProjectTo<CurrentSubscriptionVm>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(subscription => subscription.UserId == request.UserId, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(CurrentSubscription), request.UserId);
            }

            return _mapper.Map<CurrentSubscriptionVm>(entity);
        }
    }
}