using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingApp.Logic.Functions.Query.Restaurant;

namespace OrderingApp.Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/Restaurants")]
        public async Task<IActionResult> GetRestaurants()
        {
            try
            {
                var result = _mediator.Send(new GetRestaurantsQuery());
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
