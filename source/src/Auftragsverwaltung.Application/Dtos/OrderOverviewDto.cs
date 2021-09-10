using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Auftragsverwaltung.Application.Dtos
{
    public class OrderOverviewDto : AppDto
    {
        public string CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNr { get; set; }
        public string ZipCode { get; set; }
        public string Town { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderId { get; set; }
        public decimal Netto { get; set; }
        public decimal Brutto { get; set; }
    }
}
