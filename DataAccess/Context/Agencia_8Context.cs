using System;
using System.Collections.Generic;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Context
{
    public partial class Agencia_8Context : DbContext
    {
        private IConfiguration _configuration;

        public Agencia_8Context(IConfiguration configuration, string application) : base(new DbContextOptions<Agencia_8Context>())
        {
            this._configuration = configuration;
        }

        public Agencia_8Context(DbContextOptions<Agencia_8Context> options) : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<ContactPerson> ContactPerson { get; set; }
        public virtual DbSet<DecisionParam> DecisionParam { get; set; }
        public virtual DbSet<DecisionSupport> DecisionSupport { get; set; }
        public virtual DbSet<DependentFact> DependentFact { get; set; }
        public virtual DbSet<Dependent> Dependent { get; set; }
        public virtual DbSet<ExternalDependent> ExternalDependent { get; set; }
        public virtual DbSet<LtAuthentication> LtAuthentication { get; set; }
        public virtual DbSet<LtCandidate> LtCandidate { get; set; }
        public virtual DbSet<LtDependent> LtDependent { get; set; }
        public virtual DbSet<LtShopData> LtShopData { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionRole> PermissionRole { get; set; }
        public virtual DbSet<ProcedureStep> ProcedureStep { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<ShopData> ShopData { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VCandidate> VCandidate { get; set; } = null!;
        public virtual DbSet<VDependent> VDependent { get; set; } = null!;
        public virtual DbSet<VUser> VUsers { get; set; } = null!;
        public virtual DbSet<VExCandidateDependent> VExCandidateDependent { get; set; } = null!;
        public virtual DbSet<VDependentCandidateNumber> VDependentCandidateNumbers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(this._configuration.GetConnectionString("defaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.ToTable("Candidate");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.HasOne(d => d.IdDecisionSupportNavigation)
                    .WithOne(p => p.IdNavigation)
                    .HasForeignKey<DecisionSupport>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Decision_Support");

               
            });

            modelBuilder.Entity<ContactPerson>(entity =>
            {
                entity.ToTable("Contact_Person");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Bond)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.IdDependent)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Dependent");

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

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ContactPerson)
                    .HasForeignKey<ContactPerson>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_Person_Id_Candidate");

                entity.HasOne(d => d.Id1)
                    .WithOne(p => p.ContactPerson)
                    .HasForeignKey<ContactPerson>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_Person_Id_Dependent");
            });

            modelBuilder.Entity<DecisionParam>(entity =>
            {
                entity.ToTable("Decision_Params");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ActiveFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Active_Flag");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false);

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.RecomendedDecision)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Recomended_Decision");
            });

            modelBuilder.Entity<DependentFact>(entity =>
            {
                entity.ToTable("Dependent_Facts");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddRow)
                    .HasColumnType("date")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FactType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Fact_Type");

                entity.Property(e => e.IdDependent)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Dependent");

                entity.Property(e => e.UpdUserId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Upd_UserId");

                entity.HasOne(d => d.IdDependentNavigation)
                    .WithMany(p => p.DependentFacts)
                    .HasForeignKey(d => d.IdDependent)
                    .HasConstraintName("FK_Dependent_Facts_Dependet");
            });

            modelBuilder.Entity<Dependent>(entity =>
            {
                entity.ToTable("Dependet");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ActiveFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Active_Flag");

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

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.Property(e => e.UpdUserId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Upd_UserId");
            });

            modelBuilder.Entity<ExternalDependent>(entity =>
            {
                entity.HasKey(e => new { e.Number, e.Name });

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Token)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("User_Id");
            });

            modelBuilder.Entity<LtCandidate>(entity =>
            {
                entity.ToTable("LT_Candidate");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LtDependent>(entity =>
            {
                entity.ToTable("LT_Dependent");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.StepType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Step_Type");

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.Property(e => e.UpdUser)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Upd_User");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ProcedureStep)
                    .HasForeignKey<ProcedureStep>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Id_Candidate");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

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

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.IdCandidate)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Candidate");

                entity.Property(e => e.IdDependent)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Dependent");

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

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.ShopData)
                    .HasForeignKey<ShopData>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shop_Data_Id_Dependent");

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithMany(p => p.ShopData)
                    .HasForeignKey(d => d.IdCandidate)
                    .HasConstraintName("FK_Shop_Data_Id_Candidate");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.ActiveFlag)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("Active_Flag");

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

            modelBuilder.Entity<VCandidate>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Candidate");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Bond)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.IdShopData)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Shop_Data");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.LastNameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name_Contact_Person");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Contact_Person");

                entity.Property(e => e.NameShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Shop_Data");

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

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

                entity.Property(e => e.PhoneContactPerson)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Contact_Person");

                entity.Property(e => e.PhoneShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Shop_Data");

                entity.Property(e => e.ShopType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Shop_Type");

                entity.Property(e => e.Status)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.IdContactPerson)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Contact_Person");

                entity.Property(e => e.IdDecisionSupport)
                  .HasColumnType("numeric(10, 0)")
                  .HasColumnName("Id_Decision_Support");
                
            });

            modelBuilder.Entity<VDependent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Dependent");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.Bond)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.IdShopData)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Shop_Data");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.LastNameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name_Contact_Person");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Contact_Person");

                entity.Property(e => e.NameShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Shop_Data");

                entity.Property(e => e.Neighborhood)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.PatentNamber)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Patent_Namber");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

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

                entity.Property(e => e.PhoneContactPerson)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Contact_Person");

                entity.Property(e => e.PhoneShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Shop_Data");

                entity.Property(e => e.ShopType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Shop_Type");

                entity.Property(e => e.IdContactPerson)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Contact_Person");
            });

            modelBuilder.Entity<VUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_User");

                entity.Property(e => e.Id)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Role_Name");

                entity.Property(e => e.UpdRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Upd_Row");

                entity.Property(e => e.UserName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("User_Name");
            });

            modelBuilder.Entity<VExCandidateDependent>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_Ex_CandidateDependent");

                entity.Property(e => e.AddRow)
                    .HasColumnType("datetime")
                    .HasColumnName("Add_Row");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.BirthDate)
                    .HasColumnType("date")
                    .HasColumnName("Birth_Date");

                entity.Property(e => e.BondContactPerson)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Bond_Contact_Person");

                entity.Property(e => e.Condition)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Id).HasColumnType("numeric(10, 0)");

                entity.Property(e => e.IdContactPerson)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Contact_Person");

                entity.Property(e => e.IdShopData)
                    .HasColumnType("numeric(10, 0)")
                    .HasColumnName("Id_Shop_Data");

                entity.Property(e => e.LastName)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name");

                entity.Property(e => e.LastNameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Last_Name_Contact_Person");

                entity.Property(e => e.Latitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Longitude)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Marital_Status");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NameContactPerson)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Contact_Person");

                entity.Property(e => e.NameShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Name_Shop_Data");

                entity.Property(e => e.Neighborhood)
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

                entity.Property(e => e.PhoneContactPerson)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Contact_Person");

                entity.Property(e => e.PhoneShopData)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Phone_Shop_Data");

                entity.Property(e => e.ShopType)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("Shop_Type");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VDependentCandidateNumber>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_DependentCandidateNumbers");

                entity.Property(e => e.Number).HasColumnType("numeric(10, 0)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
