using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OctoBridge.Api.Controllers
{
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
