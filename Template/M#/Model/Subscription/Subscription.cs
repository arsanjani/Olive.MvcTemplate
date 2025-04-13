using MSharp;

namespace Domain
{
    public class Subscription : EntityType
    {
        public Subscription()
        {
            Int("Subscription Id").Unique().Mandatory();
            Int("Code").Mandatory();
            Associate<Collector>("Collector");
            Associate<Deliveryman>("Deliveryman");
            Associate<Deliveryman>("Deliveryman2");
            String("Title")
                .Name("Title")
                .Title("نام مشترک");
                
            String("Phone1")
                .Name("Phone1")
                .Title("تلفن");
            String("Phone2")
                .Name("Phone2")
                .Title("تلفن");
            String("Mobile")
                .Name("Mobile")
                .Title("موبایل");
            String("Address")
                .Name("Address")
                .Title("آدرس");
            String("Remarks")
                .Name("Remarks")
                .Title("توضیحات");
            Int("Count").Mandatory();
            Int("Status").Mandatory();
            Int("Region Code");

            ToStringExpression(@"Code + "" - "" + Title");
        }
    }
}
