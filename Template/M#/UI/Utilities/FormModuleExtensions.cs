namespace Modules
{
    using MSharp;
    using Olive;
    using Olive.Entities;
    using System;
    using System.Linq.Expressions;

    public static class FormModuleExtensions
    {
        public static FormModule<T> AddHeader<T>(this FormModule<T> @this, string header, bool hideHeader = false) where T : IEntity
        {
            @this.Header(@$"<div class=""module-header border-bottom d-flex align-items-center justify-content-center {"d-none".OnlyWhen(hideHeader)}"">
                                <h2 class=""m-0 p-0 col position-relative"">{header}</h2>
                            </div>");
            return @this;
        }

        public static AssociationFormElement DataQuery<TEntity>(this AssociationFormElement element, Expression<Func<IDatabaseQuery<TEntity>, IDatabaseQuery<TEntity>>> query)
            where TEntity : IEntity
        {
            var param = query.Parameters[0].Name + ".";
            var body = query.Body.ToString().ReplaceWholeWord("True", "true").ReplaceWholeWord("False", "false");

            return element.DataSource($"(await Database.Of<{typeof(TEntity).Name}>().{body.TrimStart(param)}.GetList())");
        }

        public static AssociationFormElement DisplayName(this AssociationFormElement element)
            => element.DisplayExpression("item.DisplayName").SortFormula("item.DisplayName");

        public static AssociationFormElement SortByName(this AssociationFormElement element)
           => element.SortFormula("item.Name");

        public static AssociationFormElement Sort(this AssociationFormElement element)
           => element.SortFormula("item.ToString()");


        public static AssociationFormElement RatingName(this AssociationFormElement element)
            => element.DisplayExpression("item.Rating");

        public static StringFormElement AsBarcode(this StringFormElement element)
           => element.ControlCssClass("barcode-input").AfterControlAddon("<i class=\"icon-barcode\"></i>");

        public static AssociationFormElement AddLiveSearch(this AssociationFormElement element)
           => element.ExtraControlAttributes("data-live-search=\"true\"");

        public static GenericFormElement AddLiveSearch(this GenericFormElement element)
           => element.ExtraControlAttributes("data-live-search=\"true\"");

        public static StringFormElement AddLiveSearch(this StringFormElement element)
          => element.ExtraControlAttributes("data-live-search=\"true\"");

        public static FormModule<T> AddHeader<T>(this FormModule<T> @this, string header, bool addTotal = false, bool hasButton = false) where T : IEntity
        {
            @this.Header(@$"<div class=""module-header border-bottom d-flex justify-content-between flex-column flex-md-row"">
                                <h6 class=""m-0 p-0 col position-relative"">
                                    {header}
                                </h6>
                                {("[#BUTTONS#]".OnlyWhen(hasButton).Or(""))}
                            </div>");

            return @this;
        }

        public static ModuleBox GetWrapperBox<TForm>(this FormModule<TForm> module, string cssClass)
            where TForm : IEntity
        {
            return module.Box(Guid.NewGuid().ToString(), BoxTemplate.WrapperDiv).CssClass(cssClass);
        }

        public static GenericFormElement PersianDatePicker<T>(this FormModule<T> module, Expression<Func<T, object>> field) where T : IEntity
        {
            return module.CustomField(field.GetProperty().Name)
                         .PropertyType("String")
                         .PropertyName(field.GetProperty().Name)
                         .ControlMarkup($@"
                                           <input type=""text"" asp-for=""{field.GetProperty().Name}"" class=""form-control persian-date-picker""/>
                                           <span class=""input-group-addon"">
                                              <i title=""تقویم"" class=""fa-calendar fa""></i>
                                           </span>
                                        ");
        }

        public static StringFormElement AsPersianDateTimePicker(this StringFormElement element, string maxDate = null, string minDate = null)
        {
            var atrr = "data-jdp";
            if (maxDate != null)
                atrr += $" data-jdp-max-date={maxDate}";
            if (minDate != null)
                atrr += $" data-jdp-min-date={minDate}";

            return element
                        .AfterControlAddon(@"<i title=""تقویم"" class=""fa-calendar fa""></i>")
                        .ExtraControlAttributes(atrr);
        }

        public static StringFormElement AsPersianDatePicker(this StringFormElement element, string maxDate = null, string minDate = null)
        {
            var atrr = "data-jdp data-jdp-only-date";
            if (maxDate != null)
                atrr += $" data-jdp-max-date={maxDate}";
            if (minDate != null)
                atrr += $" data-jdp-min-date={minDate}";
            return element
                        .AfterControlAddon(@"<i title=""تقویم"" class=""fa-calendar fa""></i>")
                        .ExtraControlAttributes(atrr);
            // data-jdp data-jdp-min-date="today"
            // data-jdp data-jdp-max-date="today"
            // data-jdp data-jdp-max-date="1401/10/08"

        }

        public static StringFormElement AsPersianTimePicker(this StringFormElement element)
        {
            return element
                        .AfterControlAddon(@"<i title=""تقویم"" class=""fa-clock fa""></i>")
                        .ExtraControlAttributes("data-jdp data-jdp-only-time");
        }

        public static StringFormElement DisableIf(this StringFormElement _this, string condition)
        {
            return _this.ExtraControlAttributes(@$"disabled=""@({condition} ? ""disabled"" : null)""");
        }

        public static AssociationFormElement DisableIf(this AssociationFormElement _this, string condition)
        {
            return _this.ExtraControlAttributes(@$"disabled=""@({condition} ? ""disabled"" : null)""");
        }

        public static BooleanFormElement DisableIf(this BooleanFormElement _this, string condition)
        {
            return _this.ExtraControlAttributes(@$"disabled=""@({condition} ? ""disabled"" : null)""");
        }

        public static NumberFormElement DisableIf(this NumberFormElement _this, string condition)
        {
            return _this.ExtraControlAttributes(@$"disabled=""@({condition} ? ""disabled"" : null)""");
        }

        public static BinaryFormElement DisableIf(this BinaryFormElement _this, string condition)
        {
            return _this.ExtraControlAttributes(@$"disabled=""@({condition} ? ""disabled"" : null)""");
        }
    }
}
