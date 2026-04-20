using System.Globalization;

namespace Dnd_Api.Helpers
{
	public static class StringExtensions
	{
		public static string Capitalize(this string input)
		{
			if(string.IsNullOrWhiteSpace(input))
				return input;

			return char.ToUpper(input[0], CultureInfo.CurrentCulture) + input.Substring(1);
		}
	}
}
