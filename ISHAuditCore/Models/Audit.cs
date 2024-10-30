using ISHAuditCore.Context;

namespace ISHAuditCore.Models;

public class Audit
{
    private readonly ISHAuditDbcontext _db;
    
    public Audit(ISHAuditDbcontext dbContext)
    {
        _db = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }
    
    public List<Codes> AuditType()
    {
        var data = (from audit_type in _db.audit_types
            select new Codes
            {
                Id= audit_type.id.ToString(),
                Name= audit_type.audit_type1
            }).ToList();
        return data;
    }
    public List<Codes> AuditCause()
    {
        var data = (from audit_cause in _db.audit_causes
            select new Codes
            {
                Id = audit_cause.id.ToString(),
                Name = audit_cause.cause_name
            }).ToList();
        return data;
    }
    public List<Codes> DisasterType()
    {
        var data = (from disaster_type in _db.disaster_types
            select new Codes
            {
                Id = disaster_type.id.ToString(),
                Name = disaster_type.disaster
            }).ToList();
        return data;
    }
}