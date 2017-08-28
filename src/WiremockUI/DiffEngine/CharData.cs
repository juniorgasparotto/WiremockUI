using System;
using System.Collections;

namespace WiremockUI
{
	public class CharDataDiff : IDiffList
	{
		private char[] _charList;

		public CharDataDiff(string charData)
		{
			_charList = charData.ToCharArray();
		}

		#region IDiffList Members

		public int Count()
		{
			return _charList.Length;
		}

		public IComparable GetByIndex(int index)
		{
			return _charList[index];
		}

		#endregion
	}
}