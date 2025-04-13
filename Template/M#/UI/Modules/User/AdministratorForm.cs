using MSharp;
using Domain;

namespace Modules
{
    public class AdministratorForm : FormModule<Domain.User>
    {
        public AdministratorForm()
        {
            HeaderText("اطلاعات کاربر");

            Field(x => x.FirstName);
            Field(x => x.LastName);
            Field(x => x.Email);
            Field(x => x.Role).DisplayName().AsRadioButtons(Arrange.Vertical);
            Field(x => x.IsActive).AsRadioButtons(Arrange.Vertical);

            Button("Save")
                .Text("ذخیره")
                .IsDefault().Icon(FA.Check)
            .OnClick(x =>
            {
                x.SaveInDatabase();
                x.GentleMessage("اطلاعات ثبت شد");
                x.CloseModal(Refresh.Full);
            });

            Button("Cancel")
                .Text("انصراف")
                .OnClick(x => x.CloseModal());
        }
    }
}