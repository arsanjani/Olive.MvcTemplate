using MSharp;
using Domain;

namespace Admin.Settings
{
    public class AdministratorsPage : SubPage<AdminPage>
    {
        public AdministratorsPage()
        {
            Add<Modules.AdministratorsList>();
        }
    }
}