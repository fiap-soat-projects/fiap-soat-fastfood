using Application.Controllers.DTOs.Filters;
using Application.Controllers.DTOs.Request;
using Application.Controllers.DTOs.Response;

namespace Application.Controllers.Interfaces;

public interface IMenuController
{
    Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken);
    Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken);
    Task<MenuItemResponse> UpdateAsync(string id, UpdateMenuItemRequest input, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}
