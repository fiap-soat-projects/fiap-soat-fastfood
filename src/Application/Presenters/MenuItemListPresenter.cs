using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
public class MenuItemListPresenter
{
    public IEnumerable<MenuItemResponse> ViewModel { get; init; }

    public MenuItemListPresenter(IEnumerable<MenuItem> menuItems)
    {
        ViewModel = menuItems.Select(menuItem => new MenuItemResponse(
            menuItem.Id,
            menuItem.Name,
            menuItem.Price,
            menuItem.Category,
            menuItem.Description,
            menuItem.IsActive));
    }
}
