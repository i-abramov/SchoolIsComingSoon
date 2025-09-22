using Asp.Versioning;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SchoolIsComingSoon.Application.AppUsers.Commands.CreateAppUser;
using SchoolIsComingSoon.Application.CurrentSubscriptions.Queries.GetCurrentSubscription;
using SchoolIsComingSoon.Application.Interfaces;
using SchoolIsComingSoon.WebAPI.Models.AppUser;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class CurrentSubscriptionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CurrentSubscriptionController(IMapper mapper, ICurrentUserService currentUserService) =>
            (_mapper, _currentUserService) = (mapper, currentUserService);

        /// <summary>
        /// Get current subscription by userId
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /currentSubscription/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <returns>Returns CurrentSubscriptionVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CurrentSubscriptionVm>> GetCurrentSubscription(Guid userId)
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

            var command = _mapper.Map<CreateAppUserCommand>(createAppUserDto);
            await Mediator.Send(command);

            var query = new GetCurrentSubscriptionQuery
            {
                UserId = userId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}