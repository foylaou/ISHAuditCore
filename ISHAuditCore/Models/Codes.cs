using ISHAuditCore.Context;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace ISHAuditCore.Models
{
    public class Codes
    {
        private readonly ISHAuditDbcontext _dbContext;

        // 透過依賴注入來處理 IConfiguration 和 DbContext
        public Codes(ISHAuditDbcontext dbContext)
        {
            _dbContext = dbContext;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public List<Codes> Child { get; set; }

        public List<Codes> CompanyKpi()
        {
            var company = (from company_name in _dbContext.company_names
                           join enterprise_name in _dbContext.enterprise_names on company_name.enterprise_id equals enterprise_name.id
                           where enterprise_name.code == "FPG"
                           select new Codes
                           {
                               Id = company_name.id.ToString(),
                               Name = company_name.company
                           }).ToList();

            foreach (var item in company)
            {
                var Id = int.Parse(item.Id);
                var factory = (from factory_name in _dbContext.factory_names
                               where factory_name.company_id == Id
                               select new Codes
                               {
                                   Id = factory_name.id.ToString(),
                                   Name = factory_name.factory
                               }).ToList();
                item.Child = factory;
            }

            return company;
        }

        public List<Codes> SuggestCategory()
        {
            var suggest_category = (from q in _dbContext.SuggestCategory
                                    select new Codes
                                    {
                                        Id = q.Id.ToString(),
                                        Name = q.SuggestCategory1
                                    }).ToList();

            foreach (var subitem in suggest_category)
            {
                var Id = int.Parse(subitem.Id);
                var suggest_type = (from q in _dbContext.SuggestType
                                    where q.SuggestCategoryId == Id
                                    select new Codes
                                    {
                                        Id = q.Id.ToString(),
                                        Name = q.SuggestType1
                                    }).ToList();

                foreach (var item in suggest_type)
                {
                    var itemId = int.Parse(item.Id);
                    var suggest_item = (from q in _dbContext.SuggestItem
                                        where q.SuggestTypeId == itemId
                                        select new Codes
                                        {
                                            Id = q.Id.ToString(),
                                            Name = q.SuggestItem1
                                        }).ToList();
                    item.Child = suggest_item;
                }

                subitem.Child = suggest_type;
            }

            return suggest_category;
        }

        public List<Codes> Company()
        {
            var company = (from company_name in _dbContext.CompanyName
                           join enterprise_name in _dbContext.EnterpriseName on company_name.EnterpriseId equals enterprise_name.Id
                           select new Codes
                           {
                               Id = company_name.Id.ToString(),
                               Name = company_name.Company
                           }).ToList();

            foreach (var item in company)
            {
                var Id = int.Parse(item.Id);
                var factory = (from factory_name in _dbContext.FactoryName
                               where factory_name.CompanyId == Id
                               select new Codes
                               {
                                   Id = factory_name.Id.ToString(),
                                   Name = factory_name.Factory
                               }).ToList();
                item.Child = factory;
            }

            return company;
        }

        public List<FactoryName> GetFactory(string id)
        {
            var iId = int.Parse(id);
            var query = from factory_name in _dbContext.FactoryName
                        where factory_name.CompanyId == iId
                        select factory_name;

            return query.ToList();
        }

        // 其他類似方法繼續按相同模式修改，移除 `using`，直接使用 `_dbContext`

        public string FactoryName(string id)
        {
            var name = "";
            if (string.IsNullOrEmpty(id)) return name;

            var iId = int.Parse(id);
            var query = from factory_name in _dbContext.FactoryName
                        where factory_name.Id == iId
                        select factory_name;

            return query.FirstOrDefault()?.Factory ?? "";
        }

        public string CompanyName(string id)
        {
            var name = "";
            if (string.IsNullOrEmpty(id)) return name;

            var iId = int.Parse(id);
            var query = from company_name in _dbContext.CompanyName
                        where company_name.Id == iId
                        select company_name;

            return query.FirstOrDefault()?.Company ?? "";
        }
    }
}