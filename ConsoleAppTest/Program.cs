using PhoneNumbers;

var a = PhoneNumbers.CountryCodeToRegionCodeMap.GetCountryCodeToRegionCodeMap();
var b = PhoneNumberUtil.GetInstance();
var c = b.Parse("50139000993", "DK");
var d = b.IsValidNumber(c);
Console.WriteLine(d);