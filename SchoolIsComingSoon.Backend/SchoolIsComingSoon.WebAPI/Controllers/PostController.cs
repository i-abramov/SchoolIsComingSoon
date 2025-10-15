using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Application.Posts.Commands.CreatePost;
using SchoolIsComingSoon.Application.Posts.Commands.DeletePost;
using SchoolIsComingSoon.Application.Posts.Commands.UpdatePost;
using SchoolIsComingSoon.Application.Posts.Queries.GetPost;
using SchoolIsComingSoon.Application.Posts.Queries.GetPostList;
using SchoolIsComingSoon.WebAPI.Models.AppUser;
using SchoolIsComingSoon.WebAPI.Models.Post;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class PostController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISicsDbContext _dbContext;

        public PostController(IMapper mapper, ICurrentUserService currentUserService, ISicsDbContext dbContext) =>
            (_mapper, _currentUserService, _dbContext) = (mapper, currentUserService, dbContext);

        /// <summary>
        /// Get list of posts by category id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /post
        /// </remarks>
        /// <returns>Returns PostListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostListVm>> GetAllPosts()
        {
            var query = new GetPostListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /post/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <returns>Returns PostVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostVm>> GetPost(Guid id)
        {
            var query = new GetPostQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the post
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /post
        /// {
        ///     text: "post text"
        /// }
        /// </remarks>
        /// <param name="createPostDto">CreatePostDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreatePost([FromBody] CreatePostDto createPostDto)
        {
            var createAppUserDto = new CreateAppUserDto
            {
                Id = _currentUserService.UserId,
                UserName = _currentUserService.UserName,
                FirstName = _currentUserService.FirstName,
                LastName = _currentUserService.LastName,
                Email = _currentUserService.Email,
                Role = _currentUserService.Role
            };

            var command1 = _mapper.Map<CreateAppUserCommand>(createAppUserDto);
            await Mediator.Send(command1);

            var command2 = _mapper.Map<CreatePostCommand>(createPostDto);
            command2.UserId = _currentUserService.UserId;
            var postId = await Mediator.Send(command2);
            return Ok(postId);
        }

        /// <summary>
        /// Updates the post
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /post
        /// {
        ///     text: "updated post text"
        /// }
        /// </remarks>
        /// <param name="updatePostDto">UpdatePostDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> UpdatePost([FromBody] UpdatePostDto updatePostDto)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == updatePostDto.Id);

            if (_currentUserService.UserId == post?.UserId ||
                _currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = _mapper.Map<UpdatePostCommand>(updatePostDto);
                command.UserId = _currentUserService.UserId;
                await Mediator.Send(command);
            }

            return NoContent();
        }

        /// <summary>
        /// Deletes the post by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /post/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the post (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var post = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == id);

            if (_currentUserService.UserId == post?.UserId ||
                _currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = new DeletePostCommand
                {
                    Id = id,
                    UserId = _currentUserService.UserId
                };
                await Mediator.Send(command);
            }

            return NoContent();
        }
    }
}