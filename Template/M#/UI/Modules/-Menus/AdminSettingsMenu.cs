using MSharp;
using Domain;
using Contact;

namespace Modules
{
    public class AdminSettingsMenu : MenuModule
    {
        public AdminSettingsMenu()
        {
            SubItemBehaviour(MenuSubItemBehaviour.ExpandCollapse);
            WrapInForm(false);
            AjaxRedirect();
            IsViewComponent();
            RootCssClass("sidebar-menu");
            UlCssClass("nav flex-column");
            Using("Olive.Security");

            Item("GeoLocation")
                .Icon(FA.SearchLocation)
                .Text("ثبت موقعیت مکانی")
                .OnClick(x => x.Go<GeoLocationPage>());

            Item("Administrators")
                .Icon(FA.Route)
                .Text("مسیریابی توزیع")
                .OnClick(x => x.GentleMessage("بزودی راه اندازی خواهد شد"));

            Item("General Settings")
                .Icon(FA.Users)
                .Text("کاربران")
                .OnClick(x => x.Go<Admin.Settings.AdministratorsPage>());

            Item("Delivery men")
                .Icon(FA.Envelope)
                .Text("موزعین")
                .OnClick(x => x.Go<Admin.Settings.GeneralPage>());

            Item("Collectors")
                .Icon(FA.Ghost)
                .Text("ماموران سجاب")
                .OnClick(x => x.Go<Admin.Settings.GeneralPage>());

            Item("Subscriptions")
                .Icon(FA.Dollar)
                .Text("مشترکین فعال")
                .OnClick(x => x.Go<Admin.Settings.GeneralPage>());

        }
    }
}