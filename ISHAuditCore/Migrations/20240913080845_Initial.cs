using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ISHAuditCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit_basic_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    audit_type_id = table.Column<int>(type: "int", nullable: true),
                    audit_cause_id = table.Column<int>(type: "int", nullable: true),
                    disater_types_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    disater_types = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    incident_datetime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    incident_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sd = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    penalty = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    penalty_detail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    improve_status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    situation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    participate_status_val = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    improve_status_val = table.Column<decimal>(type: "decimal(18,0)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_ba__3213E83F08EA89FE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_cause",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cause_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    cause_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_ca__3213E83F8C3921EA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_contact_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_job_title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    user_mail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    enterprise_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_co__3213E83F98B0F683", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_control_yuan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    audit_basic_id = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    come_date = table.Column<DateOnly>(type: "date", nullable: true),
                    come_doc_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    come_doc_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    come_doc_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    back_date = table.Column<DateOnly>(type: "date", nullable: true),
                    back_doc_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    back_doc_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    back_doc_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_co__3213E83F988A5EF1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_data_edit",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    end_time = table.Column<DateTime>(type: "datetime", nullable: true),
                    status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    audit_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    user_contacts = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_da__7F42793084E1BF3B", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "audit_data_edit_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    audit_data_edit_uuid = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    update_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    participate_status_val = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    improve_status_val = table.Column<decimal>(type: "decimal(5,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_da__3213E83F7FE17571", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_date",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    audit_detail_id = table.Column<int>(type: "int", nullable: false),
                    audit_date = table.Column<DateOnly>(type: "date", nullable: true),
                    due = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_da__3213E83F9A63B9BB", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_detail_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    audit_basic_id = table.Column<int>(type: "int", nullable: true),
                    enter_type_id = table.Column<int>(type: "int", nullable: true),
                    meeting_type_id = table.Column<int>(type: "int", nullable: true),
                    audit_start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    audit_end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    filename = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    filename_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    reportname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    reportname_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_de__3213E83F68DEFB5B", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_improvement_doc",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    doc_uuid = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    audit_basic_id = table.Column<int>(type: "int", nullable: true),
                    receive_date = table.Column<DateOnly>(type: "date", nullable: true),
                    doc_text = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    doc_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_im__7F427930A24FACEE", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "audit_report_commit",
                columns: table => new
                {
                    filename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    auditData_edit_uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    filetype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    original_filename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_re__7F42793074DBCE1E", x => x.filename);
                });

            migrationBuilder.CreateTable(
                name: "audit_report_file_log",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    audit_detail_id = table.Column<int>(type: "int", nullable: true),
                    filename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    filetype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_re__7F427930CBD00B9B", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "audit_response_file_log",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    audit_detail_id = table.Column<int>(type: "int", nullable: true),
                    filename = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    filetype = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_re__7F42793077BC87F5", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "audit_suggest",
                columns: table => new
                {
                    uuid = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    audit_basic_id = table.Column<int>(type: "int", nullable: true),
                    audit_detail_id = table.Column<int>(type: "int", nullable: true),
                    suggest_item_id = table.Column<int>(type: "int", nullable: true),
                    parent_id = table.Column<int>(type: "int", nullable: true),
                    item_no = table.Column<int>(type: "int", nullable: true),
                    suggest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sub_suggest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    participate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    short_term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mid_term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    long_term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    handling_situation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    improve_status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    responsible_unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    budget = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    budget_val = table.Column<int>(type: "int", nullable: true),
                    expert_id = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    create_by = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_su__7F427930BEA72FCA", x => x.uuid);
                });

            migrationBuilder.CreateTable(
                name: "audit_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    audit_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__audit_ty__3213E83F53D77E43", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__city_inf__3213E83F86F45047", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    enterprise_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__company___3213E83FD2C8FC45", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "disaster_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    disaster = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__disaster__3213E83F175B7130", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eco_kpi_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    eco_kpi_template_id = table.Column<int>(type: "int", nullable: true),
                    eco_kpi_report_id = table.Column<int>(type: "int", nullable: true),
                    execution_of_kpi = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    annual_average = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    annual_average_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    eco_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    conform = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eco_kpi___3213E83F1A66B86A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eco_kpi_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eco_kpi_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eco_kpi___3213E83F609D7504", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eco_kpi_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eco_kpi_report_year = table.Column<int>(type: "int", nullable: true),
                    report_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eco_kpi___3213E83F1A438166", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eco_kpi_sub_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eco_kpi_items_id = table.Column<int>(type: "int", nullable: true),
                    eco_kpi_sub_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deadline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eco_kpi___3213E83F4B48E634", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "eco_kpi_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name_id = table.Column<int>(type: "int", nullable: true),
                    eco_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    annual_average = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    annual_average_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__eco_kpi___3213E83F01BFB214", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enter_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enter_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__enter_ty__3213E83FCBAB158F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "enterprise_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enterprise = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__enterpri__3213E83FDA8300E8", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ep_kpi_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    ep_kpi_template_id = table.Column<int>(type: "int", nullable: true),
                    ep_kpi_report_id = table.Column<int>(type: "int", nullable: true),
                    execution_of_kpi = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    execution_of_kpi_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    annual_average = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ep_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    annual_average_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    conform = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ep_kpi_d__3213E83FDD915878", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ep_kpi_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ep_kpi_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ep_kpi_i__3213E83F2E24E921", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ep_kpi_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ep_kpi_report_year = table.Column<int>(type: "int", nullable: true),
                    report_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ep_kpi_r__3213E83F5999606C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ep_kpi_sub_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ep_kpi_items_id = table.Column<int>(type: "int", nullable: true),
                    ep_kpi_sub_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deadline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ep_kpi_s__3213E83FC72ADFEE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ep_kpi_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name_id = table.Column<int>(type: "int", nullable: true),
                    factory_name_id = table.Column<int>(type: "int", nullable: true),
                    ep_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    annual_average = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    annual_average_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__ep_kpi_t__3213E83F4A53ACF5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "expert_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    uuid = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    expert_names = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    sevice_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    position_id = table.Column<int>(type: "int", nullable: true),
                    speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    speciality_category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tel_number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    experience = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__expert_d__3213E83FF72C1A09", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "factory_name",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    factory = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    industrial_area_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__factory___3213E83FB91AFE5C", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "industrial_area_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industrial_area = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    township_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__industri__3213E83F90524761", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "KpiDocument",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    model_type = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    org = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    org_id = table.Column<int>(type: "int", nullable: true),
                    parent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    text = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    file_type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    classname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KpiDocum__3213E83FB0A232F5", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psm_kpi_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    psm_kpi_template_id = table.Column<int>(type: "int", nullable: true),
                    psm_kpi_report_id = table.Column<int>(type: "int", nullable: true),
                    psm_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    annual_average = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    annual_average_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    execution_of_kpi = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    conform = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__psm_kpi___3213E83F071A7ADA", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psm_kpi_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    psm_kpi_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    number = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__psm_kpi___3213E83F3433DA4A", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psm_kpi_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    kpi_report_year = table.Column<int>(type: "int", nullable: true),
                    report_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__psm_kpi___3213E83F1D396739", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psm_kpi_sub_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kpi_items_id = table.Column<int>(type: "int", nullable: true),
                    kpi_sub_items = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deadline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__psm_kpi___3213E83F0A262AA3", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "psm_kpi_template",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    company_name_id = table.Column<int>(type: "int", nullable: true),
                    factory_name_id = table.Column<int>(type: "int", nullable: true),
                    psm_kpi_sub_item_id = table.Column<int>(type: "int", nullable: true),
                    annual_average = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    annual_average_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    baseline_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    expressions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    objective_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    deviation = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    checkout_time_unit = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__psm_kpi___3213E83F20125115", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "result_data",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    year = table.Column<int>(type: "int", nullable: true),
                    expert_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    expert_name_id = table.Column<int>(type: "int", nullable: true),
                    enterprise_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    result_suggestion_type_id = table.Column<int>(type: "int", nullable: true),
                    indicator_category = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    suggestion_content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    response = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    execution = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    estimated_completion_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    completion_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    result_report_id = table.Column<int>(type: "int", nullable: true),
                    kpi_item_id = table.Column<int>(type: "int", nullable: true),
                    number = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    remark = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__result_d__3213E83FA4B86494", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "result_report",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    report_year = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    report_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    created_time = table.Column<DateTime>(type: "datetime2", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    status = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__result_r__3213E83F7140F8AF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "result_suggestion_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    suggestion_type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__result_s__3213E83FCE84C254", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "set_system",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    code_val = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__set_syst__3213E83FDFFCAD4D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "suggest_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    suggest_category = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__suggest___3213E83E1F6751D9", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "suggest_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    suggest_item = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    suggest_type_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__suggest___3213E83E519D8385", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "suggest_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    suggest_type = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    suggest_category_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__suggest___3213E83EC634A44C", x => x.id)
                        .Annotation("SqlServer:Clustered", false);
                });

            migrationBuilder.CreateTable(
                name: "township_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    township = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    city_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__township__3213E83F1A4DF9D1", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    username = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    enterprise_id = table.Column<int>(type: "int", nullable: true),
                    company_id = table.Column<int>(type: "int", nullable: true),
                    factory_id = table.Column<int>(type: "int", nullable: true),
                    authority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    update_at = table.Column<DateTime>(type: "datetime2", nullable: true),
                    anyllmworkspace = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__user_inf__3213E83F26124D97", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "work_position",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    position = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__work_pos__3213E83FD1103E86", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "credentials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Descriptor = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PublicKey = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    SignatureCounter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__credenti__3214EC07E795DAF5", x => x.Id);
                    table.ForeignKey(
                        name: "FK__credentia__UserI__0E6E26BF",
                        column: x => x.UserId,
                        principalTable: "user_info",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_credentials_UserId",
                table: "credentials",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_basic_info");

            migrationBuilder.DropTable(
                name: "audit_cause");

            migrationBuilder.DropTable(
                name: "audit_contact_info");

            migrationBuilder.DropTable(
                name: "audit_control_yuan");

            migrationBuilder.DropTable(
                name: "audit_data_edit");

            migrationBuilder.DropTable(
                name: "audit_data_edit_log");

            migrationBuilder.DropTable(
                name: "audit_date");

            migrationBuilder.DropTable(
                name: "audit_detail_info");

            migrationBuilder.DropTable(
                name: "audit_improvement_doc");

            migrationBuilder.DropTable(
                name: "audit_report_commit");

            migrationBuilder.DropTable(
                name: "audit_report_file_log");

            migrationBuilder.DropTable(
                name: "audit_response_file_log");

            migrationBuilder.DropTable(
                name: "audit_suggest");

            migrationBuilder.DropTable(
                name: "audit_type");

            migrationBuilder.DropTable(
                name: "city_info");

            migrationBuilder.DropTable(
                name: "company_name");

            migrationBuilder.DropTable(
                name: "credentials");

            migrationBuilder.DropTable(
                name: "disaster_type");

            migrationBuilder.DropTable(
                name: "eco_kpi_data");

            migrationBuilder.DropTable(
                name: "eco_kpi_item");

            migrationBuilder.DropTable(
                name: "eco_kpi_report");

            migrationBuilder.DropTable(
                name: "eco_kpi_sub_item");

            migrationBuilder.DropTable(
                name: "eco_kpi_template");

            migrationBuilder.DropTable(
                name: "enter_type");

            migrationBuilder.DropTable(
                name: "enterprise_name");

            migrationBuilder.DropTable(
                name: "ep_kpi_data");

            migrationBuilder.DropTable(
                name: "ep_kpi_item");

            migrationBuilder.DropTable(
                name: "ep_kpi_report");

            migrationBuilder.DropTable(
                name: "ep_kpi_sub_item");

            migrationBuilder.DropTable(
                name: "ep_kpi_template");

            migrationBuilder.DropTable(
                name: "expert_data");

            migrationBuilder.DropTable(
                name: "factory_name");

            migrationBuilder.DropTable(
                name: "industrial_area_info");

            migrationBuilder.DropTable(
                name: "KpiDocument");

            migrationBuilder.DropTable(
                name: "psm_kpi_data");

            migrationBuilder.DropTable(
                name: "psm_kpi_item");

            migrationBuilder.DropTable(
                name: "psm_kpi_report");

            migrationBuilder.DropTable(
                name: "psm_kpi_sub_item");

            migrationBuilder.DropTable(
                name: "psm_kpi_template");

            migrationBuilder.DropTable(
                name: "result_data");

            migrationBuilder.DropTable(
                name: "result_report");

            migrationBuilder.DropTable(
                name: "result_suggestion_type");

            migrationBuilder.DropTable(
                name: "set_system");

            migrationBuilder.DropTable(
                name: "suggest_category");

            migrationBuilder.DropTable(
                name: "suggest_item");

            migrationBuilder.DropTable(
                name: "suggest_type");

            migrationBuilder.DropTable(
                name: "township_info");

            migrationBuilder.DropTable(
                name: "work_position");

            migrationBuilder.DropTable(
                name: "user_info");
        }
    }
}
