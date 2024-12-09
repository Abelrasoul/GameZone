using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel :GameFormViewModel
    {

        [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaximumFileSizeInByte)]

        public IFormFile Cover { get; set; } = default!;
    }
}
