namespace GameZone.Models
{
    public class Cateogry : BaseEntity
    {
        public ICollection<Game> Games { get; set; } = new List<Game>();

    }
}
