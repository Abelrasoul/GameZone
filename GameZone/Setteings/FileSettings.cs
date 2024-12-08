namespace GameZone.Setteings
{
    public static class FileSettings
    {


        public const string ImagesPath = "/assets/images/games";
        public const string AllowedExtensions = ".jpg,.jpeg,.png";
        public const int MaximumFileSizeInMB = 1;
        public const int MaximumFileSizeInByte = MaximumFileSizeInMB * 1024 * 1024; 
    }
}
