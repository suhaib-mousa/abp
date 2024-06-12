using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.Users;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.MarkedItems;

namespace Volo.CmsKit.Public.MarkedItems;

[RequiresGlobalFeature(typeof(MarkedItemsFeature))]
public class MarkedItemPublicAppService : CmsKitPublicAppServiceBase, IMarkedItemPublicAppService
{
    public IMarkedItemDefinitionStore MarkedItemDefinitionStore { get; }

    public IUserMarkedItemRepository UserMarkedItemRepository { get; }

    public MarkedItemManager MarkedItemManager { get; }

    public MarkedItemPublicAppService(
        IMarkedItemDefinitionStore markedItemDefinitionStore,
        IUserMarkedItemRepository userMarkedItemRepository,
        MarkedItemManager markedItemManager)
    {
        MarkedItemDefinitionStore = markedItemDefinitionStore;
        UserMarkedItemRepository = userMarkedItemRepository;
        MarkedItemManager = markedItemManager;
    }

    [AllowAnonymous]
    public virtual async Task<MarkedItemWithToggleDto> GetForToggleAsync(string entityType, string entityId)
    {
        var markedItem = await MarkedItemManager.GetMarkedItemAsync(entityType);

        var userMarkedItem = CurrentUser.IsAuthenticated
            ? (await UserMarkedItemRepository
                .FindAsync(
                    CurrentUser.GetId(),
                    entityType,
                    entityId
                ))
            : null;

        return new MarkedItemWithToggleDto
        {
            MarkedItem = ConvertToMarkedItemDto(markedItem),
            IsMarkedByCurrentUser = userMarkedItem != null
        };
    }

    private MarkedItemDto ConvertToMarkedItemDto(MarkedItemDefinition markedItemDefinition)
    {
        return new MarkedItemDto
        {
            Name = markedItemDefinition.Name,
            DisplayName = markedItemDefinition.DisplayName?.Localize(StringLocalizerFactory)
        };
    }

    [Authorize] // qais: is usefull?
    public virtual async Task<ListResultDto<MarkedItemWithToggleDto>> GetUserMarkedItemsAsync(string entityType)
    {
        var markedItems = await UserMarkedItemRepository.GetListForUserAsync(
            CurrentUser.GetId(),
            entityType
            );
        return null;
        //return new ListResultDto<MarkedItemDto>()
        //{
        //    Items = ObjectMapper.Map<IReadOnlyList<UserMarkedItem>, IReadOnlyList<MarkedItemDto>>(markedItems),
        //};
    }

    [AllowAnonymous]
    public virtual async Task<bool> IsMarkedAsync(string entityType, string entityId)
    {
        if (!CurrentUser.IsAuthenticated)
        {
            return false;
        }
        return await MarkedItemManager.HasMarkedItemAsync(
            CurrentUser.GetId(), 
            entityType, 
            entityId
        );
    }

    [Authorize]
    public virtual async Task<bool> ToggleAsync(string entityType, string entityId)
    {
        return await MarkedItemManager.ToggleAsync(
            CurrentUser.GetId(),
            entityType,
            entityId
        );
    }
}
