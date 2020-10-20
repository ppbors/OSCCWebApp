using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OSCCWebApp
{
    public partial class OSCC_DBContext : DbContext
    {
        public OSCC_DBContext()
        {
        }

        public OSCC_DBContext(DbContextOptions<OSCC_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Bibliography> Bibliography { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Editors> Editors { get; set; }
        public virtual DbSet<FApparatus> FApparatus { get; set; }
        public virtual DbSet<FCommentary> FCommentary { get; set; }
        public virtual DbSet<FContext> FContext { get; set; }
        public virtual DbSet<FDifferences> FDifferences { get; set; }
        public virtual DbSet<FReconstruction> FReconstruction { get; set; }
        public virtual DbSet<FTranslations> FTranslations { get; set; }
        public virtual DbSet<Fragments> Fragments { get; set; }
        public virtual DbSet<TCommentary> TCommentary { get; set; }
        public virtual DbSet<TLines> TLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=katwijk.nolden.biz;port=3306;user=Ycreak;password=YcreakPasswd26!;database=OSCC_NEW");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("Authors_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bibliography>(entity =>
            {
                entity.HasIndex(e => e.Text)
                    .HasName("Bibliography_Books_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Article).HasColumnType("tinytext");

                entity.Property(e => e.Author).HasColumnType("tinytext");

                entity.Property(e => e.Book).HasColumnType("tinytext");

                entity.Property(e => e.ChapterTitle).HasColumnType("tinytext");

                entity.Property(e => e.ConsultDate).HasColumnType("tinytext");

                entity.Property(e => e.Editors).HasColumnType("tinytext");

                entity.Property(e => e.Journal).HasColumnType("tinytext");

                entity.Property(e => e.Pages).HasColumnType("tinytext");

                entity.Property(e => e.Place).HasColumnType("tinytext");

                entity.Property(e => e.Text).HasColumnType("int(11)");

                entity.Property(e => e.Url)
                    .HasColumnName("URL")
                    .HasColumnType("tinytext");

                entity.Property(e => e.Volume).HasColumnType("tinytext");

                entity.Property(e => e.Website).HasColumnType("tinytext");

                entity.Property(e => e.Year).HasColumnType("tinytext");

                entity.HasOne(d => d.TextNavigation)
                    .WithMany(p => p.Bibliography)
                    .HasForeignKey(d => d.Text)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("Bibliography_Books_FK");
            });

            modelBuilder.Entity<Books>(entity =>
            {
                entity.HasIndex(e => e.Author)
                    .HasName("Books_Authors_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Author).HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.Author)
                    .HasConstraintName("Books_Authors_FK");
            });

            modelBuilder.Entity<Editors>(entity =>
            {
                entity.HasIndex(e => new { e.Book, e.Name })
                    .HasName("Editors_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.MainEditor).HasColumnType("tinyint(1)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Editors)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("Editors_Books_FK");
            });

            modelBuilder.Entity<FApparatus>(entity =>
            {
                entity.ToTable("F_Apparatus");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_AppCrit_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FApparatus)
                    .HasForeignKey<FApparatus>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Apparatus_Fragments_FK");
            });

            modelBuilder.Entity<FCommentary>(entity =>
            {
                entity.ToTable("F_Commentary");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Commentary_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FCommentary)
                    .HasForeignKey<FCommentary>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Commentary_Fragments_FK");
            });

            modelBuilder.Entity<FContext>(entity =>
            {
                entity.ToTable("F_Context");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Context_FragmentReferencer_FK");

                entity.HasIndex(e => new { e.Fragment, e.ContextAuthor })
                    .HasName("F_Context_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContextAuthor)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithMany(p => p.FContext)
                    .HasForeignKey(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Context_Fragments_FK");
            });

            modelBuilder.Entity<FDifferences>(entity =>
            {
                entity.ToTable("F_Differences");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Differences_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FDifferences)
                    .HasForeignKey<FDifferences>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Differences_Fragments_FK");
            });

            modelBuilder.Entity<FReconstruction>(entity =>
            {
                entity.ToTable("F_Reconstruction");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Reconstruction_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FReconstruction)
                    .HasForeignKey<FReconstruction>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Reconstruction_Fragments_FK");
            });

            modelBuilder.Entity<FTranslations>(entity =>
            {
                entity.ToTable("F_Translations");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Translations_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.Property(e => e.Translation).HasColumnType("mediumtext");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FTranslations)
                    .HasForeignKey<FTranslations>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Translations_Fragments_FK");
            });

            modelBuilder.Entity<Fragments>(entity =>
            {
                entity.HasIndex(e => e.Editor)
                    .HasName("Fragments_Editors_FK");

                entity.HasIndex(e => new { e.Book, e.FragmentName, e.LineName, e.Editor })
                    .HasName("Fragments_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.Editor).HasColumnType("int(11)");

                entity.Property(e => e.FragmentName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LineContent).IsRequired();

                entity.Property(e => e.LineName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Published).HasColumnType("int(11)");

                entity.Property(e => e.Status).HasColumnType("tinytext");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Fragments)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("Fragments_Books_FK");

                entity.HasOne(d => d.EditorNavigation)
                    .WithMany(p => p.Fragments)
                    .HasForeignKey(d => d.Editor)
                    .HasConstraintName("Fragments_Editors_FK");
            });

            modelBuilder.Entity<TCommentary>(entity =>
            {
                entity.ToTable("T_Commentary");

                entity.HasIndex(e => e.Book)
                    .HasName("Comments_Text_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.Commentary).IsRequired();

                entity.Property(e => e.LineEnd).HasColumnType("int(11)");

                entity.Property(e => e.LineStart).HasColumnType("int(11)");

                entity.Property(e => e.Pages).IsRequired();

                entity.Property(e => e.RelevantWords).IsRequired();

                entity.Property(e => e.Source).IsRequired();

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.TCommentary)
                    .HasForeignKey(d => d.Book)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("T_Commentary_Books_FK");
            });

            modelBuilder.Entity<TLines>(entity =>
            {
                entity.ToTable("T_Lines");

                entity.HasIndex(e => e.Book)
                    .HasName("Text_Books_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.LineNumber).HasColumnType("int(11)");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.TLines)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("T_Lines_Books_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
