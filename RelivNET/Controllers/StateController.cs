using BusinessLogic.Logic;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RelivNET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;

        public StateController(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        [HttpGet("{Id:int}", Name = "GetState")]
        public async Task<ActionResult<List<State>>> GetStateById(int Id)
        {
            return Ok(await _stateRepository.GetStateByIdAsync(Id));
        }

        [HttpGet(Name = "GetAllState")]
        public async Task<ActionResult<IEnumerable<State>>> GetAllState()
        {
            return Ok(await _stateRepository.GetStateAsync());
        }

        [HttpPost(Name = "AddState")]
        public async Task<IActionResult> AddState([FromBody] State state)
        {
            _stateRepository.AddState(state);
            return Ok(state);
        }

        [HttpDelete("{Id:int}", Name = "DeleteState")]
        public async Task<IActionResult> DeleteState(int Id)
        {
            _stateRepository.DeleteState(Id);
            return Ok();
        }

        [HttpPut(Name = "UpdateState")]
        public async Task<IActionResult> UpdateState([FromBody] State state)
        {
            _stateRepository.UpdateState(state);
            return Ok(state);
        }
    }
}
