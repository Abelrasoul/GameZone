



using System.IO;

namespace GameZone.Services
{
    public class GamesServices : IGamesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagesPath;
        public GamesServices(ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagesPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public Game? GetById(int id)
        {

            return _context.Games.
              Include(g => g.Cateogry).
              Include(g => g.Device).
              ThenInclude(d => d.Device).
              AsNoTracking().
              SingleOrDefault(g => g.Id == id);
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.AsNoTracking().
                Include(g => g.Cateogry).
                Include(g => g.Device).
                ThenInclude(d => d.Device).ToList();
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = await SaveCover(model.Cover);


            Game game = new()
            {
                Name = model.Name,
                Description = model.Description,
                CateogryId = model.CateogryId,
                Cover = coverName,
                Device = model.SelectedDevices.
                Select(d => new GameDevice { DeviceId = d }).ToList(),

            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task<Game?> Update(EditFormViewModel model)
        {
            var game = _context.Games.Include(g => g.Device).SingleOrDefault(g => g.Id == model.Id);
            if (game == null)
            {
                return null;
            }
            var HasNewCover = model.Cover is not null;
            var OldCover = game.Cover;
            game.Name = model.Name;
            game.Description = model.Description;
            game.CateogryId = model.CateogryId;
            game.Device = model.SelectedDevices.
                Select(d => new GameDevice { DeviceId = d }).ToList();
            if (HasNewCover)
            {
                game.Cover = await SaveCover(model.Cover!);
            }

            var effectedRows = _context.SaveChanges();
            if (effectedRows > 0)
            {
                if (HasNewCover) {
                    var cover = Path.Combine(_imagesPath, OldCover);
                    File.Delete(cover);
                }
                return game;
            }
            else 
            {
                var cover = Path.Combine(_imagesPath, game.Cover );
                File.Delete(cover);
                return null; 
            
            }
        }
        public bool Delete(int id)
        {
            var isDeleted = false;
            var game = _context.Games.Find(id);
            if (game is null) {

                return isDeleted;
            }
            _context.Remove(game);
            var effectedRows=_context.SaveChanges();
            if (effectedRows > 0) {
                isDeleted = true;
                var cover = Path.Combine(_imagesPath, game.Cover);
                File.Delete(cover);
            }

            


            return isDeleted;
        }
        private async Task<string> SaveCover(IFormFile cover) 
        {   var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
        var path = Path.Combine(_imagesPath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
         return coverName;
        }

       
    }
}
