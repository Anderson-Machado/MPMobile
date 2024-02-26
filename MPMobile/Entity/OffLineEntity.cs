using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPMobile.Entity
{
    public class OffLineEntity
    {
        public Guid Id { get; set; }
        public string Matricula { get; set; }
        public DateTime Date { get; set; }
        public string Sentido {  get; set; }

    }
}
