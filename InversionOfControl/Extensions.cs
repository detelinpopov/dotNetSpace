using Microsoft.Practices.Unity;

namespace InversionOfControl
{
	public static class Extensions
	{
		public static void BindInRequestScope<T1, T2>(this IUnityContainer container) where T2 : T1
		{
			container.RegisterType<T1, T2>(new HierarchicalLifetimeManager());
		}

		public static void BindInSingletonScope<T1, T2>(this IUnityContainer container) where T2 : T1
		{
			container.RegisterType<T1, T2>(new ContainerControlledLifetimeManager());
		}
	}
}
