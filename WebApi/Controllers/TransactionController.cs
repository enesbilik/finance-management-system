using Application.Features.Transaction.Commands.Create;
using Application.Features.Transaction.Commands.Delete;
using Application.Features.Transaction.Commands.Update;
using Application.Features.Transaction.Queries.GetById;
using Application.Features.Transaction.Queries.GetTransactionList;
using Core.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class TransactionController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionCommand request)
    {
        var createdTransactionResponse = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, createdTransactionResponse);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromBody] UpdateTransactionCommand request)
    {
        var updatedTransactionResponse = await Mediator.Send(request);
        return Ok(updatedTransactionResponse);
    }


    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var getTransactionListQuery = new GetTransactionListQuery();

        List<GetTransactionListItemDto> response =
            await Mediator.Send(getTransactionListQuery);

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTransactionQuery getByIdTransactionQuery = new() { Id = id };

        var response = await Mediator.Send(getByIdTransactionQuery);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] Guid id)
    {
        var deleteTransactionCommand = new DeleteTransactionCommand() { Id = id };

        await Mediator.Send(deleteTransactionCommand);

        return Ok();
    }
}