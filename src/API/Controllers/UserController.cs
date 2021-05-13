using API.Commands.Users;
using API.Presenters.Users;
using Core.Abstraction.UseCases.Users;
using Core.DTO.Requests.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly ICreateUserUseCase _createUserUseCase;
        private readonly CreateUserPresenter _createUserPresenter;
        private readonly IGetUserUseCase _getUserUseCase;
        private readonly GetUserPresenter _getUserPresenter;

        public UserController(ICreateUserUseCase createUserUseCase, CreateUserPresenter createUserPresenter, IGetUserUseCase getUserUseCase, GetUserPresenter getUserPresenter)
        {
            _createUserUseCase = createUserUseCase;
            _createUserPresenter = createUserPresenter;
            _getUserUseCase = getUserUseCase;
            _getUserPresenter = getUserPresenter;
        }

        [HttpPost]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] NewUserCommand command)
        {
            if (ModelState.IsValid)
            {
                await _createUserUseCase.Handle(
                    new CreateUserRequest(command.Email, command.PasswordHash),
                    _createUserPresenter
                );

                if (_createUserPresenter.Succeeded)
                    return Created($"api/user/{_createUserPresenter.Id}", _createUserPresenter.ContentResult);
                else
                    return _createUserPresenter.ContentResult;
            }

            return BadRequest(ModelState);
        }

        [HttpGet("{id}")]
        [Authorize()]
        public async Task<IActionResult> Get(int id)
        {
            await _getUserUseCase.Handle(new GetUserRequest(id), _getUserPresenter);
            return _getUserPresenter.ContentResult;
        }
    }
}
