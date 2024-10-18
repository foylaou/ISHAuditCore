using ISHAuditCore.Context;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Migrations.Model;

namespace ISHAuditCore.Models
{
    public class Codes
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public List<Codes> Child { get; set; }
    }

    public class auth
    {
        public string user_id { get; set; }
        public string username { get; set; }
        public string nickname { get; set; }
        public string enterprise_id { get; set; }
        public string company_id { get; set; }
        public string factory_id { get; set; }
        public string factory_name { get; set; }
        public string company_name { get; set; }
    }
}