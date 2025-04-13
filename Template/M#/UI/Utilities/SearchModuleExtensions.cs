namespace Modules
{
    using Domain;
    using MSharp;
    using Olive;
    using Olive.Entities;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class SearchModuleExtensions
    {
        public static PropertyFilterElement<T> CollapsibleCheckBoxListPersianTitles<T>(this PropertyFilterElement<T> element) where T : IEntity
            => element.ExtraControlAttributes(@"deselectAllText=""لغو همه"", selectAllText=""انتخاب همه"", noneSelectedText=""انتخاب نشده"", noneResultsText=""اطلاعاتی یافت نشد""");

        public static PropertyFilterElement<T> DisplayName<T>(this PropertyFilterElement<T> element) where T : IEntity
            => element.DisplayExpression("item.DisplayName");

    }
}
