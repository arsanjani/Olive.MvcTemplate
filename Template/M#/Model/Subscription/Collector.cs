using MSharp;

namespace Domain
{
    public class Collector : EntityType
    {
        public Collector()
        {
            Int("Collector Id").Unique();
            String("First name");
            String("Last name").Mandatory();
            String("Name").Calculated().Getter("FirstName + \" \" + LastName");
            String("Phone number");
            String("Mobile");
            String("Address");
        }
    }
}
