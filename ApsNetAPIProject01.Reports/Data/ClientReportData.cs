using AspNetAPIProject01.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApsNetAPIProject01.Reports.Data
{
    public class ClientReportData
    {
        public DateTime GenerationDate { get; set; }
        public List<Client> Clients { get; set; }
    }
}
