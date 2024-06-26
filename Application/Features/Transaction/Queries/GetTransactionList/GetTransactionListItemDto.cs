namespace Application.Features.Transaction.Queries.GetTransactionList;

public class GetTransactionListItemDto
{
     public Guid Id { get; set; }
    public decimal Amount { get; set; }
    public string Category { get; set; }
    public string Description { get; set; }
    public DateTime CreatedDate { get; set; }
}