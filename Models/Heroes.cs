namespace HeroesWeb.Models
{
    public class Heroes
    {
    public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Ciudad { get; set; } = string.Empty;
    public string? IdentidadSecreta { get; set; }

        public List<SuperPoderes> SuperPoderes { get; set; } = new List<SuperPoderes>();
    }
}
