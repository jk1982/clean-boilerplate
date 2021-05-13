using API.Commands;
using API.Presenters;
using Core.Abstraction.UseCases;
using Core.DTO.Requests.Person;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    public class PersonController : Controller
    {
        private readonly IPersonUseCase _useCase;
        private readonly PersonPresenter _presenter;

        public PersonController(IPersonUseCase useCase, PersonPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] NewPersonCommand command)
        {
            if (ModelState.IsValid)
            {
                await _useCase.Handle(
                    new CreatePersonRequest(command.Name),
                    _presenter
                );

                if (_presenter.Succeeded)
                    return Created($"api/person/{_presenter.ContentResult}", _presenter.ContentResult);
                else
                    return _presenter.ContentResult;
            }

            return BadRequest(ModelState);
        }
    }
}
