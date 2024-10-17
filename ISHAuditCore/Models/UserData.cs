using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Context;
using ISHAuditCore.Migrations.Model;

namespace ISHAuditCore.Models;

public class UserDataSet
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Nickname { get; set; }
    public string Authority { get; set; }
    public string EnterpriseName { get; set; }
    public int? EnterpriseId { get; set; }
    public string CompanyName { get; set; }
    public int? CompanyId { get; set; }
    public string FactoryName { get; set; }
    public int? FactoryId { get; set; }
}

public class UserData
{
    private readonly ISHAuditDbcontext _db;

    public UserData(ISHAuditDbcontext dbContext)
    {
        _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    /// <summary>
    /// 從資料庫中取得使用者資料，包括使用者所屬的企業、公司及工廠名稱。
    /// </summary>
    /// <returns>回傳一個包含使用者資料的 List，其中包括使用者的基本資訊（如名稱、權限等），以及所屬企業、公司及工廠的名稱及 ID。</returns>
    public List<UserDataSet> DataFromDatabase()
    {

        var dataFromDatabase = (from user in _db.user_infos
            join enterprise in _db.enterprise_names on user.enterprise_id equals enterprise.id into enterpriseGroup
            from enterprise in enterpriseGroup.DefaultIfEmpty() // 左聯接
            join company in _db.company_names on user.company_id equals company.id into companyGroup
            from company in companyGroup.DefaultIfEmpty() // 左聯接
            join factory in _db.factory_names on user.factory_id equals factory.id into factoryGroup
            from factory in factoryGroup.DefaultIfEmpty() // 左聯接
            select new UserDataSet
            {
                Id = user.id,
                Username = user.username,
                Nickname = user.nickname,
                Authority = user.authority,
                EnterpriseName = enterprise != null ? enterprise.enterprise : null, // 防止 null
                EnterpriseId = enterprise != null ? enterprise.id : (int?)null, 
                CompanyName = company != null ? company.company : null, // 防止 null
                CompanyId = company != null ? company.id : (int?)null, 
                FactoryName = factory != null ? factory.factory : null, // 防止 null
                FactoryId = factory != null ? factory.id : (int?)null
            }).ToList();

        return dataFromDatabase;
    }
}




//關連到名稱
// var dataFromDatabase = (from user in _userData.DataFromDatabase()
//     join enterprise in _db.enterprise_names on user.enterprise_id equals enterprise.id into enterpriseGroup
//     from enterprise in enterpriseGroup.DefaultIfEmpty() // 左聯接
//     join company in _db.company_names on user.company_id equals company.id into companyGroup
//     from company in companyGroup.DefaultIfEmpty() // 左聯接
//     join factory in _db.factory_names on user.factory_id equals factory.id into factoryGroup
//     from factory in factoryGroup.DefaultIfEmpty() // 左聯接
//     select new
//     {
//         id = user.id,
//         username = user.username,
//         nickname = user.nickname,
//         authority = user.authority,
//         EnterpriseName = enterprise != null ? enterprise.enterprise : null, // 防止 null
//         CompanyName = company != null ? company.company : null, // 防止 null
//         FactoryName = factory != null ? factory.factory : null // 防止 null
//     }).ToList();
// return Json(dataFromDatabase);