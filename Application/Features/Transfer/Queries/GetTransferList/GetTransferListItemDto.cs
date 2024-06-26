using Domain.Enums;

namespace Application.Features.Transfer.Queries.GetTransferList;

public class GetTransferListItemDto
{
    public Guid Id { get; set; }

    public Guid FromUserId { get; set; }
    public Guid ToUserId { get; set; }

    public decimal Amount { get; set; }
    public TransferStatus TransferStatus { get; set; }
    public DateTime CreatedDate { get; set; }
}