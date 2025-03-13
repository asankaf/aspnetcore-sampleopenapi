using Asp.Versioning;
using AspNetCore.SampleOpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using SampleOpenApi.Controllers;

namespace AspNetCore.SampleOpenApi.Controllers;

[Route("api/v{version:apiVersion}/accounts")]
public class DepartmentsController : ApiControllerBase
{
    /// <summary>
    /// Get departments
    /// </summary>
    /// <returns>The <see cref="Department"/></returns>
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [HttpGet(Name = nameof(GetDepartments))]
    [ProducesResponseType(typeof(IEnumerable<Department>), StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public IEnumerable<Department> GetDepartments()
    {
        return Enumerable.Range(1, 5).Select(index => new Department
        {
            Balance = new Account { Id = index + 1, Name = "Balance 1" },
            Assets = [new Account { Id = index + 2, Name = "Asset 2" }],
            Equity = new Account { Id = index + 3, Name = "Equity 3" }
        });
    }
}