namespace Modules
{
    using Domain;
    using MSharp;
    using Olive;
    using Olive.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class ListModuleExtensions
    {
        public static void DefaultSearchButton<TEntity>(this ListModule<TEntity> module, string title = "Search")
            where TEntity : IEntity
        {
            module
                .SearchButton(title)
                .Icon(FA.Search)
                .IsDefault()
                .OnClick(x => x.Reload());
        }

        public static void DefaultListPagination<TEntity>(this ListModule<TEntity> module)
            where TEntity : IEntity
        {
            module.PagerPosition(PagerAt.Bottom);
            module.PageSize(20);
        }


        public static void ListSortableColumn<TEntity>(this ListModule<TEntity> module, string headerText = "Edit/Move", bool? nullCheck = null)
            where TEntity : IEntity, ISortable
        {
            module.LinkColumn(x => x.Order)
            .NoText()
            .HeaderText(headerText)
            .Icon(FA.Sort)
            .OnClick(x =>
            {
                if (nullCheck == true)
                    x.If("item == null").Return();

                x.DragSort();
            });
        }

        public static ListButton<TEntity> ListEditColumn<TEntity, TPage>(this ListModule<TEntity> module, string name = "Edit", Action<NavigateActivity> action = null, bool isModal = false, bool sendReturnUrl = true, bool sendItemId = true)
            where TEntity : IEntity
            where TPage : ApplicationPage
        {
            return module
                .ButtonColumn(name)
                .Text("[#EMPTY#]")
                .HeaderText("Edit")
                .GridColumnCssClass("actions")
                .Icon(FA.Edit)
                .OnClick(x =>
                {
                    var navigate = isModal ? x.PopUp<TPage>() : x.Go<TPage>();

                    if (sendReturnUrl)
                        navigate.SendReturnUrl();

                    if (sendItemId)
                        navigate.SendItemId();

                    action?.Invoke(navigate);
                });

        }

        public static ModuleButton ListAddButton<TEntity, TPage>(this ListModule<TEntity> module, string name, Action<NavigateActivity> action = null, bool isModal = false, bool sendReturnUrl = true, bool useNewKeyword = true, params ProjectRole[] roles)
            where TEntity : IEntity
            where TPage : ApplicationPage
        {
            var button = module
                .Button($"{"New ".OnlyWhen(useNewKeyword)}{name}")
                .Icon(FA.Plus)
                .OnClick(x =>
                {
                    var navigate = isModal ? x.PopUp<TPage>() : x.Go<TPage>();

                    if (sendReturnUrl)
                        navigate.SendReturnUrl();

                    action?.Invoke(navigate);
                });

            if (roles.HasAny())
                button = button.VisibleIf(roles);

            return button;
        }

        public static ListButton<TEntity> ListDeleteColumn<TEntity>(this ListModule<TEntity> module, string headerText = null,
            string customDelete = null,string confirmMessage = null)
            where TEntity : IEntity
        {
            return module
                .ButtonColumn("Delete")
                .NoText()
                .HeaderText(headerText.Or("Delete"))
                .GridColumnCssClass("actions")
                //.CssClass("btn-danger")
                .Icon(FA.Remove)
                .ConfirmQuestion(confirmMessage.Or("آیا برای حذف این رکورد اطمینان دارید؟"))
                .ExtraTagAttributes(@"data-confirm-ok=""تایید"", data-confirm-cancel=""انصراف""")
                .OnClick(x =>
                {
                    if (customDelete.IsEmpty())
                        x.DeleteItem();
                    else
                        x.CSharp(customDelete).UnhandledError();

                    x.Reload();
                });
        }
        public static ListButton<TEntity> ListDeleteAndRefreshColumn<TEntity>(this ListModule<TEntity> module, string headerText = null)
            where TEntity : IEntity
        {
            return module
                .ButtonColumn("Delete")
                .Text("[#EMPTY#]")
                .HeaderText(headerText.Or("Delete"))
                .GridColumnCssClass("actions")
                .CssClass("btn-danger")
                .Icon(FA.Remove)
                .ConfirmQuestion("Are you sure you want to delete this?")
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.RefreshPage();
                });
        }

        public static ViewElement<T> CommaSeperatedDisplay<T>(this ViewElement<T> viewElement, string property, string subproperty)
            where T : IEntity
        {
            return viewElement.DisplayExpression($@"@((await item.{property}).Select(x => x.{subproperty}).OrderBy(x => x).ToString("", ""))");
        }

        public static void DefaultExportButton<TEntity>(this ListModule<TEntity> module, string cssClass = "btn-secondary", string title = " to Excel")
            where TEntity : IEntity
        {
            module.Button(title)
                .Icon(FA.SignOut)
                .CssClass(cssClass)
                .OnClick(x => x.Export(ExportFormat.Excel));
        }

        public static ViewElement<T> CustomBoolColumn<T>(this ListModule<T> module, Expression<Func<T, object>> field) where T : IEntity
        {
            return module.Column(field)
                         .DisplayExpression($@"@if (item.{field.GetPropertyPath()} == true) 
                                                    {{<span style=""color:Green""><i title=""yes"" class=""fa-check fa""></i></span>}}
                                                else
                                                    {{<span style=""color:Red""><i title=""no"" class=""fas fa-times""></i></span>}}");
        }

    }
}
