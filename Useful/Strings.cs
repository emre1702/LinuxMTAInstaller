using System.Text;

/**
 * Provides useful methods for strings
 * */
namespace LinuxMTAInstaller.Strings {
	static class Strings {

		/**
		 * Get a string between two other strings
		 * 
		 * @param string str The full string
		 * @param string left The left string of both strings
		 * @param string right The right string of both strings
		 * @param string maybeotherright default="" - If we are not sure if the string in right or another string comes first and we want to use the one who comes first - usefull if we want a string until the end of the line and don't know if there is still a whitespace or \n for newline.
		 * 
		 * @return string The string between left and right (or maybeotherright, depends on what comes first)
		 * */
		public static string GetStringBetween ( string str, string left, string right, string maybeotherright = "" ) {
			int leftlen = left.Length;
			int leftindex = str.IndexOf ( left );
			int substartindex = leftindex + leftlen;
			int rightindex = str.IndexOf ( right, substartindex );
			if ( maybeotherright != "" ) {
				int mayberightindex = str.IndexOf ( maybeotherright, substartindex );
				if ( mayberightindex != -1 && mayberightindex < rightindex )
					rightindex = mayberightindex;
			}
			if ( leftindex != -1 && rightindex != -1 )
				return str.Substring ( substartindex, rightindex - substartindex );
			else
				return "";
		}


		/**
		 * static method to replace "REPLACEIT[NUMBER]" parts in a string
		 * 
		 * @param string str The original string
		 * @param string replace1 Use it if you got a REPLACEIT1 string in your file and want to replace it with your string
		 * @param string replace2 Use it if you got a REPLACEIT2 string in your file and want to replace it with your string
		 * 
		 * @return string The replaced string
		 * */
		public static string GetReplacedString ( string str, string replace1, string replace2 = "", string replace3 = "", string replace4 = "" ) {
			StringBuilder build = new StringBuilder ( str );
			build.Replace ( "REPLACEIT1", replace1 );
			if ( replace2 != "" ) {
				build.Replace ( "REPLACEIT2", replace2 );
				if ( replace3 != "" ) {
					build.Replace ( "REPLACEIT3", replace3 );
					if ( replace4 != "" )
						build.Replace ( "REPLACEIT4", replace4 );
				}
			}
			return build.ToString ();
		}
	}
}