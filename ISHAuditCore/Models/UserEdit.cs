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
        /// 獲取企業列表，其中每個企業包含其所屬的公司，
        /// 並且每個公司包含其對應的工廠。
        /// </summary>
        /// <returns>
        /// 返回一個 <see cref="List{Codes}"/>，每個元素代表一個企業，
        /// 包含該企業的公司列表，每個公司又包含其對應的工廠列表。
        /// </returns>
        /// <remarks>
        /// 該方法透過嵌套的 LINQ 查詢，從資料庫中的 `enterprise_names`、`company_names` 和
        /// `factory_names` 資料表中獲取企業、公司和工廠的資料。每個 <see cref="Codes"/> 物件都包含一個
        /// `Id` 表示其唯一識別碼、一個 `Name` 表示其名稱，以及一個 `Child` 表示其子級（公司或工廠）。
        /// </remarks>
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
        /// <summary>
        /// 獲取分類列表，包含每個建議分類及其對應的類型和項目。
        /// </summary>
        /// <returns>
        /// 返回一個 <see cref="List{Codes}"/>，每個元素代表一個建議分類，
        /// 其中包含該分類的類型列表，每個類型又包含其對應的項目列表。
        /// </returns>
        /// <remarks>
        /// 該方法透過嵌套的 LINQ 查詢，從資料庫中的 `suggest_categories`、`suggest_types` 和
        /// `suggest_items` 資料表中獲取分類、類型和項目的資料。每個 <see cref="Codes"/> 物件都包含一個
        /// `Id` 表示其唯一識別碼、一個 `Name` 表示其名稱，以及一個 `Child` 表示其子級（類型或項目）。
        /// </remarks>
        public List<Codes> GetCategoryList()
        {
             // 取得企業清單
             var categoryList = (from category in _db.suggest_categories
                 select new Codes
                 { 
                     Id = category.id.ToString(),
                     Name = category.suggest_category1,
                     Child = (from type in _db.suggest_types
                         where type.suggest_category_id == category.id
                         select new Codes
                         {
                             Id = type.id.ToString(),
                             Name = type.suggest_type1,
                             // 查詢每個公司對應的工廠
                             Child = (from item in _db.suggest_items
                                 where item.suggest_type_id == type.id
                                 select new Codes
                                 {
                                     Id = item.id.ToString(),
                                     Name = item.suggest_item1
                                 }).ToList()
                         }).ToList()
                 }).ToList();

             return categoryList;
        }
        
        /// <summary>
        /// 獲取工業區列表，其中每個城市包含其所屬的鄉鎮，並且每個鄉鎮包含其所屬的工業區。
        /// </summary>
        /// <returns>
        /// 返回一個 <see cref="List{Codes}"/>，每個元素代表一個城市，
        /// 包含該城市的鄉鎮列表，每個鄉鎮包含其對應的工業區列表。
        /// </returns>
        /// <remarks>
        /// 該方法透過嵌套的 LINQ 查詢，從資料庫中的 `city_infos`、`township_infos` 和
        /// `industrial_area_infos` 資料表中獲取城市、鄉鎮和工業區的資料。每個 <see cref="Codes"/> 物件都包含一個
        /// `Id` 表示其唯一識別碼、一個 `Name` 表示其名稱，以及一個 `Child` 表示其子級（鄉鎮或工業區）。
        /// </remarks>
        public List<Codes> GetCityList()
        {
            // 取得企業清單
            var cityList = (from city in _db.city_infos
                select new Codes
                {
                    Id = city.id.ToString(),
                    Name = city.city,
                    Child = (from township in _db.township_infos
                        where township.city_id == city.id
                        select new Codes
                        {
                            Id = township.id.ToString(),
                            Name = township.township,
                            // 查詢每個公司對應的工廠
                            Child = (from industrial in _db.industrial_area_infos
                                where industrial.township_id == township.id
                                select new Codes
                                {
                                    Id = industrial.id.ToString(),
                                    Name = industrial.industrial_area
                                }).ToList()
                        }).ToList()
                }).ToList();

            return cityList;
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

        /// <summary>
        /// 根據使用者ID，從資料庫中取得使用者的授權資訊及相關的企業、公司、工廠資料。
        /// </summary>
        /// <param name="userId">使用者的唯一識別碼 (ID)。</param>
        /// <returns>
        /// 返回一個包含使用者授權資訊的 <see cref="auth"/> 物件清單，
        /// 每個物件都包含使用者ID、用戶名、暱稱、所屬企業、公司及工廠的名稱。
        /// </returns>
        public List<auth> GetAuth(int userId)
        {
            var userSessionInfo = (from user in _db.user_infos
                where user.id == userId
                select new auth
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
                }).ToList();

            return userSessionInfo;
        }
        
        }
    }


