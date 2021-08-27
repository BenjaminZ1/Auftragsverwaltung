using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.WPF.Models
{
    public class PlainResponse
    {
        public int Id { get; set; }
        public int NumberOfRows { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
    }
}
