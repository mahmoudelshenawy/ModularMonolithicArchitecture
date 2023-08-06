using MediatR;
using Module.Users.Shared.Dtos;
using Module.Users.Shared.UserApiInterfaces;

namespace Module.Catalog.Core.Queries.Users.GetUserDetails
{
    public record GetUserDetailsQuery(string userId) : IRequest<IBaseUser>;


    public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, IBaseUser>
    {
        private readonly IUserPublicApi _userPublicApi;
        public GetUserDetailsQueryHandler(IUserPublicApi userPublicApi)
        {
            _userPublicApi = userPublicApi;
        }

        public async Task<IBaseUser> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
        {
            
            var response = await _userPublicApi.GetUserDetails(request.userId);
            return response;
        }
    }


}
