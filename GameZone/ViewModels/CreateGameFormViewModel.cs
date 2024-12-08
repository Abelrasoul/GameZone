using GameZone.Attributes;

namespace GameZone.ViewModels
{
    public class CreateGameFormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;
        [Display(Name= "Cateogry")]
        public int CateogryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Supported Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();
        [MaxLength(2500)]
        public string Description { get; set; } = string.Empty;
        [AllowedExtensions(FileSettings.AllowedExtensions),MaxFileSize(FileSettings.MaximumFileSizeInByte)]
        public IFormFile Cover { get; set; } = default!;
    }
}
