using System.ComponentModel.DataAnnotations.Schema;

namespace WorldApp.WPF.Model
{
    public class City
    {
        public int CityId { get; set; }

        public string CityName { get; set; }

        /// <summary>
        /// Foreign key to Country
        /// </summary>
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public bool IsCapital { get; set; }
        public long Population { get; set; }
    }
}