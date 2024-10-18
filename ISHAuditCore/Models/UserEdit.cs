using System;
using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Context;
using ISHAuditCore.Migrations.Model;
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
        /// <summary>
        /// 取得企業清單，其中每個企業包含公司，且每個公司包含工廠。
        /// </summary>
        /// <returns>取得企業清單，其中每個企業包含公司，且每個公司包含工廠</returns>
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
        
        public List<Codes> CompanyKpi()
        {
            var company = (from company_name in _db.company_names
                           join enterprise_name in _db.enterprise_names on company_name.enterprise_id equals enterprise_name.id
                           where enterprise_name.code == "FPG"
                           select new Codes
                           {
                               Id = company_name.id.ToString(),
                               Name = company_name.company
                           }).ToList();

            foreach (var item in company)
            {
                var Id = int.Parse(item.Id);
                var factory = (from factory_name in _db.factory_names
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
            var suggestCategory = (from q in _db.suggest_categories
                                    select new Codes
                                    {
                                        Id = q.id.ToString(),
                                        Name = q.suggest_category1
                                    }).ToList();

            foreach (var subitem in suggestCategory)
            {
                var Id = int.Parse(subitem.Id);
                var suggestType = (from q in _db.suggest_types
                                    where q.suggest_category_id == Id
                                    select new Codes
                                    {
                                        Id = q.id.ToString(),
                                        Name = q.suggest_type1
                                    }).ToList();

                foreach (var item in suggestType)
                {
                    var itemId = int.Parse(item.Id);
                    var suggestItem = (from q in _db.suggest_items
                                        where q.suggest_type_id == itemId
                                        select new Codes
                                        {
                                            Id = q.id.ToString(),
                                            Name = q.suggest_item1
                                        }).ToList();
                    item.Child = suggestItem;
                }

                subitem.Child = suggestType;
            }

            return suggestCategory;
        }

        public List<Codes> Company()
        {
            var company = (from company_name in _db.company_names
                           join enterprise_name in _db.enterprise_names on company_name.enterprise_id equals enterprise_name.id
                           select new Codes
                           {
                               Id = company_name.id.ToString(),
                               Name = company_name.company
                           }).ToList();

            foreach (var item in company)
            {
                var Id = int.Parse(item.Id);
                var factory = (from factory_name in _db.factory_names
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

        public List<factory_name> GetFactory(string id)
        {

            var iId = int.Parse(id);
            var query = from factory_name in _db.factory_names
                        where factory_name.company_id == iId
                        select factory_name;

            return query.ToList();
        }



        public List<user_info> GetUserList(string userid)
        {
            // Console.WriteLine(userid);
            var data = new List<user_info>();

            var query = from user_info in _db.user_infos
                    select user_info
                ;
            var iUserid = Convert.ToInt32(userid);
            if (!string.IsNullOrEmpty(userid))
                query = from q in query
                    where q.id != iUserid
                    select q;
            data = query.ToList();
            return data;
        }

        public List<auth> GetAuth(int userId)
        {
            var userSessionInfo = (from user in _db.user_infos // Assuming you have a Users DbSet
                where user.id == userId // Replace with the appropriate user ID
                select new auth()
                {
                    user_id = user.id.ToString(),
                    username = user.username,
                    nickname = user.nickname,
                    enterprise_id = user.enterprise_id.ToString(),
                    company_id = user.company_id.ToString(),
                    factory_id = user.factory_id.ToString(),
                    factory_name = _db.factory_names
                        .Where(f => f.id == user.factory_id)
                        .Select(f => f.factory)
                        .FirstOrDefault(),
                    company_name = _db.company_names
                        .Where(c => c.id == user.company_id)
                        .Select(c => c.company)
                        .FirstOrDefault()
                }).ToList(); // Convert the query to a list directly

            return userSessionInfo; // Return the list of auth objects
        }
        
        }
    }
