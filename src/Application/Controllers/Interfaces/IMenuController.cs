using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Presenters;
using Adapter.Presenters.DTOs;

namespace Adapter.Controllers.Interfaces;

public interface IMenuController
{
    Task<MenuItemPresenter> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken);
    Task<MenuItemPresenter> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<MenuItemListPresenter> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken);
    Task<MenuItemPresenter> UpdateAsync(string id, UpdateMenuItemRequest input, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}
