using Asp.Versioning;
using AspNetCore.SampleOpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using SampleOpenApi.Controllers;

namespace AspNetCore.SampleOpenApi.Controllers;

[Route("api/v{version:apiVersion}/accounts")]
public class LegalEntitiesController : ApiControllerBase
{
    /// <summary>
    /// Get legal entities
    /// </summary>
    /// <returns>The <see cref="LegalEntity"/></returns>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [HttpGet(Name = nameof(GetLegalEntityAccounts))]
    [ProducesResponseType(typeof(IEnumerable<LegalEntity>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public IEnumerable<LegalEntity> GetLegalEntityAccounts()
    {
        return Enumerable.Range(1, 5).Select(index => new LegalEntity
        {
            Balances =
            [
                new Account { Id = index + 1, Name = "Balance 1" },
                new Account { Id = index + 2, Name = "Balance 2" }
            ],
            Assets =
            [
                new Account { Id = index + 3, Name = "Asset 1" },
                new Account { Id = index + 4, Name = "Asset 2" }
            ],
            Equities =
            [
                new Account { Id = index + 5, Name = "Equity 1" },
                new Account { Id = index + 6, Name = "Equity 2" }
            ],
            DeletedAccounts = [index + 7, index + 8]
        });
    }
}
