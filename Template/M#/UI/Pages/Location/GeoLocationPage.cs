using MSharp;

namespace Contact
{
    public class GeoLocationPage : SubPage<AdminPage>
    {
        public GeoLocationPage()
        {

            Layout(Layouts.AdminDefault);

            Add<Modules.GeoLocationForm>();
        }
    }

}
