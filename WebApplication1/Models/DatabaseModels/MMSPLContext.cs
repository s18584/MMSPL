using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebApplication1.models.databasemodels
{
    public partial class MMSPLContext : DbContext
    {
        public IConfiguration Configuration { get; }

        public MMSPLContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public MMSPLContext(DbContextOptions<MMSPLContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Campaign> Campaigns { get; set; }
        public virtual DbSet<Contractor> Contractors { get; set; }
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<CostType> CostTypes { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerCampaign> CustomerCampaigns { get; set; }
        public virtual DbSet<CustomerSendingAction> CustomerSendingActions { get; set; }
        public virtual DbSet<DocType> DocTypes { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<Promocode> Promocodes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SendingAction> SendingActions { get; set; }
        public virtual DbSet<SendingActionType> SendingActionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultDatabaseApplication"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.HasAnnotation("Relational:Collation", "Polish_CI_AS");

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("Campaign");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdContractor).HasColumnName("idContractor");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.HasOne(d => d.IdContractorNavigation)
                    .WithMany(p => p.Campaigns)
                    .HasForeignKey(d => d.IdContractor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Campaign_Contractor");

                entity.Property(e => e.Budget).HasColumnName("budget");
            });

            modelBuilder.Entity<Contractor>(entity =>
            {
                entity.ToTable("Contractor");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Nip)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nip");
            });

            modelBuilder.Entity<Cost>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.IdCampaign).HasColumnName("idCampaign");

                entity.Property(e => e.IdContractor).HasColumnName("idContractor");

                entity.Property(e => e.IdCostType).HasColumnName("idCostType");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Campaign_Costs");

                entity.HasOne(d => d.IdContractorNavigation)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.IdContractor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Costs_Contractor");

                entity.HasOne(d => d.IdCostTypeNavigation)
                    .WithMany(p => p.Costs)
                    .HasForeignKey(d => d.IdCostType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Costs_CostType");
            });

            modelBuilder.Entity<CostType>(entity =>
            {
                entity.ToTable("CostType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("address");

                entity.Property(e => e.BirthDate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthDate");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("firstName");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lastName");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("postCode");
            });

            modelBuilder.Entity<CustomerCampaign>(entity =>
            {
                entity.HasKey(e => new { e.IdCustomer, e.IdCampaign })
                    .HasName("CustomerCampaign_pk");

                entity.ToTable("CustomerCampaign");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.IdCampaign).HasColumnName("idCampaign");

                entity.Property(e => e.OkToEmail).HasColumnName("okToEmail");

                entity.Property(e => e.OkToThirdParty).HasColumnName("okToThirdParty");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.CustomerCampaigns)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerCampaign_Campaign");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.CustomerCampaigns)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerCampaign_Customer");
            });

            modelBuilder.Entity<CustomerSendingAction>(entity =>
            {
                entity.ToTable("CustomerSendingAction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCustomer).HasColumnName("idCustomer");

                entity.Property(e => e.IdSendingAction).HasColumnName("idSendingAction");

                entity.Property(e => e.IsSend).HasColumnName("isSend");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.CustomerSendingActions)
                    .HasForeignKey(d => d.IdCustomer)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerSurvey_Customer");

                entity.HasOne(d => d.IdSendingActionNavigation)
                    .WithMany(p => p.CustomerSendingActions)
                    .HasForeignKey(d => d.IdSendingAction)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CustomerSurvey_Survey");
            });

            modelBuilder.Entity<DocType>(entity =>
            {
                entity.ToTable("DocType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(90)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCampaign).HasColumnName("idCampaign");

                entity.Property(e => e.IdDocType).HasColumnName("idDocType");

                entity.Property(e => e.Path)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("path");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Document_Campaign");

                entity.HasOne(d => d.IdDocTypeNavigation)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Document_DocType");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Promocode>(entity =>
            {
                entity.ToTable("Promocode");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCampaign).HasColumnName("idCampaign");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("value");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.Promocodes)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Promocode_Campaign");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<SendingAction>(entity =>
            {
                entity.ToTable("SendingAction");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.IdCampaign).HasColumnName("idCampaign");

                entity.Property(e => e.IdSendingActionType).HasColumnName("idSendingActionType");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.EmailSubject)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("emailSubject");

                entity.Property(e => e.EmailBody)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("emailBody");

                entity.HasOne(d => d.IdCampaignNavigation)
                    .WithMany(p => p.SendingActions)
                    .HasForeignKey(d => d.IdCampaign)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Survey_Campaign");

                entity.HasOne(d => d.IdSendingActionTypeNavigation)
                    .WithMany(p => p.SendingActions)
                    .HasForeignKey(d => d.IdSendingActionType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SendingAction_SendingActionType");
            });

            modelBuilder.Entity<SendingActionType>(entity =>
            {
                entity.ToTable("SendingActionType");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("login");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("password");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.IdUser, e.IdRole })
                    .HasName("UserRoles_pk");

                entity.Property(e => e.IdUser).HasColumnName("idUser");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.HasOne(d => d.IdRoleNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdRole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoles_Role");

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("UserRoles_Users");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
