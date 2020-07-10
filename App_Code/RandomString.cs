using System;
using System.Text;

namespace preventautopost
{
	public class RandomString
	{

		public static string GetRandomString(int length)
		{
			int intZero=0, intNine=0, intA=0, intZ=0, intCount=0, intRandomNumber=0;
			string strRandomString;
			Random rRandom =new Random(System.DateTime.Now.Millisecond);
			intZero = '0';
			intNine = '9';
			intA = 'A';
			intZ = 'Z';

			strRandomString = "";
			while (intCount <  length)
			{
				intRandomNumber = rRandom.Next(intZero, intZ);
				if(((intRandomNumber >= intZero) && (intRandomNumber <= intNine) || (intRandomNumber >= intA) && (intRandomNumber <= intZ)))
				{
					strRandomString = strRandomString + (char)intRandomNumber;
					intCount = intCount + 1;
				}
				if(strRandomString.IndexOf("0") > -1 || strRandomString.IndexOf("O") > -1 || strRandomString.IndexOf("1") > -1)
				{
					intCount = intCount - 1;
					strRandomString = strRandomString.Replace("0","");
					strRandomString = strRandomString.Replace("O","");
					strRandomString = strRandomString.Replace("1","");
				}																						 
			}
			
			return strRandomString;
		}
	}
}
