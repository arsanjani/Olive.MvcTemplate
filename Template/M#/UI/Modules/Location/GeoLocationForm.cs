using MSharp;

namespace Modules
{
    public class GeoLocationForm : FormModule<Domain.GeoLocation>
    {
        public GeoLocationForm()
        {
            DataSource("await GeoLocation.FindBySubscription(info.Subscription) ?? new  GeoLocation()");
            HeaderText("ثبت موقعیت مکانی مشترک");

            Field(x => x.Subscription)
                .AsAutoComplete()
                .ReloadOnChange()
                .MaximumOptions(10)
                .Mandatory();

            Field(x => x.Latitude)
                .Control(ControlType.Textbox)
                .Mandatory();

            Field(x => x.Longitude)
                .Control(ControlType.Textbox)
                .Mandatory();

            Button("Save")
                .Text("ذخیره")
                .IsDefault().Icon(FA.Check)
                .OnClick(x =>
                {
                    x.Javascript("getLocation();");
                    x.SaveInDatabase();
                    x.GentleMessage("اطلاعات ثبت شد");
                    x.RefreshPage();
                });

            OnJavascript("Call getLocation")
                .Code(@"$(document).ready(function () {
                            getLocation();
                        });");

            OnJavascript("Get current location")
                .Code(@"function getLocation() {
                            if (navigator.geolocation) {
                                navigator.geolocation.getCurrentPosition(
                                    (position) => {
                                        // Extract latitude and longitude from the position object
                                        const latitude = position.coords.latitude;
                                        const longitude = position.coords.longitude;
                                        console.log(""Latitude: "" + latitude + "", Longitude: "" + longitude);

                                        $('#Latitude').val(latitude);
                                        $('#Longitude').val(longitude);
                                    },
                                    (error) => {
                                        console.error(""Error getting location: "", error.message);
                                    }
                                );
                            } else {
                                console.error(""Geolocation is not supported by this browser."");
                            }
                        }");
        }
    }
}
