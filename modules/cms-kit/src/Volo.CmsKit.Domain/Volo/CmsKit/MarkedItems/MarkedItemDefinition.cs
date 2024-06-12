using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Localization;

namespace Volo.CmsKit.MarkedItems;

public class MarkedItemDefinition
{
    [NotNull]
    public string Name { get; }

    [CanBeNull]
    public ILocalizableString DisplayName { get; set; }

    public MarkedItemDefinition(
        [NotNull] string name,
        [CanBeNull] ILocalizableString displayName = null)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        DisplayName = displayName;
    }
}