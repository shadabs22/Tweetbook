using Refit;
using System;
using System.Threading.Tasks;

namespace Tweetbook.Sdk.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var cachedToken = string.Empty;
            var identityApi = RestService.For<IIdentityApi>("https://localhost:5001");
            var tweetbookApi = RestService.For<ITweetbookApi>("https://localhost:5001", new RefitSettings
            {
                AuthorizationHeaderValueGetter = () => Task.FromResult(cachedToken)
            });

            var registrationResponse = await identityApi.RegisterAsync(new Contracts.v1.Requests.UserRegistrationRequest {
            Email="6@abcd.com",
            Password="111aA@"
            });

            var loginResponse = await identityApi.LoginAsync(new Contracts.v1.Requests.UserLoginRequest
            {
                Email = "5@abcd.com",
                Password = "111aA@"
            });

            cachedToken = loginResponse.Content.Token;

            var allPost = await tweetbookApi.GetAllAsync();

            var createdPost = await tweetbookApi.CreateAsync(new Contracts.v1.Requests.CreatePostRequest
            {
                name = "This is post1 from SDK",
                tags = new System.Collections.Generic.List<Contracts.v1.Requests.TagPostRequest> {
                    new Contracts.v1.Requests.TagPostRequest { text = "tag from SDK" }
                }
            });

            var retrievePost = await tweetbookApi.GetAsync(createdPost.Content.id);

            var updatedPost = await tweetbookApi.UpdateAsync(createdPost.Content.id, new Contracts.v1.Requests.UpdatePostRequest
            {
                name = "This is updated post1 from SDK"
            });

            var retrievePost1 = await tweetbookApi.GetAsync(createdPost.Content.id);

            var deletePost = await tweetbookApi.DeleteAsync(createdPost.Content.id);

        }
    }
}
