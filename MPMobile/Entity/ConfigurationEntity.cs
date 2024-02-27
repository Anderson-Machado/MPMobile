

using SQLite;

namespace MPMobile.Entity
{
    public class ConfigurationEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public int Equipamento { get; set; }
        public string UrlBase { get; set; }
    }
}
