using API.Commands;
using API.Commands.Auth;
using API.Presenters;
using API.Presenters.Auth;
using Core.Abstraction.UseCases;
using Core.Abstraction.UseCases.Auth;
using Core.DTO.Requests.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly ILoginUseCase _loginUseCase;
        private readonly LoginPresenter _loginPresenter;

        public AuthController(ILoginUseCase loginUseCase, LoginPresenter loginPresenter)
        {
            this._loginUseCase = loginUseCase;
            this._loginPresenter = loginPresenter;
        }

        [HttpPost]
        [AllowAnonymous]
        [Produces("application/json")]
        public async Task<IActionResult> Post([FromBody] LoginCommand command)
        {
            if (ModelState.IsValid)
            {
                await _loginUseCase.Handle(
                    new LoginRequest(command.Email, command.PasswordHash),
                    _loginPresenter
                );

                return _loginPresenter.ContentResult;
            }

            return BadRequest(ModelState);
        }
    }
}
