using System;
using System.Collections.Generic;
using ISHAuditCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace ISHAuditCore.Context;

public partial class ISHAuditDbcontext : DbContext
{
    public ISHAuditDbcontext()
    {
    }

    public ISHAuditDbcontext(DbContextOptions<ISHAuditDbcontext> options)
        : base(options)
    {
    }

    public virtual DbSet<KpiDocument> KpiDocuments { get; set; }

    public virtual DbSet<audit_basic_info> audit_basic_infos { get; set; }

    public virtual DbSet<audit_cause> audit_causes { get; set; }

    public virtual DbSet<audit_contact_info> audit_contact_infos { get; set; }

    public virtual DbSet<audit_control_yuan> audit_control_yuans { get; set; }

    public virtual DbSet<audit_data_edit> audit_data_edits { get; set; }

    public virtual DbSet<audit_data_edit_log> audit_data_edit_logs { get; set; }

    public virtual DbSet<audit_date> audit_dates { get; set; }

    public virtual DbSet<audit_detail_info> audit_detail_infos { get; set; }

    public virtual DbSet<audit_improvement_datum> audit_improvement_data { get; set; }

    public virtual DbSet<audit_improvement_doc> audit_improvement_docs { get; set; }

    public virtual DbSet<audit_report_commit> audit_report_commits { get; set; }

    public virtual DbSet<audit_report_file_log> audit_report_file_logs { get; set; }

    public virtual DbSet<audit_response_file_log> audit_response_file_logs { get; set; }

    public virtual DbSet<audit_suggest> audit_suggests { get; set; }

    public virtual DbSet<audit_suggest_log> audit_suggest_logs { get; set; }

    public virtual DbSet<audit_type> audit_types { get; set; }

    public virtual DbSet<city_info> city_infos { get; set; }

    public virtual DbSet<company_name> company_names { get; set; }

    public virtual DbSet<credential> credentials { get; set; }

    public virtual DbSet<disaster_type> disaster_types { get; set; }

    public virtual DbSet<eco_kpi_datum> eco_kpi_data { get; set; }

    public virtual DbSet<eco_kpi_item> eco_kpi_items { get; set; }

    public virtual DbSet<eco_kpi_report> eco_kpi_reports { get; set; }

    public virtual DbSet<eco_kpi_sub_item> eco_kpi_sub_items { get; set; }

    public virtual DbSet<eco_kpi_template> eco_kpi_templates { get; set; }

    public virtual DbSet<enter_type> enter_types { get; set; }

    public virtual DbSet<enterprise_name> enterprise_names { get; set; }

    public virtual DbSet<ep_kpi_datum> ep_kpi_data { get; set; }

    public virtual DbSet<ep_kpi_item> ep_kpi_items { get; set; }

    public virtual DbSet<ep_kpi_report> ep_kpi_reports { get; set; }

    public virtual DbSet<ep_kpi_sub_item> ep_kpi_sub_items { get; set; }

    public virtual DbSet<ep_kpi_template> ep_kpi_templates { get; set; }

    public virtual DbSet<expert_datum> expert_data { get; set; }

    public virtual DbSet<factory_name> factory_names { get; set; }

    public virtual DbSet<improve_type_tb> improve_type_tbs { get; set; }

    public virtual DbSet<industrial_area_info> industrial_area_infos { get; set; }

    public virtual DbSet<psm_kpi_datum> psm_kpi_data { get; set; }

    public virtual DbSet<psm_kpi_item> psm_kpi_items { get; set; }

    public virtual DbSet<psm_kpi_report> psm_kpi_reports { get; set; }

    public virtual DbSet<psm_kpi_sub_item> psm_kpi_sub_items { get; set; }

    public virtual DbSet<psm_kpi_template> psm_kpi_templates { get; set; }

    public virtual DbSet<result_datum> result_data { get; set; }

    public virtual DbSet<result_report> result_reports { get; set; }

    public virtual DbSet<result_suggestion_type> result_suggestion_types { get; set; }

    public virtual DbSet<set_system> set_systems { get; set; }

    public virtual DbSet<suggest_category> suggest_categories { get; set; }

    public virtual DbSet<suggest_item> suggest_items { get; set; }

    public virtual DbSet<suggest_type> suggest_types { get; set; }

    public virtual DbSet<township_info> township_infos { get; set; }

    public virtual DbSet<user_info> user_infos { get; set; }

    public virtual DbSet<work_position> work_positions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=59.127.84.234;Database=isha_sys_dev;User Id=sa;Password=Isha0486;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<KpiDocument>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__KpiDocum__3213E83FB0A232F5");
        });

        modelBuilder.Entity<audit_basic_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_ba__3213E83F08EA89FE");
        });

        modelBuilder.Entity<audit_cause>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_ca__3213E83F8C3921EA");
        });

        modelBuilder.Entity<audit_contact_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_co__3213E83F98B0F683");
        });

        modelBuilder.Entity<audit_control_yuan>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_co__3213E83F988A5EF1");
        });

        modelBuilder.Entity<audit_data_edit>(entity =>
        {
            entity.HasKey(e => e.uuid).HasName("PK__audit_da__7F42793084E1BF3B");
        });

        modelBuilder.Entity<audit_data_edit_log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_da__3213E83F7FE17571");
        });

        modelBuilder.Entity<audit_date>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_da__3213E83F9A63B9BB");
        });

        modelBuilder.Entity<audit_detail_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_de__3213E83F68DEFB5B");
        });

        modelBuilder.Entity<audit_improvement_doc>(entity =>
        {
            entity.HasKey(e => e.uuid).HasName("PK__audit_im__7F427930A24FACEE");
        });

        modelBuilder.Entity<audit_report_commit>(entity =>
        {
            entity.HasKey(e => e.filename).HasName("PK__audit_re__7F42793074DBCE1E");
        });

        modelBuilder.Entity<audit_report_file_log>(entity =>
        {
            entity.HasKey(e => e.uuid).HasName("PK__audit_re__7F427930CBD00B9B");
        });

        modelBuilder.Entity<audit_response_file_log>(entity =>
        {
            entity.HasKey(e => e.uuid).HasName("PK__audit_re__7F42793077BC87F5");
        });

        modelBuilder.Entity<audit_suggest>(entity =>
        {
            entity.HasKey(e => e.uuid).HasName("PK__audit_su__7F427930BEA72FCA");
        });

        modelBuilder.Entity<audit_suggest_log>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_su__3213E83F3F6546DF");
        });

        modelBuilder.Entity<audit_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__audit_ty__3213E83F53D77E43");
        });

        modelBuilder.Entity<city_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__city_inf__3213E83F86F45047");
        });

        modelBuilder.Entity<company_name>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__company___3213E83FD2C8FC45");
        });

        modelBuilder.Entity<credential>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__credenti__3214EC07E795DAF5");

            entity.HasOne(d => d.User).WithMany(p => p.credentials)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__credentia__UserI__0E6E26BF");
        });

        modelBuilder.Entity<disaster_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__disaster__3213E83F175B7130");
        });

        modelBuilder.Entity<eco_kpi_datum>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eco_kpi___3213E83F1A66B86A");
        });

        modelBuilder.Entity<eco_kpi_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eco_kpi___3213E83F609D7504");
        });

        modelBuilder.Entity<eco_kpi_report>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eco_kpi___3213E83F1A438166");
        });

        modelBuilder.Entity<eco_kpi_sub_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eco_kpi___3213E83F4B48E634");
        });

        modelBuilder.Entity<eco_kpi_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__eco_kpi___3213E83F01BFB214");
        });

        modelBuilder.Entity<enter_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__enter_ty__3213E83FCBAB158F");
        });

        modelBuilder.Entity<enterprise_name>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__enterpri__3213E83FDA8300E8");
        });

        modelBuilder.Entity<ep_kpi_datum>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ep_kpi_d__3213E83FDD915878");
        });

        modelBuilder.Entity<ep_kpi_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ep_kpi_i__3213E83F2E24E921");
        });

        modelBuilder.Entity<ep_kpi_report>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ep_kpi_r__3213E83F5999606C");
        });

        modelBuilder.Entity<ep_kpi_sub_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ep_kpi_s__3213E83FC72ADFEE");
        });

        modelBuilder.Entity<ep_kpi_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__ep_kpi_t__3213E83F4A53ACF5");
        });

        modelBuilder.Entity<expert_datum>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__expert_d__3213E83FF72C1A09");

            entity.Property(e => e.id).ValueGeneratedNever();
            entity.Property(e => e.uuid).IsFixedLength();
        });

        modelBuilder.Entity<factory_name>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__factory___3213E83FB91AFE5C");
        });

        modelBuilder.Entity<industrial_area_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__industri__3213E83F90524761");
        });

        modelBuilder.Entity<psm_kpi_datum>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__psm_kpi___3213E83F071A7ADA");
        });

        modelBuilder.Entity<psm_kpi_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__psm_kpi___3213E83F3433DA4A");
        });

        modelBuilder.Entity<psm_kpi_report>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__psm_kpi___3213E83F1D396739");
        });

        modelBuilder.Entity<psm_kpi_sub_item>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__psm_kpi___3213E83F0A262AA3");
        });

        modelBuilder.Entity<psm_kpi_template>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__psm_kpi___3213E83F20125115");
        });

        modelBuilder.Entity<result_datum>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__result_d__3213E83FA4B86494");
        });

        modelBuilder.Entity<result_report>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__result_r__3213E83F7140F8AF");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<result_suggestion_type>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__result_s__3213E83FCE84C254");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<set_system>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__set_syst__3213E83FDFFCAD4D");
        });

        modelBuilder.Entity<suggest_category>(entity =>
        {
            entity.HasKey(e => e.id)
                .HasName("PK__suggest___3213E83E1F6751D9")
                .IsClustered(false);

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<suggest_item>(entity =>
        {
            entity.HasKey(e => e.id)
                .HasName("PK__suggest___3213E83E519D8385")
                .IsClustered(false);

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<suggest_type>(entity =>
        {
            entity.HasKey(e => e.id)
                .HasName("PK__suggest___3213E83EC634A44C")
                .IsClustered(false);

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        modelBuilder.Entity<township_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__township__3213E83F1A4DF9D1");
        });

        modelBuilder.Entity<user_info>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__user_inf__3213E83F26124D97");

            entity.Property(e => e.salt).IsFixedLength();
        });

        modelBuilder.Entity<work_position>(entity =>
        {
            entity.HasKey(e => e.id).HasName("PK__work_pos__3213E83FD1103E86");

            entity.Property(e => e.id).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
