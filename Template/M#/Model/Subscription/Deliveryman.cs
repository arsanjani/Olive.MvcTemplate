using MSharp;

namespace Domain
{
    public class Deliveryman : EntityType
    {
        public Deliveryman()
        {
            Int("Deliveryman Id").Unique();
            Int("Code")
                .Name("Code")
                .Title("کد");
            String("First name");
            String("Last Name");
            String("FullName")
                .Name("FullName")
                .Title("نام و نام خوانوادگی")
                .Calculated().Getter("FirstName + \" \" + LastName");
            String("Phone")
                .Name("Phone")
                .Title("تلفن");
            String("Remarks")
                .Max(500)
                .Name("Remarks")
                .Title("توضیحات");
        }
    }
}
