using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.Application.Reactions.Commands.CreateReaction;
using SchoolIsComingSoon.Application.Reactions.Commands.DeleteReaction;
using SchoolIsComingSoon.Application.Reactions.Queries.GetReactionList;
using SchoolIsComingSoon.WebAPI.Models.AppUser;
using SchoolIsComingSoon.WebAPI.Models.Reaction;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class ReactionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public ReactionController(IMapper mapper, ICurrentUserService currentUserService) =>
            (_mapper, _currentUserService) = (mapper, currentUserService);

        /// <summary>
        /// Get list of reactions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /reaction/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <param name="postId">Post id (guid)</param>
        /// <returns>Returns ReactionListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReactionListVm>> GetAllReactions(Guid postId)
        {
            var query = new GetReactionListQuery
            {
                PostId = postId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Creates the reaction
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /reaction
        /// {
        ///     postId: "id of the post",
        ///     isLiked: "type of reaction"
        /// }
        /// </remarks>
        /// <param name="createReactionDto">CreateReactionDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Guid>> CreateReaction([FromBody] CreateReactionDto createReactionDto)
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

            var command2 = _mapper.Map<CreateReactionCommand>(createReactionDto);
            command2.UserId = _currentUserService.UserId;
            var commentId = await Mediator.Send(command2);
            return Ok(commentId);
        }

        /// <summary>
        /// Deletes the reaction by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /reaction/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <param name="id">Id of the reaction (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteReaction(Guid id)
        {
            var command = new DeleteReactionCommand
            {
                Id = id,
                UserId = _currentUserService.UserId
            };
            await Mediator.Send(command);
            return NoContent();
        }
    }
}