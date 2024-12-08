using GameZone.Models;


namespace GameZone.Controllers
{

    public class GamesController : Controller
    {
       
        private readonly ICateogriesService _cateogriesService;
        private readonly IGamesServices _gamesServices;
        private readonly IDevicesService _devicesService;

        public GamesController(ICateogriesService cateogriesService, IDevicesService devicesService, IGamesServices gamesServices)
        {

            _cateogriesService = cateogriesService;
            _devicesService = devicesService;
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);
            if ( game == null)
            {
                return NotFound();
            }
            return View(game);
        }
        [HttpGet]
        public IActionResult Create()

        {

            CreateGameFormViewModel viewModel = new()
            {
                Categories = _cateogriesService.GetSelectList(),
                Devices = _devicesService.GetSelectList()
            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> Create(CreateGameFormViewModel model)

        {
            if (!ModelState.IsValid)
            {
                model.Categories = _cateogriesService.GetSelectList();
                model.Devices = _devicesService.GetSelectList();
                return View(model);

            }
            await _gamesServices.Create (model);

            return RedirectToAction(nameof(Index));


        }
    }
}
