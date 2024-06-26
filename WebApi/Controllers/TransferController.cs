using Application.Features.Transaction.Commands.Create;
using Application.Features.Transaction.Commands.Delete;
using Application.Features.Transaction.Commands.Update;
using Application.Features.Transaction.Queries.GetById;
using Application.Features.Transaction.Queries.GetTransactionList;
using Application.Features.Transfer.Commands.Create;
using Application.Features.Transfer.Queries.GetById;
using Application.Features.Transfer.Queries.GetTransferList;
using Core.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class TransferController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransferCommand request)
    {
        var createdTransferResponse = await Mediator.Send(request);
        return StatusCode(StatusCodes.Status201Created, createdTransferResponse);
    }


    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var getTransferListQuery = new GetTransferListQuery();

        List<GetTransferListItemDto> response =
            await Mediator.Send(getTransferListQuery);

        return Ok(response);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTransferQuery getByIdTransferQuery = new() { Id = id };

        var response = await Mediator.Send(getByIdTransferQuery);

        return Ok(response);
    }
}