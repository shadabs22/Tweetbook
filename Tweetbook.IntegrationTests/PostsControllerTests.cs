using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Tweetbook.Contracts.v1;
using Tweetbook.Domain.v1;
using Xunit;

namespace Tweetbook.IntegrationTests
{
    public class PostsControllerTests : IntegrationTests
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnEmptyResponse()
        {
            //arrange
            await AuthenticateAsync();

            //act
            var reponse = await _httpClient.GetAsync(ApiRoutes.Posts.GetAll);

            //assert
            reponse.StatusCode.Should().Be(HttpStatusCode.OK);
            (await reponse.Content.ReadAsAsync<List<Post>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task GetAll_ReturnPost_WhenPostExistsInTheDatabase()
        {
            //arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new Contracts.v1.Requests.CreatePostRequest { name = "Test request" });

            //act
            var response = await _httpClient.GetAsync(ApiRoutes.Posts.Get.Replace("{postId}", "1"));

            //assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Post>();
            returnedPost.id.Should().Be(createdPost.id);
            returnedPost.name.Should().Be("Test request");
        }
    }
}
