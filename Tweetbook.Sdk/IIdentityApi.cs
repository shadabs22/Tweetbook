using Refit;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Contracts.v1.Requests;
using Tweetbook.Contracts.v1.Responses;

namespace Tweetbook.Sdk
{
    public interface IIdentityApi
    {
        [Post(ApiRoutes.Identity.Register)]
        Task<ApiResponse<AuthSuccessResponse>> RegisterAsync([Body] UserRegistrationRequest registerationRequest);

        [Post(ApiRoutes.Identity.Login)]
        Task<ApiResponse<AuthSuccessResponse>> LoginAsync([Body] UserLoginRequest loginRequest);

        [Post(ApiRoutes.Identity.Refresh)]
        Task<ApiResponse<AuthSuccessResponse>> RefreshAsync([Body] RefreshTokenRequest refreshRequest);
    }
}
