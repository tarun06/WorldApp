using System.ComponentModel.DataAnnotations.Schema;

namespace WorldApp.WPF.Model
{
    public class Country
    {
        /// <summary>
        /// Foreign key to Continent
        /// </summary>
        [ForeignKey("ContinentId")]
        public virtual Continent Continent { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string Currency { get; set; }
        public string Language { get; set; }
    }
}