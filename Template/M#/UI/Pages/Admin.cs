using MSharp;
using Domain;

public class AdminPage : RootPage
{
    public AdminPage()
    {
        Roles(AppRole.Admin);

        Set(PageSettings.LeftMenu, "AdminSettingsMenu");

        Add<Modules.MainMenu>();

        //OnStart(x => x.Go<Admin.SettingsPage>().RunServerSide());
    }
}