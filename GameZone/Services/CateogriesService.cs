
using Microsoft.EntityFrameworkCore;

namespace GameZone.Services
{
    public class CateogriesService : ICateogriesService
    {
        private readonly ApplicationDbContext _context;
        public CateogriesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<SelectListItem> GetSelectList()
        {
            return _context.Cateogries.Select(c => new SelectListItem
            {

                Value = c.Id.ToString(),
                Text = c.Name
            }).OrderBy(c => c.Text).AsNoTracking().ToList();
        }
    }
}
