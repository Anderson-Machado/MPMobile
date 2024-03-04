using SQLite;
using System.ComponentModel.DataAnnotations;

namespace MPMobile.Entity
{
    public class OffLineEntity
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Matricula { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public int Equipamento { get; set; }
    }
}
