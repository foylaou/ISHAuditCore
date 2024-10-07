using ISHAuditCore.Context;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Migrations.Model;

namespace ISHAuditCore.Models
{
    
    public class Codes
    {
        private readonly ISHAuditDbcontext _db; // 使用更具描述性的名稱
        // 無參數構造函數，供 LINQ 使用
        public Codes()
        {
        }
        public Codes(ISHAuditDbcontext dbContext) // 構造函數名稱修正
        {
            _db = dbContext;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Parent { get; set; }
        public List<Codes> Child { get; set; }

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

        // 其他類似方法繼續按相同模式修改，移除 `using`，直接使用 `_db`

        public string FactoryName(string id)
        {
            var name = "";
            if (string.IsNullOrEmpty(id)) return name;

            var iId = int.Parse(id);
            var query = from factory_name in _db.factory_names
                        where factory_name.id == iId
                        select factory_name;

            return query.FirstOrDefault()?.factory ?? "";
        }

        public string CompanyName(string id)
        {
            var name = "";
            if (string.IsNullOrEmpty(id)) return name;

            var iId = int.Parse(id);
            var query = from company_name in _db.company_names
                        where company_name.id == iId
                        select company_name;

            return query.FirstOrDefault()?.company ?? "";
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


    }
}