using System;
using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Context;
using ISHAuditCore.Models;

namespace ISHAudit.Models
{
    public class UserEditService
    {
        private readonly ISHAuditDbcontext _db;
        
        public UserEditService(ISHAuditDbcontext dbContext)
        {
            _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // 取得企業清單，其中每個企業包含公司，且每個公司包含工廠
        public List<Codes> GetEnterpriseList()
        {
            // 取得企業清單
            var enterpriseList = (from enterprise in _db.enterprise_names
                                  select new Codes
                                  {
                                      Id = enterprise.id.ToString(),
                                      Name = enterprise.enterprise,
                                      Child = (from company in _db.company_names
                                               where company.enterprise_id == enterprise.id
                                               select new Codes
                                               {
                                                   Id = company.id.ToString(),
                                                   Name = company.company,
                                                   // 查詢每個公司對應的工廠
                                                   Child = (from factory in _db.factory_names
                                                            where factory.company_id == company.id
                                                            select new Codes
                                                            {
                                                                Id = factory.id.ToString(),
                                                                Name = factory.factory
                                                            }).ToList()
                                               }).ToList()
                                  }).ToList();

            return enterpriseList;
        }
    }
}