using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using Sveit.Models;

namespace Sveit.API.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name = TCC")
        {
            // Inicializa o banco de dados com registros caso o modelo seja alterado.
            Database.SetInitializer(new DatabaseInitializer());
        }

        public virtual DbSet<Apply> Applies { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<EndorseSkill> EndorseSkills { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GamePlatform> GamePlatforms { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<PlayerSkill> PlayerSkills { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<TeamPlayer> TeamPlayers { get; set; }
        public virtual DbSet<Vacancy> Vacancies { get; set; }

        public override int SaveChanges()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is EntityBase && (x.State == EntityState.Added || x.State == EntityState.Modified));
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((EntityBase)entity.Entity).CreatedAt = DateTime.Now;
                    ((EntityBase)entity.Entity).UpdatedAt = DateTime.Now;
                }
                else
                {
                    Entry(((EntityBase)entity.Entity)).Property(x => x.CreatedAt).IsModified = false;
                    ((EntityBase)entity.Entity).UpdatedAt = DateTime.Now;
                }

            }
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            //Database.SetInitializer<DatabaseContext>(null);
            // Realiza o DROP no banco de dados caso o modelo sofra alterações.
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
            base.OnModelCreating(modelBuilder);
        }
    }
}