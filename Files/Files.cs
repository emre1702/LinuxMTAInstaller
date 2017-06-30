using System.IO;
using System;
using System.Xml;
using System.Reflection;
using System.Text;

namespace LinuxMTAInstaller.Files {
	static class Files {

		public static void CreateFile ( string path, string content ) {
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
		}

		public static void ChangeXMLValue ( string path, string element, string newvalue ) {
			XmlDocument xmlDoc = new XmlDocument ();
			xmlDoc.Load ( path );
			XmlNode node = xmlDoc.SelectSingleNode ( "/config/" + element );
			node.InnerText = newvalue;
			xmlDoc.Save ( path );
		}

		public static void ChangeString ( string path, string oldstring, string newstring ) {
			try {
				string text = File.ReadAllText ( path );
				File.Delete ( path );
				using ( File.Create ( path ) ) { };
				File.WriteAllText ( path, text.Replace ( oldstring, newstring ) );
			} catch ( Exception e ) {
				Console.WriteLine ( "Error: " + e );
			}
		}

		public static void AppendString ( string path, string thestring ) {
			try {
				string text = File.ReadAllText ( path );
				File.Delete ( path );
				using ( File.Create ( path ) ) { };
				File.WriteAllText ( path, text + "\n" + thestring );
			} catch ( Exception e ) {
				Console.WriteLine ( "Error: " + e );
			}
		}

		public static void WriteResourceToFile ( string resourceName, string fileName, string replace1 = "", string replace2 = "", string replace3 = "" ) {
			using ( Stream resource = Assembly.GetExecutingAssembly ().GetManifestResourceStream ( resourceName ) ) {
				using ( FileStream file = new FileStream ( fileName, FileMode.Create, FileAccess.Write ) ) {
					if ( replace1 == "" )
						resource.CopyTo ( file );
					else {
						using ( StreamReader reader = new StreamReader ( resource ) ) {
							using ( StreamWriter filewriter = new StreamWriter ( file ) ) {
								StringBuilder build = new StringBuilder ( reader.ReadToEnd () );
								build.Replace ( "REPLACEIT1", replace1 );
								if ( replace2 != "" )
									build.Replace ( "REPLACEIT2", replace2 );
								if ( replace3 != "" )
									build.Replace ( "REPLACEIT3", replace3 );
								filewriter.Write ( build );

							}
						}
					}
				}
			}
		}

	}
}
