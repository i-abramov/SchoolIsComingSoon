using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscription;
using SchoolIsComingSoon.Application.Subscriptions.Queries.GetSubscriptionList;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    public class SubscriptionController : BaseController
    {
        /// <summary>
        /// Get subscription by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscription/20DC3FB7-BFA9-40AE-8CEF-12BC6F31DD79
        /// </remarks>
        /// <returns>Returns SubscriptionVm</returns>
        /// <response code="200">Success</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SubscriptionVm>> GetSubscription(Guid id)
        {
            var query = new GetSubscriptionQuery
            {
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Get list of subscriptions
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /subscription/25FFCB91-AB0A-4A24-9108-EC94F8BE000A
        /// </remarks>
        /// <returns>Returns SubscriptionListVm</returns>
        /// <response code="200">Success</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SubscriptionListVm>> GetAllSubscriptions()
        {
            var query = new GetSubscriptionListQuery();
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
    }
}