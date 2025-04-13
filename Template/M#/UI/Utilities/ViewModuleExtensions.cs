namespace Modules
{
    using MSharp;
    using Olive;
    using Olive.Entities;
    using System;
    using System.Linq.Expressions;

    public static class ViewModuleExtensions
    {
        public static ViewModule<T> AddHeader<T>(this ViewModule<T> @this, string header, bool addTotal = false, bool hasButton = false) where T : IEntity
        {
            @this.Header(@$"<div class=""module-header border-bottom d-flex justify-content-between flex-column flex-md-row"">
                                <h6 class=""m-0 p-0 col position-relative"">
                                    {header}
                                </h6>
                                {("[#BUTTONS#]".OnlyWhen(hasButton).Or(""))}
                            </div>");

            return @this;
        }

        public static ViewElement<T> DisplayMoney<T>(this ViewElement<T> @this) where T : IEntity
        {
            return @this.DisplayFormat("{0:c}");
        }

        //public static ViewElement<T> DisplayName<T>(this ViewElement<T> @this) where T : IEntity
        //    => @this.DisplayExpression($"@item.{@this.}.DisplayName");
    }
}
