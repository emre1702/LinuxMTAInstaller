/**
 * Used to provide multiple languages 
 * */
static partial class Languages { 
	public static string lang = "English";

	private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> languages = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.Dictionary<string, string>> {
		{ "German", german },
		{ "English", english }
	};

	/**
	 * Returns the string for the language
	 * 
	 * @param string index The index for the string
	 * 
	 * @return string String for the language
	 * */
	public static string GetLang ( string index ) {
		return languages[lang][index];
	}
}
