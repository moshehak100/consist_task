using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace consist.DataTypes
{
    public class FamilyPerson
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public int? parent { get; set; }
        public List<FamilyPerson> childs { get; set; }

		public static FamilyPerson CreateObjectFromJsonItem(string jsonItemStr)
		{
			Dictionary<string, string> jsonItemParams = getParamsFromJsonItem(jsonItemStr);

			int id = int.Parse(jsonItemParams["id"]);
			string name = jsonItemParams["name"];

			int parentInt;
			int? parent = null;
			if (int.TryParse(jsonItemParams["parent"], out parentInt))
			{
				parent = parentInt;
			}

			return new FamilyPerson()
			{
				id = id,
				name = name,
				parent = parent,
				childs = new List<FamilyPerson>()
			};
		}

		private static Dictionary<string, string> getParamsFromJsonItem(string jsonItemStr)
        {
			Dictionary<string, string> itemParams = new Dictionary<string, string>();

			// remove unused json characters
			char[] unusedJsonChars = new Char[] { '{', '}', '\\', '"' };
			string cleanJsonItem = new string((from c in jsonItemStr
												where !unusedJsonChars.Contains(c)
											   select c).ToArray());

			// split json item params, after this split the keys will be in the list even location and the values in the odd
			String[] delimiters = { ":", "," };
			List<String> itemKeysAndValues = cleanJsonItem.Split(delimiters, StringSplitOptions.None).ToList();

			for (int i= 0; i < itemKeysAndValues.Count; i+=2)
            {
				string key = itemKeysAndValues[i].Trim(' '); // remove spaces
				string val = itemKeysAndValues[i + 1].Trim(' '); // remove spaces
				itemParams.Add(key, val);
			}

			return itemParams;
		}
    }
}
