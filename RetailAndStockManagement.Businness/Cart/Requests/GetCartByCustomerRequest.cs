using MediatR;

public class GetCartByCustomerRequest : IRequest<GetCartByCustomerModel>
{
    public int CustomerId { get; set; }
}