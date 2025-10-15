using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Application.PostImages.Commands.CreatePostImage;
using SchoolIsComingSoon.Application.PostImages.Commands.DeletePostImage;
using SchoolIsComingSoon.Application.PostImages.Queries.GetPostImageList;
using SchoolIsComingSoon.WebAPI.Models.AppUser;
using SchoolIsComingSoon.WebAPI.Models.PostImage;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class PostImageController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISicsDbContext _dbContext;

        public PostImageController(IMapper mapper, ICurrentUserService currentUserService, ISicsDbContext dbContext) =>
            (_mapper, _currentUserService, _dbContext) = (mapper, currentUserService, dbContext);

        /// <summary>
        /// Get list of postImages
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /postImage/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <param name="postId">Post id (guid)</param>
        /// <returns>Returns PostImageListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostImageListVm>> GetAllPostImages(Guid postId)
        {
            var query = new GetPostImageListQuery
            {
                PostId = postId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the postImage
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /postImage
        /// {
        ///     postId: "id of the post",
        ///     base64Code: "base64 code"
        /// }
        /// </remarks>
        /// <param name="createPostImageDto">CreatePostImageDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreatePostImage([FromBody] CreatePostImageDto createPostImageDto)
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

            var command2 = _mapper.Map<CreatePostImageCommand>(createPostImageDto);
            var postImageId = await Mediator.Send(command2);
            return Ok(postImageId);
        }

        /// <summary>
        /// Deletes the postImage by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /postImage/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the postImage (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePostImage(Guid id)
        {
            var postImage = await _dbContext.Images.FirstOrDefaultAsync(c => c.Id == id);

            if (_currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = new DeletePostImageCommand
                {
                    Id = id,
                    PostId = postImage.PostId
                };
                await Mediator.Send(command);
            }

            return NoContent();
        }
    }
}