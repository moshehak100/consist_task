using consist.DataTypes;
using consist.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace consist.Services
{
    public class FamilyPersonJsonConvertor : IJsonConvertor
    {
		/// <summary>
		/// in this method we get json list FamilyPerson items and convert it to FamilyPerson objects
		/// </summary>
		/// <param name="personsJsonList"></param>
		/// <returns></returns>
		public List<FamilyPerson> JsonListToObjects(string personsJsonList)
        {
			// note: this method could be implemented ass Template method to allow Generic type of conversions

			// i assumed for this task that the input json is valid with all items properties

			List<FamilyPerson> persons = new List<FamilyPerson>();

			int currJsonItemIndex = 1;
			while (currJsonItemIndex < personsJsonList.Length)
			{
				string jsonPersonItem;
				int nextItemStrIndex;
				bool itemFound = getNextJsonListItem(personsJsonList, currJsonItemIndex, out jsonPersonItem, out nextItemStrIndex);
				if (itemFound == false) // no more json items
					break;

				persons.Add(FamilyPerson.CreateObjectFromJsonItem(jsonPersonItem));

				currJsonItemIndex = nextItemStrIndex; // moving to next item location
			}

			return persons;
		}

		/// <summary>
		/// this method gets json list and index
		/// it return the next json list item from the inserted index
		/// </summary>
		/// <param name="jsonListStr"></param>
		/// <param name="indexToStartFrom"></param>
		/// <param name="jsonItemStr"></param>
		/// <param name="startIndexForNextItem"></param>
		/// <returns> true if next item exists and false otherwise</returns>
		bool getNextJsonListItem(string jsonListStr, int indexToStartFrom, out string jsonItemStr, out int startIndexForNextItem)
        {
			startIndexForNextItem = -1;
			jsonItemStr = null;

			int nextItemStartIndex = jsonListStr.IndexOf('{', indexToStartFrom);
			if (nextItemStartIndex == -1) // no more json items
				return false;

			int endOfItemIndex = jsonListStr.IndexOf('}', nextItemStartIndex);
			jsonItemStr = jsonListStr.Substring(nextItemStartIndex, endOfItemIndex - nextItemStartIndex + 1);
			startIndexForNextItem = endOfItemIndex + 1;

			return true;
		}

	}
}
