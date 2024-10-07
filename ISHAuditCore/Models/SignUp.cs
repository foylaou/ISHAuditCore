using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Context;
using ISHAuditCore.Models;

namespace ISHAudit.Models
{
    public class signUpService
    {
        private readonly ISHAuditDbcontext _db;

        // 使用建構函數來注入資料庫上下文
        public signUpService(ISHAuditDbcontext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public List<Codes> GetCompanyList()
        {
            // 一次性查詢所有公司和企業資料
            var companyList = (from companyName in _db.company_names
                               join enterpriseName in _db.enterprise_names
                               on companyName.enterprise_id equals enterpriseName.id
                               select new Codes
                               {
                                   Id = companyName.id.ToString(),
                                   Name = companyName.company
                               }).ToList();

            // 一次性查詢所有工廠並關聯到公司
            var companyIds = companyList.Select(c => int.Parse(c.Id)).ToList();
            var factories = _db.factory_names
                .Where(factory => companyIds.Contains((int)factory.company_id!))
                .ToList();

            foreach (var company in companyList)
            {
                var companyId = int.Parse(company.Id);
                company.Child = factories
                    .Where(factory => factory.company_id == companyId)
                    .Select(factory => new Codes
                    {
                        Id = factory.id.ToString(),
                        Name = factory.factory!
                    }).ToList();
            }

            return companyList;
        }

        public List<Codes> GetFactoryList()
        {
            // 查詢所有工廠
            return _db.factory_names
                .Select(factory => new Codes
                {
                    Id = factory.id.ToString(),
                    Name = factory.factory!
                }).ToList();
        }

        public List<Codes> GetEnterpriseList()
        {
            // 一次性查詢所有企業和關聯的公司
            var enterpriseList = _db.enterprise_names
                .Select(enterprise => new Codes
                {
                    Id = enterprise.id.ToString(),
                    Name = enterprise.enterprise
                }).ToList();

            // 一次性查詢所有公司並關聯到企業
            var enterpriseIds = enterpriseList.Select(e => int.Parse(e.Id)).ToList();
            var companies = _db.company_names
                .Where(company => enterpriseIds.Contains((int)company.enterprise_id!))
                .ToList();

            foreach (var enterprise in enterpriseList)
            {
                var enterpriseId = int.Parse(enterprise.Id);
                enterprise.Child = companies
                    .Where(company => company.enterprise_id == enterpriseId)
                    .Select(company => new Codes
                    {
                        Id = company.id.ToString(),
                        Name = company.company!
                    }).ToList();
            }

            return enterpriseList;
        }
    }
}