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
                return HttpContext.Session.GetString("UI");
            case StandardInternal.unitIdentification.storage.Type.Local:
                return Request.Cookies.Single(x => x.Key == "UI").Value;
            default:
                return "¯\\_(ツ)_/¯";
        }
    }
    [Route("websitebehind/storage/save")]
    [HttpPost]
    public bool Save(StandardInternal.websiteBehind.Data Data)
    {
        if (Data.Type is StandardInternal.unitIdentification.storage.Type.Local) {
            if (HttpContext.Session.Keys.Any(x => x == "UI")) 
                HttpContext.Session.Clear();
            Response.Cookies.Append("UI", Data.Token, new Microsoft.AspNetCore.Http.CookieOptions { Expires = DateTime.UtcNow.AddMonths(3), IsEssential = true, HttpOnly = true, SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict, Secure = true });
            return true;
        }
        if (Request.Cookies.Any(x => x.Key == "UI")) 
            Response.Cookies.Delete("UI");
        HttpContext.Session.SetString("UI", Data.Token);
        return true;
    }
    [Route("websitebehind/storage/type")]
    [HttpGet]
    public StandardInternal.unitIdentification.storage.Type Type()=> HttpContext.Session.Keys.Any(x => x == "UI") ? StandardInternal.unitIdentification.storage.Type.Temporarily : Request.Cookies.Any(x => x.Key == "UI") ? StandardInternal.unitIdentification.storage.Type.Local : StandardInternal.unitIdentification.storage.Type.None;
}