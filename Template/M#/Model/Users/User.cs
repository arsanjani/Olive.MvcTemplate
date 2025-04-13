using MSharp;

namespace Domain
{
    public class User : EntityType
    {
        public User()
        {
            //Abstract();

            String("First name").Name("FirstName").Title("نام").Mandatory();

            String("Last name").Name("LastName").Title("نام خانوادگی").Mandatory();

            String("Name").Name("Name").Calculated().Getter("FirstName + \" \" + LastName")
                .Title("نام و نام خانوادگی");

            String("Email", 100).Mandatory().Unique().Accepts(TextPattern.EmailAddress)
                .Name("Email")
                .Title("نام کاربری");

            String("Password", 100).HashPassword().SaltProperty("Salt").Accepts(TextPattern.Password)
                .Name("Password")
                .Title("رمز عبور");

            String("Salt");

            Bool("Is active")
                .Mandatory()
                .Name("IsActive")
                .Title("فعال")
                .TrueText("بلی")
                .FalseText("خیر");

            Associate<Role>("Role")
                .Name("Role")
                .Title("گروه کاربری")
                .Mandatory();
        }
    }
}