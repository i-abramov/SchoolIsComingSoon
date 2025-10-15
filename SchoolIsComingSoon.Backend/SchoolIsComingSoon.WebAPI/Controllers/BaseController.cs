using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SchoolIsComingSoon.WebAPI.Controllers
{
    [ApiController]
    [Route("api/{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        private IMediator _mediator;
    }
}