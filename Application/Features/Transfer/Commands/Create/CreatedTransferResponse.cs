using Domain.Enums;

namespace Application.Features.Transfer.Commands.Create;

public class CreatedTransferResponse
{
    public Guid FromUserId { get; set; }

    public Guid ToUserId { get; set; }

    public decimal Amount { get; set; }

    public TransferStatus TransferStatus { get; set; }
}