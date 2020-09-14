using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OSCCWebApp
{
    public partial class OSCC_DEVContext : DbContext
    {
        public OSCC_DEVContext()
        {
        }

        public OSCC_DEVContext(DbContextOptions<OSCC_DEVContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authors> Authors { get; set; }
        public virtual DbSet<Bibliography> Bibliography { get; set; }
        public virtual DbSet<Books> Books { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Comments2> Comments2 { get; set; }
        public virtual DbSet<Editors> Editors { get; set; }
        public virtual DbSet<FAppCrit> FAppCrit { get; set; }
        public virtual DbSet<FCommentary> FCommentary { get; set; }
        public virtual DbSet<FContext> FContext { get; set; }
        public virtual DbSet<FDifferences> FDifferences { get; set; }
        public virtual DbSet<FReconstruction> FReconstruction { get; set; }
        public virtual DbSet<FTranslations> FTranslations { get; set; }
        public virtual DbSet<FragmentReferencer> FragmentReferencer { get; set; }
        public virtual DbSet<Fragments> Fragments { get; set; }
        public virtual DbSet<Text> Text { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=katwijk.nolden.biz;port=3306;user=Ycreak;password=YcreakPasswd26!;database=OSCC_DEV");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authors>(entity =>
            {
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

            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasIndex(e => e.Text)
                    .HasName("Comments_Text_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineCommentaar)
                    .IsRequired()
                    .HasColumnName("lineCommentaar");

                entity.Property(e => e.LineEnd)
                    .HasColumnName("lineEnd")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineStart)
                    .HasColumnName("lineStart")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineWords)
                    .IsRequired()
                    .HasColumnName("lineWords");

                entity.Property(e => e.Pages).IsRequired();

                entity.Property(e => e.Source).IsRequired();

                entity.Property(e => e.Text).HasColumnType("int(11)");

                entity.HasOne(d => d.TextNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.Text)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comments_Text_FK");
            });

            modelBuilder.Entity<Comments2>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Text)
                    .HasName("Comments2_Text_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineCommentaar).HasColumnName("lineCommentaar");

                entity.Property(e => e.LineEnd)
                    .HasColumnName("lineEnd")
                    .HasColumnType("int(11)");

                entity.Property(e => e.LineStart)
                    .HasColumnName("lineStart")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Text).HasColumnType("int(11)");

                entity.HasOne(d => d.TextNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.Text)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Comments2_Text_FK");
            });

            modelBuilder.Entity<Editors>(entity =>
            {
                entity.HasIndex(e => new { e.Book, e.EditorName })
                    .HasName("Editors_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.DefaultEditor)
                    .HasColumnName("defaultEditor")
                    .HasColumnType("int(11)");

                entity.Property(e => e.EditorName)
                    .IsRequired()
                    .HasColumnName("editorName")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Editors)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("Editors_Books_FK");
            });

            modelBuilder.Entity<FAppCrit>(entity =>
            {
                entity.ToTable("F_AppCrit");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_AppCrit_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithOne(p => p.FAppCrit)
                    .HasForeignKey<FAppCrit>(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_AppCrit_FragmentReferencer_FK");
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
                    .HasConstraintName("F_Commentary_FragmentReferencer_FK");
            });

            modelBuilder.Entity<FContext>(entity =>
            {
                entity.ToTable("F_Context");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Context_FragmentReferencer_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ContextAuthor).HasColumnType("tinytext");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithMany(p => p.FContext)
                    .HasForeignKey(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Context_FragmentReferencer_FK");
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
                    .HasConstraintName("F_Differences_FragmentReferencer_FK");
            });

            modelBuilder.Entity<FReconstruction>(entity =>
            {
                entity.ToTable("F_Reconstruction");

                entity.HasIndex(e => e.Fragment)
                    .HasName("F_Reconstruction_FragmentReferencer_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fragment).HasColumnType("int(11)");

                entity.HasOne(d => d.FragmentNavigation)
                    .WithMany(p => p.FReconstruction)
                    .HasForeignKey(d => d.Fragment)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("F_Reconstruction_FragmentReferencer_FK");
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
                    .HasConstraintName("F_Translations_FragmentReferencer_FK");
            });

            modelBuilder.Entity<FragmentReferencer>(entity =>
            {
                entity.HasIndex(e => new { e.Book, e.Editor, e.FragmentNo })
                    .HasName("FragmentReferencer_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.Editor).HasColumnType("int(11)");

                entity.Property(e => e.FragmentNo).HasColumnType("int(11)");

                entity.Property(e => e.Published)
                    .HasColumnName("published")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Fragments>(entity =>
            {
                entity.HasIndex(e => new { e.Book, e.FragmentName, e.LineName, e.Editor })
                    .HasName("Fragments_UN")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.Editor).HasColumnType("int(11)");

                entity.Property(e => e.FragmentContent)
                    .IsRequired()
                    .HasColumnName("fragmentContent");

                entity.Property(e => e.FragmentName)
                    .IsRequired()
                    .HasColumnName("fragmentName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LineName)
                    .IsRequired()
                    .HasColumnName("lineName")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Published)
                    .HasColumnName("published")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("tinytext");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Fragments)
                    .HasForeignKey(d => d.Book)
                    .HasConstraintName("Fragments_Books_FK");
            });

            modelBuilder.Entity<Text>(entity =>
            {
                entity.HasIndex(e => e.Book)
                    .HasName("Text_Books_FK");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Book).HasColumnType("int(11)");

                entity.Property(e => e.LineContent)
                    .HasColumnName("lineContent")
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LineNumber)
                    .HasColumnName("lineNumber")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.BookNavigation)
                    .WithMany(p => p.Text)
                    .HasForeignKey(d => d.Book)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Text_Books_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
