using System.Threading.Tasks;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.CmsKit.Public.MarkedItems;

public interface IMarkedItemPublicAppService : IApplicationService
{ // qais: method names?
    Task<ListResultDto<MarkedItemWithToggleDto>> GetUserMarkedItemsAsync(string entityType);
    Task<MarkedItemWithToggleDto> GetForToggleAsync(string entityType, string entityId);
    Task<bool> IsMarkedAsync(string entityType, string entityId);
    Task<bool> ToggleAsync(string entityType, string entityId);
}