using Microsoft.AspNetCore.Builder;
using PersonalFinanceAssistant;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<PersonalFinanceAssistantWebTestModule>();

public partial class Program
{
}
