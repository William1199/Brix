using Data.Configuration;
using Data.Entities;
using Data.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContext
{
    public class BuildingConstructDbContext : IdentityDbContext<User, Role, Guid>
    {
        public BuildingConstructDbContext()
        {
        }
        public BuildingConstructDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            //{
            //    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            //}
            modelBuilder.ApplyConfiguration(new ContractorPostConfiguration());
            modelBuilder.ApplyConfiguration(new MaterialStoreConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ContractorConfiguration());
            modelBuilder.ApplyConfiguration(new SkillConfiguration());
            modelBuilder.ApplyConfiguration(new VerifyConfiguration());
            modelBuilder.ApplyConfiguration(new BuilderSkillConfiguration());
            modelBuilder.ApplyConfiguration(new ContractorPostSkillConfiguration());
            modelBuilder.ApplyConfiguration(new TypeConfiguration());
            modelBuilder.ApplyConfiguration(new BuilderConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new GroupMemberConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoriesConfiguration());
            modelBuilder.ApplyConfiguration(new AppliedPostConfiguration());
            modelBuilder.ApplyConfiguration(new PostCommitmentsConfigurations());
            modelBuilder.ApplyConfiguration(new ContractorPostTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CartConfiguration());
            modelBuilder.ApplyConfiguration(new SaveConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfigurations());
            modelBuilder.ApplyConfiguration(new BillDetailConfiguration());
            modelBuilder.ApplyConfiguration(new NotificationConfigurations());
            modelBuilder.ApplyConfiguration(new WorkerConstructionTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfigurations());
            modelBuilder.ApplyConfiguration(new PostInviteConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new OtherConfiguration());
            modelBuilder.ApplyConfiguration(new SizeConfiguration());
            modelBuilder.ApplyConfiguration(new ColorConfiguration());
            modelBuilder.ApplyConfiguration(new QuizConfiguration());
            modelBuilder.ApplyConfiguration(new QuestionConfiguration());
            modelBuilder.ApplyConfiguration(new AnswerConfiguration());
            modelBuilder.ApplyConfiguration(new UserAnswerConfiguration());
            modelBuilder.ApplyConfiguration(new ReportProblemConfiguration());

            modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins").HasKey(x => x.UserId);
            modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles").HasKey(x => new { x.UserId, x.RoleId });
            modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens").HasKey(x => x.UserId);

            modelBuilder.Seed();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Builder> Builders { get; set; }
        public DbSet<BuilderSkill> BuilderSkills { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Verify> Verifies { get; set; }
        public DbSet<MaterialStore> MaterialStores { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<WorkerContructionType> WorkerContructionTypes { get; set; }
        public DbSet<ContractorPost> ContractorPosts { get; set; }
        public DbSet<ContractorPostSkill> ContractorPostSkills { get; set; }
        public DbSet<Entities.Type> Types { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<ProductCategories> ProductCategories { get; set; }
        public DbSet<AppliedPost> AppliedPosts { get; set; }
        public DbSet<PostCommitment> PostCommitments { get; set; }
        public DbSet<ContractorPostType> ContractorPostTypes { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Save> Saves { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Notification> Notifcations { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<PostInvite> PostInvites { get; set; }
        public DbSet<Payment> Payments { get; set; }

        public DbSet<Color> Colors { get; set; }
        public DbSet<ProductSize> Sizes { get; set; }
        public DbSet<Other> Others { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
    }
}
