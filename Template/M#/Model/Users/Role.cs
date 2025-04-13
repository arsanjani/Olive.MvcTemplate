using MSharp;

namespace Domain
{
    public class Role : EntityType
    {
        public Role()
        {
            InstanceAccessors("Admin", "Collector", "Deliveryman");
            DefaultToString = String("Name");
            String("Display name");
        }
    }
}