using Microsoft.AspNetCore.Mvc;
using src.Application.DTOs;
using src.Application.Services;

namespace src.API.Controller
{
    [ApiController]
    [Route("/")]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("reset")]
        public IActionResult Reset()
        {
            _accountService.Reset();
            return StatusCode(200);
        }

        [HttpGet("balance")]
        public IActionResult GetBalance([FromQuery] string account_id)
        {
            var account = _accountService.GetAccount(account_id);
            if (account == null)
                return NotFound(0);

            return Ok(account.Balance);
        }

        [HttpPost("event")]
        public IActionResult HandleEvent([FromBody] RawEventRequest request)
        {
            switch (request.Type.ToLower())
            {
                case "deposit":
                    var deposited = _accountService.Deposit(request.Destination, request.Amount);
                    return Created(string.Empty, new

                    {
                        destination = new
                        {
                            id = deposited.Id,
                            balance = deposited.Balance
                        }
                    });
                case "withdraw":
                    var withdrawn = _accountService.Withdraw(request.Origin, request.Amount);
                    if (withdrawn == null)
                        return NotFound(0);

                    return Created(string.Empty, new
                    {
                        origin = new
                        {
                            id = withdrawn.Id,
                            balance = withdrawn.Balance
                        }
                    });
                case "transfer":
                    var transferResult = _accountService.Transfer(request.Origin, request.Destination, request.Amount);
                    if (transferResult == null)
                        return NotFound(0);


                    return Created(string.Empty, new
                    {
                        origin = new
                        {
                            id = transferResult.Value.Origin.Id,
                            balance = transferResult.Value.Origin.Balance
                        },
                        destination = new
                        {
                            id = transferResult.Value.Destination.Id,
                            balance = transferResult.Value.Destination.Balance
                        }
                    });

                default:
                    return BadRequest("Operação inválida");
            }
        }
    }
}
