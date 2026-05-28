using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RetailAndStockManagement.Businness.Country.Requests;

namespace RetailAndStockManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CountryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CountryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCountries()
        {
            var result = await _mediator.Send(new GetAllCountriesRequest());
            return Ok(result);
        }

        [HttpGet("regions/{countryId}")]
        public async Task<IActionResult> GetRegionsByCountryId(int countryId)
        {
            var result = await _mediator.Send(new GetRegionsByCountryIdRequest { CountryId = countryId });
            return Ok(result);
        }

        [HttpGet("stores/{regionId}")]
        public async Task<IActionResult> GetStoresByRegionId(int regionId)
        {
            var result = await _mediator.Send(new GetStoresByRegionIdRequest { RegionId = regionId });
            return Ok(result);
        }
    }
}