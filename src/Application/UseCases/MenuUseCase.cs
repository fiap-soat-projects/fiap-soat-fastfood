using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;
using Application.UseCases.Interfaces;
using Domain.Entities;
using Domain.Services.DTOs;
using Domain.Services.Interfaces;

namespace Application.UseCases;

internal class MenuUseCase : IMenuUseCase
{
    private readonly IMenuItemService _menuItemService;

    public MenuUseCase(IMenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    public async Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken)
    {
        var menuItem = new MenuItem(
            input.Name!,
            input.Price,
            input.Description!,
            input.Category);

        menuItem = await _menuItemService.CreateAsync(menuItem, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var menuItem = await _menuItemService.GetByIdAsync(id, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken)
    {
        var menuFilter = new MenuItemFilter(
            filter.Name,
            filter.Category,
            filter.Skip,
            filter.Limit);

        var items = await _menuItemService.GetAllAsync(menuFilter, cancellationToken);

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

        menuItem = await _menuItemService.UpdateAsync(menuItem, cancellationToken);

        return CreateResponse(menuItem);
    }

    public async Task SoftDeleteAsync(string id, CancellationToken cancellationToken)
    {
        await _menuItemService.SoftDeleteAsync(id, cancellationToken);
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
