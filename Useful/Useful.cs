// used for Console //
using System;

/** 
 * Some useful methods
 * */
namespace LinuxMTAInstaller.Useful {
	static class Useful {

		/** 
		 * Writes a "title" to the console
		 * 
		 * @param string info The string we want to output to the console
		 * */
		public static void Titel ( string info ) {
			Console.WriteLine ( "" );
			Console.WriteLine ( "__________________________________" );
			Console.WriteLine ( info );
			Console.WriteLine ( "__________________________________" );
		}

	}
}
