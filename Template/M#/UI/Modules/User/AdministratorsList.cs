using Domain;
using MSharp;

namespace Modules
{
    public class AdministratorsList : ListModule<Domain.User>
    {
        public AdministratorsList()
        {
            this.AddHeader("کاربران سیستم", hasButton: true);

            ShowFooterRow()
                .ShowHeaderRow()
                .Sortable()
                .UseDatabasePaging(false)
                .PageSize(10);

            EmptyMarkup("کاربری برای نمایش وجود ندارد");

            //================ Search: ================
            var search = SearchButton("Search")
                .Icon(FA.Search)
                .Text("جستجو")
                .OnClick(x => x.Reload());

            Search(x => x.Name);
            Search(x => x.Email);
            Search(x => x.Role)
                .CollapsibleCheckBoxListPersianTitles()
                .DisplayName()
                .ShowAllRecords()
                .AsCollapsibleCheckBoxList();
            Search(x => x.IsActive);

            //================ Columns: ================

            LinkColumn(x => x.Name)
                .HeaderText("نام و نام خانوادگی")
                .OnClick(x =>
                {
                    x.Go<Admin.Settings.Administrators.ViewPage>()
                    .SendReturnUrl()
                    .Send("item", "item.ID");
                });

            Column(x => x.Email);

            Column(x => x.Role)
                .DisplayExpression("@item.Role.DisplayName");

            this.CustomBoolColumn(x => x.IsActive);
                

            ButtonColumn("Edit")
                .HeaderText("ویرایش")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .NoText()
                .OnClick(x => x.PopUp<Admin.Settings.Administrators.EnterPage>().Send("item", "item.ID"));

            this.ListDeleteColumn(headerText:"حذف");

            Button("New User")
                .Icon(FA.Plus)
                .Text("کاربر جدید")
                .OnClick(x => x.PopUp<Admin.Settings.Administrators.EnterPage>());
        }
    }
}