using PhoneNumbers;

var a = PhoneNumbers.CountryCodeToRegionCodeMap.GetCountryCodeToRegionCodeMap();
//var b = PhoneNumberUtil.GetInstance();
//var c = b.Parse("50139000993", "DK");
//var d = b.IsValidNumber(c);
//Console.WriteLine(d);
//	Console.WriteLine($"                <div class=\"item\" @onclick='()=>this.HUIA.SetPrefix(\"{g.TwoLetterISORegionName}\")' >\r\n                    <div class=\"iso3166\">{g.TwoLetterISORegionName}</div>\r\n                    <div class=\"prefix-number\">+{b.Key}</div>\r\n                    <div class=\"country\">@{g.EnglishName}</div>\r\n                </div>");

var d = PhoneNumberUtil.GetInstance();
List<(string TwoLetterISORegionName, string EnglishName, int Key)> List = new List<( string TwoLetterISORegionName, string EnglishName, int Key)>();
foreach (var b in a.Where(x=>x.Value.First()!="World"))
{

	try
	{
		var g = new System.Globalization.RegionInfo(b.Value.First());
	if(g.TwoLetterISORegionName!="001")
		List.Add(new(g.TwoLetterISORegionName, g.EnglishName, b.Key));

		
	}
	catch (Exception)
	{

	}
}
foreach (var b in List.OrderBy(x=>x.TwoLetterISORegionName)) {
	Console.WriteLine($"new(\"{b.TwoLetterISORegionName}\",{b.Key}),");

}