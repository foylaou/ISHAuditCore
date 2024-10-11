using System.Collections.Generic;
using System.Linq;
using ISHAuditCore.Context;
using ISHAuditCore.Migrations.Model;

namespace ISHAuditCore.Models;

public class UserData
{
    private readonly ISHAuditDbcontext _db;
        
    public UserData(ISHAuditDbcontext dbContext)
    {
        _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    public string UserName { get; set; }
    public string UserMail { get; set; }

    public List<user_info> DataFromDatabase()
    {

        List<user_info> dataFromDatabase;

        
        dataFromDatabase = _db.user_infos.ToList();
        
        return dataFromDatabase;
    }
}