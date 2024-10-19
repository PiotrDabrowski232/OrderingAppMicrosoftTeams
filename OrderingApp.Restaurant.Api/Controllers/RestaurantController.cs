using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingApp.Restaurant.Logic.Functions.Query;

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
                var result = await _mediator.Send(new GetAllRestaurantQuery());
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/Restaurant")]
        public async Task<IActionResult> AddRestaurants([FromQuery] string id)
        {
            try
            {
                var result = await _mediator.Send(new GetRestaurantQuery(id));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
