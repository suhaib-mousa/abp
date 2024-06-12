using System;
using JetBrains.Annotations;

namespace Volo.CmsKit.Public.MarkedItems;

[Serializable]
public class MarkedItemDto
{
    [NotNull]
    public string Name { get; set; }

    [CanBeNull]
    public string DisplayName { get; set; }
}