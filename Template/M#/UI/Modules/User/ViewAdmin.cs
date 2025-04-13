using MSharp;
using Domain;

namespace Modules
{
    public class ViewAdmin : ViewModule<Domain.User>
    {
        public ViewAdmin()
        {
            HideEmptyElements().HeaderText("@item Details");

            Field(x => x.Name);
            Field(x => x.Email);
            Field(x => x.IsActive).DisplayExpression("c#:item.IsActive ? \"بلی\" : \"خیر\"");

            Button("Back")
                .Icon(FA.ChevronLeft)
                .OnClick(x => x.ReturnToPreviousPage());

            Button("Delete")
                .Icon(FA.Remove)
                .OnClick(x =>
                {
                    x.DeleteItem();
                    x.ReturnToPreviousPage();
                });
        }
    }
}