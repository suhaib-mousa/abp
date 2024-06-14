﻿using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AutoMapper;
using Volo.Abp.Http.ProxyScripting.Generators.JQuery;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;
using Volo.CmsKit.Reactions;
using Volo.CmsKit.Web.Icons;
using Markdig;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.SettingManagement.Web;
using Volo.CmsKit.MarkedItems;

namespace Volo.CmsKit.Web;

[DependsOn(
    typeof(AbpAspNetCoreMvcUiThemeSharedModule),
    typeof(CmsKitCommonApplicationContractsModule),
    typeof(AbpAutoMapperModule),
    typeof(AbpSettingManagementWebModule)
    )]
public class CmsKitCommonWebModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<CmsKitUiOptions>(options =>
        {
            // Reaction Icons
            options.ReactionIcons[StandardReactions.Smile] = new LocalizableIconDictionary("fas fa-smile text-warning");
            options.ReactionIcons[StandardReactions.ThumbsUp] = new LocalizableIconDictionary("fa fa-thumbs-up text-primary");
            options.ReactionIcons[StandardReactions.Confused] = new LocalizableIconDictionary("fas fa-surprise text-warning");
            options.ReactionIcons[StandardReactions.Eyes] = new LocalizableIconDictionary("fas fa-meh-rolling-eyes text-warning");
            options.ReactionIcons[StandardReactions.Heart] = new LocalizableIconDictionary("fa fa-heart text-danger");
            options.ReactionIcons[StandardReactions.HeartBroken] = new LocalizableIconDictionary("fas fa-heart-broken text-danger");
            options.ReactionIcons[StandardReactions.Wink] = new LocalizableIconDictionary("fas fa-grin-wink text-warning");
            options.ReactionIcons[StandardReactions.Pray] = new LocalizableIconDictionary("fas fa-praying-hands text-info");
            options.ReactionIcons[StandardReactions.Rocket] = new LocalizableIconDictionary("fa fa-rocket text-success");
            options.ReactionIcons[StandardReactions.ThumbsDown] = new LocalizableIconDictionary("fa fa-thumbs-down text-secondary");
            options.ReactionIcons[StandardReactions.Victory] = new LocalizableIconDictionary("fas fa-hand-peace text-warning");
            options.ReactionIcons[StandardReactions.Rock] = new LocalizableIconDictionary("fas fa-hand-rock text-warning");

            // MarkedItem Icons
            options.MarkedItemIcons[StandardMarkedItems.Favorite] = new LocalizableIconDictionary("fa fa-heart text-danger");
            options.MarkedItemIcons[StandardMarkedItems.Flagged] = new LocalizableIconDictionary("fa fa-flag text-info");
            options.MarkedItemIcons[StandardMarkedItems.Bookmark] = new LocalizableIconDictionary("fa fa-bookmark text-primary");
            options.MarkedItemIcons[StandardMarkedItems.Starred] = new LocalizableIconDictionary("fa fa-star text-warning");

        });
        
        context.Services
                    .AddSingleton(_ => new MarkdownPipelineBuilder()
                        .UseAutoLinks()
                        .UseBootstrap()
                        .UseGridTables()
                        .UsePipeTables()
                        .Build());

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<CmsKitCommonWebModule>();
        });

        Configure<DynamicJavaScriptProxyOptions>(options =>
        {
            options.DisableModule(CmsKitCommonRemoteServiceConsts.ModuleName);
        });
    }
}

