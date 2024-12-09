namespace GameZone.Services
{
    public interface IGamesServices
    {
        IEnumerable<Game> GetAll();
        Game? GetById(int id);
        Task Create(CreateGameFormViewModel model);
        Task<Game?> Update(EditFormViewModel model);
        bool Delete(int id);
    }
}
