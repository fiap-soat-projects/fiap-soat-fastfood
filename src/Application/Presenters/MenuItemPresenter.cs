using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
public class MenuItemPresenter
{
    public MenuItemResponse ViewModel { get; init; }

    public MenuItemPresenter(MenuItem menuItem)
    {
        ViewModel = new MenuItemResponse(
            menuItem.Id,
            menuItem.Name,
            menuItem.Price,
            menuItem.Category,
            menuItem.Description,
            menuItem.IsActive);
    }
}
