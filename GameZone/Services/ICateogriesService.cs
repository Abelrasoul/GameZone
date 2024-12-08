namespace GameZone.Services
{
    public interface ICateogriesService
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}
