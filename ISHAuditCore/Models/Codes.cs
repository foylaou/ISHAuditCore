using ISHAuditCore.Context;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Entities;
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
    
    public class SuggestDto
    {
        public string Uuid { get; set; }
        public int? AuditBasicId { get; set; }
        public int? AuditDetailId { get; set; }
        public string EnterType { get; set; }
        public int? SuggestItemId { get; set; }
        public int? ParentId { get; set; }
        public int? ItemNo { get; set; }
        public string Suggest { get; set; }
        public string SubSuggest { get; set; }
        public string Participate { get; set; }
        public string Action { get; set; }
        public string ShortTerm { get; set; }
        public string MidTerm { get; set; }
        public string LongTerm { get; set; }
        public string HandlingSituation { get; set; }
        public string ImproveStatus { get; set; }
        public string ResponsibleUnit { get; set; }
        public string Budget { get; set; }
        public int? BudgetVal { get; set; }
        public int? ExpertId { get; set; }
        public string Remarks { get; set; }
        public SuggestType SuggestType { get; set; }
        public SuggestItem SuggestItem { get; set; }
        public SuggestCategory SuggestCategory { get; set; }
    }
}