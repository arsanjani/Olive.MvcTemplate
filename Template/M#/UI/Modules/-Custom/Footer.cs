using Domain;
using MSharp;

namespace Modules
{
    public class Footer : GenericModule
    {
        const string WEBSITE = "https://sarasari.khorasanonlin.ir/";

        public Footer()
        {
            IsInUse().IsViewComponent()
                .Using("Olive.Security")
                .RootCssClass("website-footer")
                .Markup(@"
           <div>
               [#BUTTONS(SoftwareDevelopment)#] توسط واحد نرم افزار [#BUTTONS(Geeks)#]
                &copy; @LocalTime.Now.Year تمام حقوق محفوظ است.
            </div>");



            Link("Geeks")
                .Text("روزنامه خراسان")
                .OnClick(x => x.Go(WEBSITE, OpenIn.NewBrowserWindow));

            Link("Software development")
                .Text("توسعه و پیاده سازی")
                .CssClass("plain-text")
                .OnClick(x => x.Go(WEBSITE, OpenIn.NewBrowserWindow));
        }
    }
}