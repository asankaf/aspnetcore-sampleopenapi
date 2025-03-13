namespace AspNetCore.SampleOpenApi.Models;

public class Department
{
    public required Account Balance { get; init; }
    public required ICollection<Account> Assets { get; init; }
    public required Account Equity { get; init; }
}
