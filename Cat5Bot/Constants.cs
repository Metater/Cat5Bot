namespace Cat5Bot; //{}

public static class Constants
{
    public const double CancellableCommandTimeout = 120;
    public const int DBSavePeriod = 30;
    
    public static class Permissions
    {
        public const byte WriteDB = 16;
        public const byte NameOther = 64;
        public const byte SetPermissionLevel = 128;
    }
}