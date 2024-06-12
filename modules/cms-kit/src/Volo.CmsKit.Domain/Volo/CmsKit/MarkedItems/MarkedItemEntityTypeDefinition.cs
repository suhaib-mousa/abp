using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace Volo.CmsKit.MarkedItems;

public class MarkedItemEntityTypeDefinition : PolicySpecifiedDefinition
{
    [NotNull]
    public MarkedItemDefinition MarkedItem { get; }

    public MarkedItemEntityTypeDefinition(
        [NotNull] string entityType,
        [NotNull] MarkedItemDefinition markedItem,
        IEnumerable<string> createPolicies = null,
        IEnumerable<string> updatePolicies = null,
        IEnumerable<string> deletePolicies = null) : base(entityType, createPolicies, updatePolicies, deletePolicies)
    {
        MarkedItem = Check.NotNull(markedItem, nameof(markedItem));
    }
}