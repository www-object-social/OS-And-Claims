using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebsiteBehind;
[ApiController]
public class Storage: ControllerBase
{
    [Route("websitebehind/storage/read")]
    [HttpGet]
    public string Read()
    {
        switch (Type()) {
            case StandardInternal.unitIdentification.storage.Type.Temporarily:
                return HttpContext.Session.GetString("SUI");
            case StandardInternal.unitIdentification.storage.Type.Local:
                return Request.Cookies.Single(x => x.Key == "CUI").Value;
            default:
                return "¯\\_(ツ)_/¯";
        }
    }
    [Route("websitebehind/storage/save")]
    [HttpPost]
    public bool Save(StandardInternal.websiteBehind.Data Data)
    {
        if (Data.Type is StandardInternal.unitIdentification.storage.Type.Local) {
            Response.Cookies.Append("CUI", Data.Token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddMonths(3), IsEssential = true, HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax, Secure = true });
			if (HttpContext.Session.Keys.Any(x => x == "SUI"))
			{
				HttpContext.Session.Remove("SUI");
				HttpContext.Session.Clear();
			}
			return true;
        }
        HttpContext.Session.SetString("SUI", Data.Token);
		if (Request.Cookies.Any(x => x.Key == "CUI"))
			Response.Cookies.Delete("CUI");
		return true;
    }
    [Route("websitebehind/storage/type")]
    [HttpGet]
    public StandardInternal.unitIdentification.storage.Type Type()=> HttpContext.Session.Keys.Any(x => x == "SUI") ? StandardInternal.unitIdentification.storage.Type.Temporarily : Request.Cookies.Any(x => x.Key == "CUI") ? StandardInternal.unitIdentification.storage.Type.Local : StandardInternal.unitIdentification.storage.Type.None;
}