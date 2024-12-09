using GameZone.Attributes;

namespace GameZone.Models
{
    public class EditFormViewModel : GameFormViewModel
    {
        public int Id { get; set; }
        public string? CurrentCover { get; set; }

        [AllowedExtensions(FileSettings.AllowedExtensions), MaxFileSize(FileSettings.MaximumFileSizeInByte)]

        public IFormFile?  Cover { get; set; } = default!;
    }
}
