using MSharp;

namespace Domain
{
    public class GeoLocation : EntityType
    {
        public GeoLocation()
        {
            Associate<Subscription>("Subscription")
                .Name("Subscription")
                .Title("کد مشترک")
                .Unique()
                .RequiredValidationMessage("فیلد کد مشترک اجباری است")
                .Mandatory();
            
            Double("Latitude")
                .Name("Latitude")
                .Title("عرض جغرافیایی")
                .Min(double.MinValue)
                .RequiredValidationMessage("فیلد عرض جغرافیایی اجباری است")
                .Scale(7)
                .Mandatory();

            Double("Longitude")
                .Name("Longitude")
                .Title("طول جغرافیایی")
                .Min(double.MinValue)
                .Scale(7)
                .RequiredValidationMessage("فیلد طول جغرافیایی اجباری است")
                .Mandatory();


        }
    }
}
