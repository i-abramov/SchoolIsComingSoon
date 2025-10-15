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
    public class CurrentSubscriptionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<CurrentSubscriptionController> _logger;

        public CurrentSubscriptionController(IMapper mapper, ICurrentUserService currentUserService, ILogger<CurrentSubscriptionController> logger) =>
            (_mapper, _currentUserService, _logger) = (mapper, currentUserService, logger);

        /// <summary>
        /// Get current user's subscription
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /currentSubscription
        /// </remarks>
        /// <returns>Returns CurrentSubscriptionVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CurrentSubscriptionVm>> GetCurrentSubscription()
        {
            _logger.LogInformation("Claims: {Claims}",
                string.Join(", ", User?.Claims.Select(c => $"{c.Type}={c.Value}") ?? new string[0]));

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
                UserId = _currentUserService.UserId
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}