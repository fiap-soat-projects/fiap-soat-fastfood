using Domain.Services.DTOs;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface IMenuItemMongoDbRepository
{
    Task<MenuItemMongoDb> InsertOneAsync(MenuItemMongoDb item, CancellationToken cancellationToken);
    Task<MenuItemMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<IEnumerable<MenuItemMongoDb>> GetAllAsync(MenuItemFilter filter, CancellationToken cancellationToken);
    Task UpdateAsync(string id, MenuItemMongoDb item, CancellationToken cancellationToken);
}
