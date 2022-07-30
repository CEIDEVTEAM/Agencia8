using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataAccess.Context
{
    public partial class Agencia_8Context : DbContext
    {
        public Agencia_8Context()
        {
        }

        public Agencia_8Context(DbContextOptions<Agencia_8Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; } 
        public virtual DbSet<ContactPerson> ContactPerson { get; set; } 
        public virtual DbSet<DecisionParam> DecisionParam { get; set; } 
        public virtual DbSet<DecisionSupport> DecisionSupport { get; set; } 
        public virtual DbSet<Dependet> Dependet { get; set; } 
        public virtual DbSet<ExternalDependent> ExternalDependent { get; set; } 
        public virtual DbSet<LtAuthentication> LtAuthentication { get; set; } 
        public virtual DbSet<LtCandidate> LtCandidate { get; set; } 
        public virtual DbSet<LtDependent> LtDeLtDependentpendents { get; set; } 
        public virtual DbSet<LtShopData> LtShopData { get; set; } 
        public virtual DbSet<Permission> Permission { get; set; } 
        public virtual DbSet<PermissionRole> PermissionRole { get; set; } 
        public virtual DbSet<ProcedureStep> ProcedureStep { get; set; } 
        public virtual DbSet<Role> Role { get; set; } 
        public virtual DbSet<ShopData> ShopData { get; set; } 
        public virtual DbSet<StepType> StepType { get; set; } 
        public virtual DbSet<User> Users { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-TM6H1UB\\SQLEXPRESS;Database=Agencia_8;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdDecisionSupport)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Decision_Support");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.PersonalAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Address");

                entity.Property(e => e.PersonalDocument)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Document");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.HasOne(d => d.IdDecisionSupportNavigation)
                    .WithMany(p => p.Candidates)
                    .HasForeignKey(d => d.IdDecisionSupport)
                    .HasConstraintName("FK_Decision_Support");
            });

            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("Contact_Person");

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Bond)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.HasOne(d => d.NumberNavigation)
                    .WithOne(p => p.ContactPerson)
                    .HasForeignKey<ContactPerson>(d => d.Number)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Dependet_Number");
            });

            modelBuilder.Entity<DecisionParam>(entity =>
            {
                entity.ToTable("Decision_Params");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.ActiveFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Active_Flag");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow).HasColumnType("datetime");

                entity.Property(e => e.Value)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DecisionSupport>(entity =>
            {
                entity.ToTable("Decision_Support");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RecomendedDecision)
                    .HasMaxLength(10)
                    .HasColumnName("Recomended_Decision")
                    .IsFixedLength();
            });

            modelBuilder.Entity<Dependet>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("Dependet");

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PatentNamber)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Patent_Namber");

                entity.Property(e => e.PersonalAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Address");

                entity.Property(e => e.PersonalDocument)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Document");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.Property(e => e.UpdUserId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Upd_UserId");
            });

            modelBuilder.Entity<ExternalDependent>(entity =>
            {
                entity.HasKey(e => e.Number);

                entity.ToTable("External_Dependents");

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyNumber)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Agency_Number");

                entity.Property(e => e.Condition)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");
            });

            modelBuilder.Entity<LtAuthentication>(entity =>
            {
                entity.ToTable("LT_Authentication");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.UserId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("User_Id");
            });

            modelBuilder.Entity<LtCandidate>(entity =>
            {
                entity.ToTable("LT_Candidate");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.IdDecisionSupport)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Decision_Support");

                entity.Property(e => e.IdUser)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_User");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.PersonalAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Address");

                entity.Property(e => e.PersonalDocument)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Document");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LtDependent>(entity =>
            {
                entity.ToTable("LT_Dependent");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdUser)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_User");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.PatentNamber)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Patent_Namber");

                entity.Property(e => e.PersonalAddress)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Address");

                entity.Property(e => e.PersonalDocument)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Personal_Document");

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LtShopData>(entity =>
            {
                entity.ToTable("LT_Shop_Data");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.IdShopData)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Shop_Data");

                entity.Property(e => e.IdUser)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_User");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumberDependent)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Number_Dependent");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ShopType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Shop_Type");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Feature)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PermissionRole>(entity =>
            {
                entity.HasKey(e => new { e.IdRole, e.IdPermission });

                entity.ToTable("Permission_Role");

                entity.Property(e => e.IdRole)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Role");

                entity.Property(e => e.IdPermission)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Permission");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Permission");

                entity.HasOne(d => d.IdRole1)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Role");
            });

            modelBuilder.Entity<ProcedureStep>(entity =>
            {
                entity.ToTable("Procedure_Step");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Comment)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.IdStepType)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Step_Type");

                entity.Property(e => e.IdUser)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_User");

                entity.Property(e => e.IsComplete)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Is_Complete");

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ProcedureStep)
                    .HasForeignKey<ProcedureStep>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Candidate");

                entity.HasOne(d => d.Id1)
                    .WithOne(p => p.ProcedureStep)
                    .HasForeignKey<ProcedureStep>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_User");

                entity.HasOne(d => d.IdStepTypeNavigation)
                    .WithMany(p => p.ProcedureSteps)
                    .HasForeignKey(d => d.IdStepType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Step_Type");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ShopData>(entity =>
            {
                entity.ToTable("Shop_Data");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NumberDependent)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Number_Dependent");

                entity.Property(e => e.Phone)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ShopType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Shop_Type");

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.ShopData)
                    .HasForeignKey(d => d.IdCandidate)
                    .HasConstraintName("FK_Shop_Data_Id_Candidate");

                entity.HasOne(d => d.NumberDependentNavigation)
                    .WithMany(p => p.ShopData)
                    .HasForeignKey(d => d.NumberDependent)
                    .HasConstraintName("FK_Number_Dependet");
            });

            modelBuilder.Entity<StepType>(entity =>
            {
                entity.ToTable("Step_Type");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AactiveFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Aactive_Flag");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IdRole)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Role");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdRole)
                    .HasConstraintName("FK_User_Id_Role");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
