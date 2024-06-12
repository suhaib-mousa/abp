using System.Threading.Tasks;
using JetBrains.Annotations;

namespace Volo.CmsKit.MarkedItems;

public interface IMarkedItemDefinitionStore : IEntityTypeDefinitionStore<MarkedItemEntityTypeDefinition>
{
    Task<MarkedItemDefinition> GetMarkedItemAsync([NotNull] string entityType);
}