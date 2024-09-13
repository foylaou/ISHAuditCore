using System;
using Newtonsoft.Json;
namespace ISHAuditCore.Models;

public class Authority
{
    
    [JsonProperty("Audit")] public string Audit { get; set; }

    [JsonProperty("KPI")] public string KPI { get; set; }

    [JsonProperty("Sys")] public string Sys { get; set; }

    [JsonProperty("Org")] public string Org { get; set; }

    private bool SessionIsNotNull(object oSession)
    {
        var bSession = false;
        try
        {
            if (!string.IsNullOrEmpty(oSession.ToString())) bSession = true;
        }
        catch
        {
        }

        return bSession;
    }

    public bool ModalAuthority(string sController, string SysAuthority, string AuditAuthority, string KpiAuthority)
    {
        if (sController.ToUpper() == "Audit".ToUpper() && AuditAuthority == "none".ToUpper())
            return false;
        if (sController.ToUpper() == "Kpi".ToUpper() && KpiAuthority == "none".ToUpper()) return false;
        return true;
    }

    public bool FunctionAuthority(string sController, string sAction, string SysAuthority, string AuditAuthority,
        string KpiAuthority, object factory_id)
    {
        if (SessionIsNotNull(factory_id))
        {
            var b = Convert.ToInt32(factory_id);
        }

        if (sController.ToUpper() == "Kpi".ToUpper())
        {
            if ((sAction.ToUpper() == "CreatePsmReport".ToUpper() || sAction.ToUpper() == "CreateEPReport".ToUpper()
                                                                  || sAction.ToUpper() == "CreateEcoReport".ToUpper() ||
                                                                  sAction.ToUpper() == "CreateReport".ToUpper())
                && KpiAuthority.ToUpper() != "power".ToUpper() && KpiAuthority.ToUpper() != "admin".ToUpper())
                return false;

            if ((sAction.ToUpper() == "CreatePsmReport".ToUpper() || sAction.ToUpper() == "CreateEPReport".ToUpper()
                                                                  || sAction.ToUpper() == "CreateEcoReport".ToUpper() ||
                                                                  sAction.ToUpper() == "CreateReport".ToUpper())
                && KpiAuthority.ToUpper() == "none".ToUpper())
            {
            }
            else if (sAction.ToUpper() == "ECO".ToUpper() && SessionIsNotNull(factory_id))
            {
                return false;
            }
        }
        else if (sController.ToUpper() == "Audit".ToUpper())
        {
            //return false;
        }

        return true;
    }

    public bool Login(object session)
    {
        if (session == null || string.IsNullOrWhiteSpace(session.ToString()))
            return false;
        return true;
    }
}