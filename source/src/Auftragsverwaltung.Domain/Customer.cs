using System;
using System.Collections.Generic;
using System.Text;

namespace Auftragsverwaltung.Domain
{
    public class Customer
    {
#nullable enable
        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public virtual Adress Adress { get; set; }
        public string Email { get; set; }
        public string? Website { get; set; }
        public  Byte[] Password { get; set; }
#nullable disable

    }
}
