using MediatR;
using Microsoft.EntityFrameworkCore;
using RetailAndStockManagement.Businness.User.Requests;
using RetailAndStockManagement.Data.EF;

namespace RetailAndStockManagement.Businness.User.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserRequest, bool>
{
    private readonly RetailAndStockManagementContext _context;

    public DeleteUserHandler(RetailAndStockManagementContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object[] { request.Id }, cancellationToken);
        if (user == null) return false;

        user.IsActive = false;
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}
