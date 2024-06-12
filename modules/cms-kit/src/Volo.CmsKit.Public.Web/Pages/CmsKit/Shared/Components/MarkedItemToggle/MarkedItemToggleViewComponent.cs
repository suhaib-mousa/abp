using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI;
using Volo.Abp.AspNetCore.Mvc.UI.Widgets;
using Volo.CmsKit.Public.MarkedItems;
using Volo.CmsKit.Web;

namespace Volo.CmsKit.Public.Web.Pages.CmsKit.Shared.Components.MarkedItemToggle;

[ViewComponent(Name = "CmsMarkedItemToggle")]
[Widget(
    ScriptTypes = new[] { typeof(MarkedItemToggleScriptBundleContributor) },
    StyleTypes = new[] { typeof(MarkedItemToggleStyleBundleContributor) },
    RefreshUrl = "/CmsKitPublicWidgets/MarkedItem",
    AutoInitialize = true
)]
public class MarkedItemToggleViewComponent : AbpViewComponent
{
    protected IMarkedItemPublicAppService MarkedItemPublicAppService { get; set; }

    protected CmsKitUiOptions Options { get; }

    public AbpMvcUiOptions AbpMvcUiOptions { get; }

    public MarkedItemToggleViewComponent(
        IMarkedItemPublicAppService markedItemPublicAppService,
        IOptions<CmsKitUiOptions> options,
        IOptions<AbpMvcUiOptions> abpMvcUiOptions)
    {
        MarkedItemPublicAppService = markedItemPublicAppService;
        Options = options.Value;
        AbpMvcUiOptions = abpMvcUiOptions.Value;
    }

    public virtual async Task<IViewComponentResult> InvokeAsync(
        string entityType,
        string entityId)
    {
        var result = await MarkedItemPublicAppService.GetForToggleAsync(entityType, entityId);

        var loginUrl =
            $"{AbpMvcUiOptions.LoginUrl}?returnUrl={HttpContext.Request.Path.ToString()}&returnUrlHash=#cms-markedItem_{entityType}_{entityId}";

        var viewModel = new MarkedItemToggleViewModel
        {
            EntityType = entityType,
            EntityId = entityId,
            MarkedItem = new MarkedItemViewModel()
            {
                Icon = Options.MarkedItemIcons.GetLocalizedIcon(result.MarkedItem.Name),
                IsMarkedByCurrentUser = result.IsMarkedByCurrentUser
            },
            LoginUrl = loginUrl
        };

        return View("~/Pages/CmsKit/Shared/Components/MarkedItemToggle/Default.cshtml", viewModel);
    }
    public class MarkedItemToggleViewModel 
    {
        public string EntityType { get; set; }

        public string EntityId { get; set; }

        public MarkedItemViewModel MarkedItem { get; set; }

        public string LoginUrl { get; set; }
    }

    public class MarkedItemViewModel
    {
        [NotNull]
        public string Icon { get; set; }

        public bool IsMarkedByCurrentUser { get; set; }
    }
}
