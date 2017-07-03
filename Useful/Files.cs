using System.IO;
using System;
using System.Xml;
using System.Reflection;
using System.Text;

/**
 * Provides some useful file-methods 
 * */

namespace LinuxMTAInstaller.Files {
	static class Files {

		/*public static void CreateFile ( string path, string content ) {
			using ( StreamWriter file = File.CreateText ( path ) ) {
				file.WriteLine ( content );
			}
		}

		public static bool WriteInFileBeforeLine ( string path, string writethat, string beforethat ) {
			try {
				string[] lines = File.ReadAllLines ( path );
				File.Delete ( path );
				using ( File.Create ( path ) ) { };
				bool done = false;
				using ( StreamWriter writer = new StreamWriter ( path ) ) {
					for ( int i = 0; i < lines.Length; i++ ) {
						if ( lines[i].Contains ( writethat ) ) {
							done = true;
						} else if ( lines[i].Contains ( beforethat ) && !done ) {
							writer.WriteLine ( writethat );
							writer.WriteLine ( lines[i] );
							done = true;
						} else
							writer.WriteLine ( lines[i] );
					}
				}
				return done;
			} catch ( Exception e ) {
				Console.WriteLine ( "Error: " + e );
				return false;
			}
		}*/


		/**
		 * Changes value of XML
		 * 
		 * @param string path Path of the XML file
		 * @param string element Element which value we want to change
		 * @param string newvalue The new value for the Element
		 * */
		public static void ChangeXMLValue ( string path, string element, string newvalue ) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load ( path );
			XmlNode node = xmlDoc.SelectSingleNode ( "/config/" + element );
			node.InnerText = newvalue;
			xmlDoc.Save ( path );
		}

		/**
		 * Replaces a string in a file 
		 * 
		 * @param string path Path of the file
		 * @param string oldstring The string we want to replace
		 * @param string newstring The string we want to have instead of the oldstring
		 * */
		public static void ChangeString ( string path, string oldstring, string newstring ) {
			try {
				string text = File.ReadAllText ( path );
				File.Delete ( path );
				// because File.Create opens the file, but we needed it to be closed //
				using ( File.Create ( path ) ) { };
				File.WriteAllText ( path, text.Replace ( oldstring, newstring ) );
			} catch ( Exception e ) {
				Console.WriteLine ( "Error: " + e );
			}
		}


		/*public static void AppendString ( string path, string thestring ) {
			try {
				string text = File.ReadAllText ( path );
				File.Delete ( path );
				using ( File.Create ( path ) ) { };
				File.WriteAllText ( path, text + "\n" + thestring );
			} catch ( Exception e ) {
				Console.WriteLine ( "Error: " + e );
			}
		}*/

		/**
		 * static method to write a embedded resource to a file
		 * That way we can make the programm "standalone" and still can use the files we need
		 * 
		 * @param string resourceName The name of the resource we want to create
		 * @param string filename The new file with the content of the resource
		 * @param string replace1 Use it if you got a REPLACEIT1 string in your file and want to replace it with your string
		 * @param string replace2 Use it if you got a REPLACEIT2 string in your file and want to replace it with your string
		 * ...
		 * */
		public static void WriteResourceToFile ( string resourceName, string fileName, string replace1 = "", string replace2 = "", string replace3 = "", string replace4 = "" ) {
			using ( Stream resource = Assembly.GetExecutingAssembly ().GetManifestResourceStream ( resourceName ) ) {
				using ( FileStream file = new FileStream ( fileName, FileMode.Create, FileAccess.Write ) ) {
					if ( replace1 == "" )
						resource.CopyTo ( file );
					else {
						using ( StreamReader reader = new StreamReader ( resource ) ) {
							using ( StreamWriter filewriter = new StreamWriter ( file ) ) {
								string str = Strings.Strings.GetReplacedString ( reader.ReadToEnd (), replace1, replace2, replace3, replace4 );
								filewriter.Write ( str );
							}
						}
					}
				}
			}
		}

	}
}
