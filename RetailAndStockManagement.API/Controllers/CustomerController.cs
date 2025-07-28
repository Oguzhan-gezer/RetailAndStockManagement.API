using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.Customer.Requests;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator) => _mediator = mediator;

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateCustomerRequest command)
    {
        if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrWhiteSpace(command.Password))
        {
            return BadRequest(new { message = "Username ve Password zorunludur." });
        }

        var result = await _mediator.Send(command);

        if (!result.IsSuccess)
        {
            return BadRequest(new { message = result.Message });
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCustomerRequest request)
    {
        var result = await _mediator.Send(request);
        if (!result.IsSuccess)
            return Unauthorized(result);

        return Ok(result);
    }

    [HttpGet("stock-by-barcode/{barcode}")]
    public async Task<IActionResult> GetStockByBarcode(string barcode)
    {
        var request = new GetStoreStockByBarcodeRequest { Barcode = barcode };

        var result = await _mediator.Send(request);

        return Ok(result);
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllCustomerRequest());
        return Ok(result);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteCustomerRequest { Id = id });

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> Update([FromBody] UpdateCustomerRequest request)
    {
        var result = await _mediator.Send(request);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

}