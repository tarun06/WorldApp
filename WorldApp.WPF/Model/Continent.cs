using System.ComponentModel.DataAnnotations;

namespace WorldApp.WPF.Model
{
    public class Continent
    {
        [Key]
        public int ContinentId { get; set; }

        public string ContinentName { get; set; }
    }
}