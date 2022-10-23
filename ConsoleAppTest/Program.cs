using PhoneNumbers;

var a = PhoneNumbers.CountryCodeToRegionCodeMap.GetCountryCodeToRegionCodeMap();
//var b = PhoneNumberUtil.GetInstance();
//var c = b.Parse("50139000993", "DK");
//var d = b.IsValidNumber(c);
//Console.WriteLine(d);
var d = PhoneNumberUtil.GetInstance();
foreach (var b in a.Where(x=>x.Value.First()!="World"))
{
	try
	{
		var g = new System.Globalization.RegionInfo(b.Value.First());
		
		Console.WriteLine($"                <div class=\"item\">\r\n                    <div class=\"iso6311\">{g.TwoLetterISORegionName}</div>\r\n                    <div class=\"prefix-number\">+{b.Key}</div>\r\n                    <div class=\"country\">{g.EnglishName}</div>\r\n                </div>");
	}
	catch (Exception)
	{

	}
}