using System.Globalization;
using ISHAuditCore.Context;
using ISHAuditCore.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ISHAuditCore.Controllers;

public class AuditController : baseController
{
    private readonly UserEditService _userEditService;
    private readonly Audit _audit;
    private readonly ISHAuditDbcontext _db;


    public AuditController(ISHAuditDbcontext dbContext, Authority authorityClass,Audit audit, UserEditService userEditService)
        : base(dbContext, authorityClass,userEditService) // 呼叫 baseController 的構造函數
    {
        _db = dbContext;
        _userEditService = userEditService ?? throw new ArgumentNullException(nameof(userEditService));
        _audit = audit ?? throw new ArgumentNullException(nameof(audit));
    }
    // GET
    public IActionResult Index()
    {
        var cityList = _userEditService.GetCityList();
        ViewBag.CityListJson = JsonConvert.SerializeObject(cityList);
        var company = _userEditService.Company();
        ViewBag.CompanyJson = JsonConvert.SerializeObject(company);
        var auditType = _audit.AuditType();
        ViewBag.AuditTypeJson = JsonConvert.SerializeObject(auditType);
        var auditCause = _audit.AuditCause();
        ViewBag.AuditCauseJson = JsonConvert.SerializeObject(auditCause);
        var disasterType = _audit.DisasterType();
        ViewBag.DisasterTypeJson = JsonConvert.SerializeObject(disasterType);
        var categoryList = _userEditService.GetCategoryList();
        ViewBag.CategoryListJson = JsonConvert.SerializeObject(categoryList);
        
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<string> Query(IFormCollection post)
    {

        var basicData = from basic in _db.audit_basic_infos
            from audit_detail_info in _db.audit_detail_infos
                .Where(o => o.audit_basic_id == basic.id)
                .OrderBy(o => o.audit_start_date).Take(1)
            join audit_type in _db.audit_types on basic.audit_type_id equals audit_type.id into audit_type
            from H in audit_type.DefaultIfEmpty()
            join factory_name in _db.factory_names on basic.factory_id equals factory_name.id into factory
            from C in factory.DefaultIfEmpty()
            join audit_cause in _db.audit_causes on basic.audit_cause_id equals audit_cause.id into audit_cause
            from auditCause in audit_cause.DefaultIfEmpty()
            join company_name in _db.company_names on C.company_id equals company_name.id into company
            from D in company.DefaultIfEmpty()
            join industrial_area_info in _db.industrial_area_infos on C.industrial_area_id equals
                industrial_area_info.id into industrial_area_info
            from E in industrial_area_info.DefaultIfEmpty()
            join township_info in _db.township_infos on E.township_id equals township_info.id into township_info
            from F in township_info.DefaultIfEmpty()
            select new
            {
                id = basic.id,
                uuid = basic.uuid,
                audit_cause_id = basic.audit_cause_id,
                audit_cause = auditCause == null ? string.Empty : auditCause.cause_name,
                audit_start_date = audit_detail_info.audit_start_date,
                factory = C.factory ?? string.Empty,
                company = D.company ?? string.Empty,
                industrial_area = E.industrial_area ?? string.Empty,
                audit_type = H.audit_type1 ?? string.Empty,
                city_id = F.city_id ?? 0,
                township_id = E.township_id ?? 0,
                industrial_area_id = C.industrial_area_id ?? 0,
                company_id = C.company_id ?? 0,
                factory_id = basic.factory_id ?? 0,
                audit_type_id = basic.audit_type_id ?? 0,
                disater_types_id = basic.disater_types_id ?? string.Empty,
                disater_types = basic.disater_types ?? string.Empty,
                incident_datetime = basic.incident_datetime,
                incident_description = basic.incident_description ?? string.Empty,
                sd = basic.sd ?? string.Empty,
                penalty = basic.penalty ?? string.Empty,
                penalty_detail = basic.penalty_detail ?? string.Empty,
                situation = basic.situation ?? string.Empty,
                improve_status_val = basic.improve_status_val ?? 0
            };
        if (!string.IsNullOrEmpty(post["city"]))
        {
            int city_id = Convert.ToInt32(post["city"]);
            basicData = basicData.Where(s => s.city_id == city_id);
        }

        if (!string.IsNullOrEmpty(post["audit_cause"]))
        {
            int audit_cause_id = Convert.ToInt32(post["audit_cause"]);
            basicData = from s in basicData
                where s.audit_cause_id == audit_cause_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["township"]))
        {
            int township_id = Convert.ToInt32(post["township"]);
            basicData = from s in basicData
                where s.township_id == township_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["industrial_area"]))
        {
            int industrial_area_id = Convert.ToInt32(post["industrial_area"]);
            basicData = from s in basicData
                where s.industrial_area_id == industrial_area_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["company"]))
        {
            int company_id = Convert.ToInt32(post["company"]);
            basicData = from s in basicData
                where s.company_id == company_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["factory"]))
        {
            int factory_id = Convert.ToInt32(post["factory"]);
            basicData = from s in basicData
                where s.factory_id == factory_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["audit_type"]))
        {
            int audit_type_id = Convert.ToInt32(post["audit_type"]);
            basicData = from s in basicData
                where s.audit_type_id == audit_type_id
                select s;
        }

        if (!string.IsNullOrEmpty(post["shut_down"]))
        {
            string shut_down = post["shut_down"];
            basicData = from s in basicData
                where s.sd == shut_down
                select s;
        }

        if (!string.IsNullOrEmpty(post["penalty"]))
        {
            string penalty = post["penalty"];
            basicData = from s in basicData
                where s.penalty == penalty
                select s;
        }

        if (!string.IsNullOrEmpty(post["disaster_type"]))
        {
            var condition = post["disaster_type"].ToString().Split(',');
            for (int i = 0; i < condition.Length; i++)
            {
                string conditions = condition[i];
                if (i == 0)
                {
                    basicData = from s in basicData
                        where s.disater_types_id.Contains("d" + conditions + ";")
                        select s;
                }
            }
        }

        if (!string.IsNullOrEmpty(post["improve_status"]))
        {
            if (post["improve_status"] == "Y")
            {
                basicData = from s in basicData
                    where s.improve_status_val == 100
                    select s;
            }
            else
            {
                basicData = from s in basicData
                    where s.improve_status_val < 100
                    select s;
            }
        }

        var basicDataList = basicData.ToList();
        var detailData = from detail in _db.audit_detail_infos
            select detail;
        if (!string.IsNullOrEmpty(post["audit_year"]))
        {
            DateOnly start_date = DateOnly.FromDateTime(Convert.ToDateTime(post["audit_year"] + "/01/01"));
            DateOnly end_date = start_date.AddYears(1);
    
            basicDataList = basicDataList
                .Where(v => v.audit_start_date >= start_date && v.audit_start_date < end_date)
                .ToList();
        }

        var suggets = from sugget in _db.audit_suggests
            join audit_basic_info in _db.audit_basic_infos on sugget.audit_basic_id equals audit_basic_info.id
                into audit_basic_info
            from basic in audit_basic_info.DefaultIfEmpty()
            join audit_type in _db.audit_types on basic.audit_type_id equals audit_type.id into audit_type
            from auditType in audit_type.DefaultIfEmpty()
            join factory_name in _db.factory_names on basic.factory_id equals factory_name.id into factory
            from C in factory.DefaultIfEmpty()
            join suggest_item in _db.suggest_items on sugget.suggest_item_id equals suggest_item.id into
                suggest_item
            from M in suggest_item.DefaultIfEmpty()
            join suggest_type in _db.suggest_types on M.suggest_type_id equals suggest_type.id into suggest_type
            from L in suggest_type.DefaultIfEmpty()
            join suggest_category in _db.suggest_categories on L.suggest_category_id equals suggest_category.id
                into suggest_category
            from K in suggest_category.DefaultIfEmpty()
            select new
            {
                id = sugget.uuid,
                audit_type = auditType.audit_type1,
                factory = C.factory,
                audit_detail_id = sugget.audit_detail_id ?? 0,
                suggest_category_id = L.suggest_category_id ?? 0,
                suggest_type_id = M.suggest_type_id ?? 0,
                suggest_item_id = sugget.suggest_item_id ?? 0,
                suggest_category = K.suggest_category1 ?? string.Empty,
                suggest_type = L.suggest_type1 ?? string.Empty,
                suggest_item = M.suggest_item1 ?? string.Empty,
                item_no = sugget.item_no ?? 0,
                suggest = sugget.suggest ?? string.Empty,
                audit_basic_id = sugget.audit_basic_id,
                action = sugget.action ?? string.Empty,
                short_term = sugget.short_term ?? string.Empty,
                Mid_term = sugget.Mid_term ?? string.Empty,
                long_term = sugget.long_term ?? string.Empty,
                handling_situation = sugget.handling_situation ?? string.Empty,
                improve_status = sugget.improve_status ?? string.Empty,
                responsible_unit = sugget.responsible_unit ?? string.Empty,
                budget = sugget.budget ?? string.Empty,
                Remarks = sugget.Remarks ?? string.Empty
            };
        if (!string.IsNullOrEmpty(post["suggest_category"]))
        {
            int suggest_category_id = Convert.ToInt32(post["suggest_category"]);
            suggets = suggets.Where(s => s.suggest_category_id == suggest_category_id);
        }

        if (!string.IsNullOrEmpty(post["suggest_type"]))
        {
            int suggest_type_id = Convert.ToInt32(post["suggest_type"]);
            suggets = suggets.Where(s => s.suggest_type_id == suggest_type_id);
        }

        if (!string.IsNullOrEmpty(post["suggest_item"]))
        {
            int suggest_item_id = Convert.ToInt32(post["suggest_item"]);
            suggets = suggets.Where(s => s.suggest_item_id == suggest_item_id);
        }

        var basicDataListId = basicDataList.Select(s => s.id).Distinct().ToList();
        var suggetsList = suggets.Where(v => basicDataListId.Contains(v.audit_basic_id ?? 0))
            .ToList();
        var audit_basic_id = suggetsList.Select(s => s.audit_basic_id).Distinct().ToList();
        var basics = basicDataList.Where(v => audit_basic_id.Contains(v.id)).Distinct().ToList();
        try
        {
            Task.Run(() => ChatData(Json(basics), Json(suggetsList)));
            var vData = new object[] { basics, suggetsList };
            return JsonConvert.SerializeObject(vData.ToList());
        }
        catch (Exception)
        {
            var vData = new object[] { basics, suggetsList };
            return JsonConvert.SerializeObject(vData.ToList());
        }
        
    }
     public async Task ChatData(JsonResult BasicData, JsonResult SuggestData)
    {
        if (SuggestData == null && BasicData == null)
        {
            // 處理錯誤或記錄訊息
            return;
        }

        // 序列化數據
        var datakeyMap = new Dictionary<string, string>
        {
            { "audit_cause", "督導原因" },
            { "audit_start_date", "督導日期" },
            { "factory", "工廠" },
            { "company", "公司" },
            { "industrial_area", "工業區" },
            { "audit_type", "督導類型" },
            { "incident_description", "事故原因" },
            { "penalty_detail", "裁罰" },
            { "improve_status_val", "改善完成率" },
            // 沒有給其他鍵定義新的鍵，保持它們不變
        };
        // 序列化數據
        var tablekeyMap = new Dictionary<string, string>
        {
            { "audit_type", "督導類型" },
            { "factory", "工廠" },
            { "suggest_category", "建議類別" },
            { "suggest_type", "建議類型" },
            { "suggest_item", "建議項目" },
            { "item_no", "項目編號" },
            { "suggest", "建議" },
            { "action", "改善行動" },
            { "short_term", "短期" },
            { "Mid_term", "中期" },
            { "long_term", "長期" },
            { "handling_situation", "處理情況" },
            { "improve_status", "改善狀態" },
            { "responsible_unit", "負責單位" },
            { "budget", "預算" },
            { "Remarks", "備註" }
            // 沒有給其他鍵定義新的鍵，保持它們不變
        };
    }
        // 撈取督導回覆資料
        [HttpPost]
        public JsonResult GetImprovementKpi(FormCollection post)
        {
            // 使用特定格式來解析日期
            try
            {
                string start = post["start"]; //2024-01-01
                string end = post["end"];
                // 將字符串轉換為 DateTime
                DateTime st;
                DateTime ed;
                // 使用 DateTime.ParseExact 進行精確解析
                st = DateTime.ParseExact(start, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                ed = DateTime.ParseExact(end, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                
                var combinedQuery = (from edit in _db.audit_data_edits
                        join info in _db.audit_basic_infos
                            on edit.audit_uuid equals info.uuid
                        join factory in _db.factory_names
                            on info.factory_id equals factory.id
                        join company_name in _db.company_names
                            on factory.company_id equals company_name.id
                        join suggest in _db.audit_suggests
                            on info.id equals suggest.audit_basic_id
                        join commit in _db.audit_report_commits
                            on edit.uuid equals commit.auditData_edit_uuid into commitGroup
                        join detail in _db.audit_detail_infos
                            on suggest.audit_detail_id equals detail.id
                        join type in _db.audit_types
                            on info.audit_type_id equals type.id
                        from commit in commitGroup.DefaultIfEmpty()
                        where edit.start_time >= st && edit.end_time <= ed // 篩選開始和結束時間
                        group new { info, edit, factory, detail, type, commit, company_name } by edit.uuid
                        into grouped // 根據 audit_basic_info 為單位
                        let firstItem = grouped.FirstOrDefault()
                        select new
                        {
                            CompanyName = firstItem.company_name.company, // 公司
                            FactoryName = firstItem.factory.factory, // 工廠名稱
                            AuditDate = firstItem.detail.audit_start_date.HasValue
                                ? firstItem.detail.audit_start_date.Value.ToString("yyyy-MM-dd") 
                                : "", // 督導時間
                            begintime = firstItem.edit.start_time,
                            endtime = firstItem.edit.end_time,
                            Audittype = firstItem.type.audit_type1, // 督導類別
                            Status = firstItem.edit.status == "N" ? "否" : "是", // 是否完成結案 N否 Y是
                            time = firstItem.commit.update_at.HasValue
                                ? "民國" + 
                                  (firstItem.commit.update_at.Value.Year - 1911) + "年" + 
                                  firstItem.commit.update_at.Value.Month + "月" + 
                                  firstItem.commit.update_at.Value.Day + "日" + 
                                  firstItem.commit.update_at.Value.Hour + "時" 
                                : "", // 更新時間
                            auditcommiturl = firstItem.edit.audit_uuid != null
                                ? "/Auditedit/EditAuditInfo/" + firstItem.edit.uuid
                                : "", // 督導連結
                            CommitFilename = firstItem.commit != null
                                ? "/Files/Commit/" + firstItem.commit.filename + firstItem.commit.filetype
                                : "未完成上傳" // 伺服器檔名
                        })
                    .OrderBy(x => x.FactoryName) // 添加排序，按工廠升序
                    .ThenBy(y => y.AuditDate)
                    .ToList();

                return Json(combinedQuery);
                
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                return Json(new { success = false, message = ex });
            }
        }
        // 輔助方法：將日期格式化為指定格式
        private static string FormatDateTime(DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ToString("yyyy-MM-dd HH:mm") : "--";
        }
        
        public IActionResult AuditInfo(string id)
        {
            if (String.IsNullOrEmpty(id))
            {
                Response.Redirect("~/");
                return new EmptyResult();
            }
            else
            {
                
                var audit_basic_info = (from basic in _db.audit_basic_infos
                        join factory_name in _db.factory_names on basic.factory_id equals factory_name.id into factory
                        from C in factory.DefaultIfEmpty()
                        join industrial_area_info in _db.industrial_area_infos on C.industrial_area_id equals
                            industrial_area_info.id into industrial_area_info
                        from E in industrial_area_info.DefaultIfEmpty()
                        join township_info in _db.township_infos on E.township_id equals township_info.id into
                            township_info
                        from F in township_info.DefaultIfEmpty()
                        join company_name in _db.company_names on C.company_id equals company_name.id into company
                        from D in company.DefaultIfEmpty()
                        join city_info in _db.city_infos on F.city_id equals city_info.id into city_info
                        from G in city_info.DefaultIfEmpty()
                        join audit_type in _db.audit_types on basic.audit_type_id equals audit_type.id into audit_type
                        from H in audit_type.DefaultIfEmpty()
                        join audit_cause in _db.audit_causes on basic.audit_cause_id equals audit_cause.id into
                            audit_cause
                        from I in audit_cause.DefaultIfEmpty()
                        select new
                        {
                            id = basic.id,
                            uuid = basic.uuid,
                            city_id = F.city_id ?? 0,
                            city = G.city ?? string.Empty,
                            township_id = E.township_id ?? 0,
                            township = F.township ?? string.Empty,
                            industrial_area_id = C.industrial_area_id ?? 0,
                            industrial_area = E.industrial_area ?? string.Empty,
                            company_id = C.company_id ?? 0,
                            company = D.company,
                            factory = C.factory,
                            factory_id = basic.factory_id ?? 0,
                            audit_type_id = basic.audit_type_id ?? 0,
                            audit_type = H.audit_type1 ?? string.Empty,
                            audit_cause_id = basic.audit_cause_id ?? 0,
                            audit_cause = I == null ? "" : I.cause_name,
                            disater_types_id = basic.disater_types_id ?? string.Empty,
                            disater_types = basic.disater_types == "" || basic.disater_types == null
                                ? "無"
                                : basic.disater_types,
                            incident_datetime = basic.incident_datetime.HasValue
                                ? basic.incident_datetime.Value.ToString("yyyy-MM-dd HH:mm")
                                : "--",
                            incident_description = basic.incident_description ?? string.Empty,
                            sd = basic.sd == "Y" ? "是" : "否",
                            penalty = basic.penalty == "Y" ? "是" : "否",
                            penalty_detail = basic.penalty_detail ?? string.Empty,
                            situation = basic.situation ?? string.Empty
                        }
                    ).ToList().Where(q => q.uuid == id).FirstOrDefault();
                
                var audit_detail_info = (from c in _db.audit_detail_infos
                    join enter_type in _db.enter_types on c.enter_type_id equals enter_type.id into enter_type
                    from s in enter_type.DefaultIfEmpty()
                    select new
                    {
                        id = c.id,
                        audit_basic_id = c.audit_basic_id,
                        enter_type = s.enter_type1,
                        start_date = c.audit_start_date.HasValue
                            ? c.audit_start_date.Value.ToString("yyyy-MM-dd")
                            : "--",
                        end_date = c.audit_end_date.HasValue ? c.audit_end_date.Value.ToString("yyyy-MM-dd") : "--",
                        filename = c.filename ?? string.Empty,
                        filename_uuid = c.filename_uuid ?? string.Empty,
                        reportname = c.reportname ?? string.Empty,
                        reportname_uuid = c.reportname_uuid ?? string.Empty
                    }).ToList().Where(q => q.audit_basic_id == audit_basic_info.id).OrderBy(t => t.start_date);
                var details_id = audit_detail_info.Select(s => s.id).Distinct().ToList();
                var audit_date = _db.audit_dates.Where(v => details_id.Contains(v.audit_detail_id))
                    .Select(d => new
                    {
                        id = d.id, audit_detail_id = d.audit_detail_id,
                        audit_date1 = d.audit_date1.HasValue ? d.audit_date1.Value.ToString("yyyy-MM-dd") : "--",
                        due = d.due
                    }).ToList();
                var suggest = (from sugget in _db.audit_suggests
                   join detail_info in _db.audit_detail_infos on sugget.audit_detail_id equals detail_info.id into detailGroup
                   from detail_info in detailGroup.DefaultIfEmpty()
                   join enter_type in _db.enter_types on detail_info.enter_type_id equals enter_type.id into enterTypeGroup
                   from enter_type in enterTypeGroup.DefaultIfEmpty()
                   join suggest_item in _db.suggest_items on sugget.suggest_item_id equals suggest_item.id into suggestItemGroup
                   from M in suggestItemGroup.DefaultIfEmpty()
                   join suggest_type in _db.suggest_types on M.suggest_type_id equals suggest_type.id into suggestTypeGroup
                   from L in suggestTypeGroup.DefaultIfEmpty()
                   join suggest_category in _db.suggest_categories on L.suggest_category_id equals suggest_category.id into suggestCategoryGroup
                   from K in suggestCategoryGroup.DefaultIfEmpty()
                   where sugget.audit_basic_id == audit_basic_info.id
                   select new
                   {
                       suggest = new
                       {
                           uuid = sugget.uuid,
                           audit_basic_id = sugget.audit_basic_id,
                           audit_detail_id = sugget.audit_detail_id,
                           enter_type = enter_type.enter_type1,
                           suggest_item_id = sugget.suggest_item_id,
                           parent_id = sugget.parent_id,
                           item_no = sugget.item_no,
                           suggest = sugget.suggest,
                           sub_suggest = sugget.sub_suggest,
                           participate = sugget.participate,
                           action = sugget.action,
                           short_term = sugget.short_term,
                           Mid_term = sugget.Mid_term,
                           long_term = sugget.long_term,
                           handling_situation = sugget.handling_situation,
                           improve_status = sugget.improve_status,
                           responsible_unit = sugget.responsible_unit,
                           budget = sugget.budget,
                           budget_val = sugget.budget_val,
                           expert_id = sugget.expert_id,
                           Remarks = sugget.Remarks,
                       },
                       suggest_type = L,
                       suggest_item = M,
                       suggest_category = K
                   }).ToList();
                
                var audit_control_yuan = _db.audit_control_yuans.Where(c => c.audit_basic_id == audit_basic_info.id)
                    .OrderBy(t => t.come_date).ToList();
                var audit_improvement_doc = _db.audit_improvement_docs
                    .Where(c => c.audit_basic_id == audit_basic_info.id).OrderBy(t => t.receive_date).ToList();
                
                double allParticipate = suggest.Where(x => x.suggest.participate == "是").Count();
                double improveYet = suggest
                    .Where(x => x.suggest.participate == "是" && x.suggest.improve_status == "否").Count();
                double dParticipates = suggest.Count();
                double participate_status = 0;
                double improve_status = 0;
                double allimproveYet = suggest.Where(x => x.suggest.improve_status == "").Count();
                if (allParticipate != 0 && dParticipates != allimproveYet)
                {
                    double dCaculate = ((allParticipate - improveYet) / allParticipate);
                    improve_status = Math.Round(dCaculate, 2) * 100;
                }
                else
                {
                    improve_status = 0;
                }
                
                if (dParticipates != 0 && allParticipate != 0)
                {
                    double dParticipate = (allParticipate / dParticipates);
                    participate_status = Math.Round(dParticipate, 2) * 100;
                }
                
                ViewBag.participate_status = participate_status;
                ViewBag.improve_status = improve_status;
                ViewBag.audit_basic_id = audit_basic_info.id;
                ViewBag.company = audit_basic_info.company;
                ViewBag.city = audit_basic_info.city;
                ViewBag.township = audit_basic_info.township;
                ViewBag.industrial_area = audit_basic_info.industrial_area;
                ViewBag.company = audit_basic_info.company;
                ViewBag.factory = audit_basic_info.factory;
                ViewBag.audit_type = audit_basic_info.audit_type;
                ViewBag.audit_cause = audit_basic_info.audit_cause;
                ViewBag.disater_types = audit_basic_info.disater_types;
                ViewBag.incident_datetime = audit_basic_info.incident_datetime;
                ViewBag.incident_description = audit_basic_info.incident_description;
                ViewBag.sd = audit_basic_info.sd;
                ViewBag.penalty = audit_basic_info.penalty;
                ViewBag.penalty_detail = audit_basic_info.penalty_detail;
                ViewBag.situation = audit_basic_info.situation;
                ViewBag.AuditControlYuan = audit_control_yuan;
                ViewBag.AuditDetailInfo = audit_detail_info;
                ViewBag.audit_improvement_doc = audit_improvement_doc;
                ViewBag.AuditDate = audit_date;
                ViewBag.Suggest = suggest;
                ViewBag.SuggestData = DetailSuggest(audit_basic_info.id);
                
                var enterType = _userEditService.EnterType();
                ViewBag.enterTypeJson = JsonConvert.SerializeObject(enterType);
                return View();
            }
        }
        public string DetailSuggest(int id)
        {
            var suggest_join_enterType = (from sugget in _db.audit_suggests
                join A in _db.audit_detail_infos on sugget.audit_detail_id equals A.id into A
                from detail_info in A.DefaultIfEmpty()
                join B in _db.enter_types on detail_info.enter_type_id equals B.id into B
                from enter_type in B.DefaultIfEmpty()
                where sugget.audit_basic_id == id
                select new
                {
                    uuid = sugget.uuid,
                    audit_basic_id = sugget.audit_basic_id,
                    audit_detail_id = sugget.audit_detail_id,
                    enter_type = enter_type.enter_type1,
                    suggest_item_id = sugget.suggest_item_id,
                    parent_id = sugget.parent_id,
                    item_no = sugget.item_no,
                    suggest = sugget.suggest,
                    sub_suggest = sugget.sub_suggest,
                    participate = sugget.participate,
                    action = sugget.action,
                    short_term = sugget.short_term,
                    Mid_term = sugget.Mid_term,
                    long_term = sugget.long_term,
                    handling_situation = sugget.handling_situation,
                    improve_status = sugget.improve_status,
                    responsible_unit = sugget.responsible_unit,
                    budget = sugget.budget,
                    budget_val = sugget.budget_val,
                    expert_id = sugget.expert_id,
                    Remarks = sugget.Remarks,
                }).ToList();
            string data = JsonConvert.SerializeObject(suggest_join_enterType);
            return data;
            
        }
}