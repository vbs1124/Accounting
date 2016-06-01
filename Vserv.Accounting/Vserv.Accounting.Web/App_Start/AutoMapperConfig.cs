using Heroic.AutoMapper;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Vserv.Accounting.Web.AutoMapperConfig), "Configure")]
namespace Vserv.Accounting.Web
{
	public static class AutoMapperConfig
	{
		public static void Configure()
		{
			//NOTE: By default, the current project and all referenced projects will be scanned.
			//		You can customize this by passing in a lambda to filter the assemblies by name,
			//		like so:
			//HeroicAutoMapperConfigurator.LoadMapsFromCallerAndReferencedAssemblies(x => x.Name.StartsWith("YourPrefix"));
			HeroicAutoMapperConfigurator.LoadMapsFromCallerAndReferencedAssemblies();
			//If you run into issues with the maps not being located at runtime, try using this method instead: 
			//HeroicAutoMapperConfigurator.LoadMapsFromAssemblyContainingTypeAndReferencedAssemblies<SomeControllerOrTypeInYourWebProject>();
		}
	}
}