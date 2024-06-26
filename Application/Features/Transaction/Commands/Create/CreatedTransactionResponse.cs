using MediatR;

namespace Application.Features.Transaction.Commands.Create;

public class CreatedTransactionResponse
{
    public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
}