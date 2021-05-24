using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Auftragsverwaltung.Application.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Auftragsverwaltung.Application.Dtos
{
    public class ResponseDto<T> where T : EntityBase
    {
        public int Id { get; set; }
        public int NumberOfRows { get; set; }
        public bool Flag { get; set; }
        public string Message { get; set; }
        public T Entity { get; set; }
    }
}
