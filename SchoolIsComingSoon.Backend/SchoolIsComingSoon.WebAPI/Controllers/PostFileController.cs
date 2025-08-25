using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Application.PostFiles.Commands.CreatePostFile;
using SchoolIsComingSoon.Application.PostFiles.Commands.DeletePostFile;
using SchoolIsComingSoon.Application.PostFiles.Queries.GetPostFileList;
using SchoolIsComingSoon.WebAPI.Models.AppUser;
using SchoolIsComingSoon.WebAPI.Models.PostFile;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class PostFileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISicsDbContext _dbContext;

        public PostFileController(IMapper mapper, ICurrentUserService currentUserService, ISicsDbContext dbContext) =>
            (_mapper, _currentUserService, _dbContext) = (mapper, currentUserService, dbContext);

        /// <summary>
        /// Get list of postFiles
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /postFile/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <param name="postId">Post id (guid)</param>
        /// <returns>Returns PostFileListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PostFileListVm>> GetAllPostFiles(Guid postId)
        {
            var query = new GetPostFileListQuery
            {
                PostId = postId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the postFile
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /postFile
        /// {
        ///     postId: "id of the post",
        ///     base64Code: "base64 code"
        /// }
        /// </remarks>
        /// <param name="createPostFileDto">CreatePostFileDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize(Roles = "Admin,Owner")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreatePostFile([FromBody] CreatePostFileDto createPostFileDto)
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

            var command2 = _mapper.Map<CreatePostFileCommand>(createPostFileDto);
            var postFileId = await Mediator.Send(command2);
            return Ok(postFileId);
        }

        /// <summary>
        /// Deletes the postFile by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /postFile/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the postFile (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeletePostFile(Guid id)
        {
            var postFile = await _dbContext.Files.FirstOrDefaultAsync(c => c.Id == id);

            if (_currentUserService.Role == "Admin" ||
                _currentUserService.Role == "Owner")
            {
                var command = new DeletePostFileCommand
                {
                    Id = id,
                    PostId = postFile.PostId
                };
                await Mediator.Send(command);
            }

            return NoContent();
        }
    }
}