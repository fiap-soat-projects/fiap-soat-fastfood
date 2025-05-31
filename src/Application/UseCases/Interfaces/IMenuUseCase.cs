using Application.UseCases.DTOs.Filters;
using Application.UseCases.DTOs.Request;
using Application.UseCases.DTOs.Response;

namespace Application.UseCases.Interfaces;

public interface IMenuUseCase
{
    Task<MenuItemResponse> RegisterAsync(RegisterMenuItemRequest input, CancellationToken cancellationToken);
    Task<MenuItemResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItemResponse>> GetAllAsync(MenuFilter filter, CancellationToken cancellationToken);
    Task<MenuItemResponse> UpdateAsync(string id, UpdateMenuItemRequest input, CancellationToken cancellationToken);
    Task SoftDeleteAsync(string id, CancellationToken cancellationToken);
}
