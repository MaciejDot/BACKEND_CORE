using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BackendCore.Data
{
    public partial class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(ApplicationDatabaseOptionsBuilder builder)
            :base(builder.GetOptions())
        {
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<DictionaryOfEnglishWords> DictionaryOfEnglishWords { get; set; }
        public virtual DbSet<Exercise> Exercise { get; set; }
        public virtual DbSet<ExerciseGroup> ExerciseGroup { get; set; }
        public virtual DbSet<ExerciseJoinExerciseGroup> ExerciseJoinExerciseGroup { get; set; }
        public virtual DbSet<ExerciseJoinMuscleGroup> ExerciseJoinMuscleGroup { get; set; }
        public virtual DbSet<MuscleGroup> MuscleGroup { get; set; }
        public virtual DbSet<Photos> Photos { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Thread> Thread { get; set; }
        public virtual DbSet<Thumbnails> Thumbnails { get; set; }
        public virtual DbSet<Thumbnails1> Thumbnails1 { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Article>(entity =>
            {
                entity.ToTable("Article", "Article");

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Content).IsRequired();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Article__AuthorI__06CD04F7");

                entity.HasOne(d => d.Thumbnail)
                    .WithMany(p => p.Article)
                    .HasForeignKey(d => d.ThumbnailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Article__Thumbna__07C12930");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<DictionaryOfEnglishWords>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Exercise>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Exercise__737584F7B770840E");

                entity.ToTable("Exercise", "Wiki");

                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExerciseGroup>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__Exercise__737584F772DE2397");

                entity.ToTable("ExerciseGroup", "Wiki");

                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<ExerciseJoinExerciseGroup>(entity =>
            {
                entity.ToTable("ExerciseJoinExerciseGroup", "Wiki");

                entity.Property(e => e.ExerciseGroupName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ExerciseName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.ExerciseGroupNameNavigation)
                    .WithMany(p => p.ExerciseJoinExerciseGroup)
                    .HasForeignKey(d => d.ExerciseGroupName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExerciseJ__Exerc__30C33EC3");

                entity.HasOne(d => d.ExerciseNameNavigation)
                    .WithMany(p => p.ExerciseJoinExerciseGroup)
                    .HasForeignKey(d => d.ExerciseName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExerciseJ__Exerc__2FCF1A8A");
            });

            modelBuilder.Entity<ExerciseJoinMuscleGroup>(entity =>
            {
                entity.ToTable("ExerciseJoinMuscleGroup", "Wiki");

                entity.Property(e => e.ExerciseGroupName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.ExerciseName)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.HasOne(d => d.ExerciseGroupNameNavigation)
                    .WithMany(p => p.ExerciseJoinMuscleGroup)
                    .HasForeignKey(d => d.ExerciseGroupName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExerciseJ__Exerc__3E1D39E1");

                entity.HasOne(d => d.ExerciseNameNavigation)
                    .WithMany(p => p.ExerciseJoinMuscleGroup)
                    .HasForeignKey(d => d.ExerciseName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ExerciseJ__Exerc__3D2915A8");
            });

            modelBuilder.Entity<MuscleGroup>(entity =>
            {
                entity.HasKey(e => e.Name)
                    .HasName("PK__MuscleGr__737584F71FD25F35");

                entity.ToTable("MuscleGroup", "Wiki");

                entity.Property(e => e.Name).ValueGeneratedNever();
            });

            modelBuilder.Entity<Photos>(entity =>
            {
                entity.ToTable("Photos", "Article");

                entity.Property(e => e.Content).IsRequired();
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post", "Forum");

                entity.Property(e => e.Answear).HasMaxLength(4000);

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__AuthorId__208CD6FA");

                entity.HasOne(d => d.Thread)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.ThreadId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Post__ThreadId__2180FB33");
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section", "Wiki");

                entity.Property(e => e.Exercise)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Name).HasMaxLength(450);

                entity.HasOne(d => d.ExerciseNavigation)
                    .WithMany(p => p.Section)
                    .HasForeignKey(d => d.Exercise)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__Exercis__2B0A656D");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("Subject", "Forum");

                entity.Property(e => e.Title).HasMaxLength(300);

                entity.HasOne(d => d.Thumbnail)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.ThumbnailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Subject__Thumbna__17F790F9");
            });

            modelBuilder.Entity<Thread>(entity =>
            {
                entity.ToTable("Thread", "Forum");

                entity.Property(e => e.AuthorId)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Question).HasMaxLength(4000);

                entity.Property(e => e.Title).HasMaxLength(300);

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Thread)
                    .HasForeignKey(d => d.AuthorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Thread__AuthorId__1BC821DD");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Thread)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Thread__SubjectI__1CBC4616");
            });

            modelBuilder.Entity<Thumbnails>(entity =>
            {
                entity.ToTable("Thumbnails", "Article");

                entity.Property(e => e.Content).IsRequired();
            });

            modelBuilder.Entity<Thumbnails1>(entity =>
            {
                entity.ToTable("Thumbnails", "Forum");

                entity.Property(e => e.Content).IsRequired();
            });
        }
    }
}
