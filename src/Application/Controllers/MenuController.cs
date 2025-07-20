using Adapter.Controllers.DTOs;
using Adapter.Controllers.DTOs.Filters;
using Adapter.Controllers.Interfaces;
using Adapter.Presenters;
using Business.Entities;
using Business.UseCases.DTOs;
using Business.UseCases.Interfaces;

namespace Adapter.Controllers;

internal class MenuController : IMenuController
{
    private readonly IMenuItemUseCase _menuItemUseCase;

    public MenuController(IMenuItemUseCase menuItemUseCase)
    {
        _menuItemUseCase = menuItemUseCase;
    }

    public async Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken)
    {
        var menuItem = new MenuItem(
            input.Name!,
            input.Price,
            input.Description!,
            input.Category);

        menuItem = await _menuItemUseCase.CreateAsync(menuItem, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemUseCase.GetByIdAsync(id, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken)
    {
        var menuFilter = new MenuItemFilter(
            filter.Name,
            filter.Category,
            filter.Skip,
            filter.Limit);

        var items = await _menuItemUseCase.GetAllAsync(menuFilter, cancellationToken);

        var output = items.Select(menuItem => CreateResponse(menuItem));

        return output;
    }

    public async Task<MenuItemResponse> UpdateAsync(string id, UpdateMenuItemRequest input, CancellationToken cancellationToken)
    {
        var menuItem = new MenuItem(
            input.Name!,
            input.Price,
            input.Description!,
            input.Category)
        {
            Id = id,
            IsActive = input.IsActive
        };

        menuItem = await _menuItemUseCase.UpdateAsync(menuItem, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _menuItemUseCase.SoftDeleteAsync(id, cancellationToken);
    }

    private static MenuItemResponse CreateResponse(MenuItem menuItem)
    {
        return new MenuItemResponse(
            menuItem.Id,
            menuItem.Name,
            menuItem.Price,
            menuItem.Category,
            menuItem.Description,
            menuItem.IsActive);
    }
}
