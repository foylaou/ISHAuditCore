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
        
        public List<Codes> GetCompanyList()
        {
            var company = (from companyName in _db.company_names
                                   join enterpriseName in _db.enterprise_names on companyName.enterprise_id equals enterpriseName.id
                                   select new Codes
                                   {
                                       Id = companyName.id.ToString(),
                                       Name = companyName.company,
                                   }).ToList();
            foreach (var item in company)
            {
                var id = int.Parse(item.Id);
                var factory = (from factoryName in _db.factory_names.AsEnumerable()
                                       where factoryName.company_id == id
                                       select new Codes
                                       {
                                           Id = factoryName.id.ToString(),
                                           Name = factoryName.factory
                                       }).ToList();
                item.Child = factory;
            }
            return company;
            
        }
        public List<Codes> GetFactoryList()
        {
            List<Codes> factory = (from factoryName in _db.factory_names
                                   select new Codes
                                   {
                                       Id = factoryName.id.ToString(),
                                       Name = factoryName.factory,
                                   }).ToList();
            return factory;
        }
        public List<Codes> GetEnterpriseList()
        {
            var enterprise = (from enterpriseName in _db.enterprise_names
                                      select new Codes
                                      {
                                          Id = enterpriseName.id.ToString(),
                                          Name = enterpriseName.enterprise,
                                      }).ToList();
            foreach (var item in enterprise)
            {
                var id = int.Parse(item.Id);
                var company = (from companyName in _db.company_names.AsEnumerable()
                                       where companyName.enterprise_id == id
                                       select new Codes
                                       {
                                           Id = companyName.id.ToString(),
                                           Name = companyName.company
                                       }).ToList();
                item.Child = company;
            }
            return enterprise;
            
        }
    }
}