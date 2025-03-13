namespace AspNetCore.SampleOpenApi.Models;

public class LegalEntity
{
    public required ICollection<Account> Balances { get; init; }
    public required ICollection<Account> Assets { get; init; }
    public required ICollection<Account> Equities { get; init; }
    public required ICollection<int> DeletedAccounts { get; init; }
}
