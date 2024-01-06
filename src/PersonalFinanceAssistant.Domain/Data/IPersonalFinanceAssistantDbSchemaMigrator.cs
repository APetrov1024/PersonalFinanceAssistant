using System.Threading.Tasks;

namespace PersonalFinanceAssistant.Data;

public interface IPersonalFinanceAssistantDbSchemaMigrator
{
    Task MigrateAsync();
}
