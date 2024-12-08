



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
            _imagesPath=$"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
        }
        public Game? GetById(int id)
        {
            
            return _context.Games.
              Include(g => g.Cateogry).
              Include(g => g.Device).
              ThenInclude(d => d.Device).
              AsNoTracking().
              SingleOrDefault(g=> g.Id == id);
        }
        public IEnumerable<Game> GetAll()
        {
            return _context.Games.AsNoTracking().
                Include(g=>g.Cateogry).
                Include(g=>g.Device).
                ThenInclude(d=>d.Device).ToList();
        }
        public async Task Create(CreateGameFormViewModel model)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(model.Cover.FileName)}";
            var path = Path.Combine(_imagesPath,coverName);
            using var stream=File.Create(path);
            await model.Cover.CopyToAsync(stream);
         
            Game game = new()
            {
                Name=model.Name,
                Description=model.Description,
                CateogryId=model.CateogryId,
                Cover=coverName,
                Device=model.SelectedDevices.
                Select(d=>new GameDevice { DeviceId=d}).ToList(),

            };
            _context.Add(game);
            _context.SaveChanges();
        }

       
    }
}
