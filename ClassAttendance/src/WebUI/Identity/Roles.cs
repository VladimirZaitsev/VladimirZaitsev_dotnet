namespace WebUI.Identity
{
    public static class Roles
    {
        public const string User = "User";

        public const string Manager = "Manager";

        // As i'm passing this values to attribute, i can't use
        // something like string.Format or interpolated string
        public const string UserAndManager = "User,Manager";
    }
}
