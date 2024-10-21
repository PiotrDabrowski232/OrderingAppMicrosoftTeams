using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderingApp.Restaurant.Logic.DTO;
using OrderingApp.Restaurant.Logic.Functions.Command;
using OrderingApp.Restaurant.Logic.Functions.Query;

namespace OrderingApp.Restaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DishController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("/Dishes")]
        public async Task<IActionResult> GetDishes([FromQuery] string restaurantId)
        {
            try
            {
                var result = await _mediator.Send(new GetAllRestaurantDishesQuery(restaurantId));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("/Block")]
        public async Task<IActionResult> BlockDish([FromQuery] string dishId)
        {
            try
            {
                var result = await _mediator.Send(new BlockDishCommand(dishId));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        [Route("/UnBlock")]
        public async Task<IActionResult> UnBlockDish([FromQuery] string dishId)
        {
            try
            {
                var result = await _mediator.Send(new UnBlockDishCommand(dishId));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/AddDish")]
        public async Task<IActionResult> AddDish([FromQuery] string restaurantId, [FromBody] NewDishDto dish)
        {
            try
            {
                var result = await _mediator.Send(new AddDishCommand(restaurantId, dish));
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
