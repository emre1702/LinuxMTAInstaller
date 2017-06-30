using System.Collections.Generic;

namespace LinuxMTAInstaller.Services {
	class Service {
		private string name;

		public Service ( string name ) {
			this.name = name;
		}
	}

	static class ServicesManager {
		public static Dictionary<string, Service> services = new Dictionary<string, Service> ();

		public static void AddService ( string name ) {
			Service s = new Service ( name );
			services[name] = s;
		}

		public static bool DoesExist ( string name ) {
			return services.ContainsKey ( name );
		}

		public static Service GetService ( string name ) {
			return services[name];
		}
	}
}
