using Application.Controllers.DTOs.Filters;
using Application.Controllers.DTOs.Request;
using Application.Controllers.DTOs.Response;
using Application.Controllers.Interfaces;
using Domain.Entities;
using Domain.UseCases.DTOs;
using Domain.UseCases.Interfaces;

namespace Application.Controllers;

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
