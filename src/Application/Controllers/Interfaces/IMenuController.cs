using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Presenters;

namespace Adapter.Controllers.Interfaces;

public interface IMenuController
{
    Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken);
    Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken);
    Task<MenuItemResponse> UpdateAsync(string id, UpdateMenuItemRequest input, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}
