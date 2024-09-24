using System;
using System.Collections.Generic;
using CommonLibrary.Models.EF;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using File = CommonLibrary.Models.EF.File;

namespace CommonLibrary.Models;

public partial class ChatDbContext : DbContext
{
    public ChatDbContext()
    {
    }

    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dialogue> Dialogues { get; set; }

    public virtual DbSet<File> Files { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Icon> Icons { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Session> Sessions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VerificationCode> VerificationCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=chat_db;User Id=postgres;Password=local;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dialogue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("dialogues_pkey");

            entity.ToTable("dialogues");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<File>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("files_pkey");

            entity.ToTable("files");

            entity.HasIndex(e => e.MessageId, "idx_files_message");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FileName).HasColumnName("file_name");
            entity.Property(e => e.FilePath)
                .HasMaxLength(256)
                .HasColumnName("file_path");
            entity.Property(e => e.MessageId).HasColumnName("message_id");

            entity.HasOne(d => d.Message).WithMany(p => p.Files)
                .HasForeignKey(d => d.MessageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_files_message");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.DialogueId).HasName("groups_pkey");

            entity.ToTable("groups");

            entity.Property(e => e.DialogueId)
                .ValueGeneratedNever()
                .HasColumnName("dialogue_id");
            entity.Property(e => e.DialogueName)
                .HasMaxLength(50)
                .HasColumnName("dialogue_name");
            entity.Property(e => e.IconPath)
                .HasMaxLength(256)
                .HasColumnName("icon_path");

            entity.HasOne(d => d.Dialogue).WithOne(p => p.Group)
                .HasForeignKey<Group>(d => d.DialogueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_groups_dialogue");
        });

        modelBuilder.Entity<Icon>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("icons_pkey");

            entity.ToTable("icons");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("user_id");
            entity.Property(e => e.Path)
                .HasMaxLength(256)
                .HasColumnName("path");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("messages_pkey");

            entity.ToTable("messages");

            entity.HasIndex(e => e.DialogueId, "idx_messages_dialogue");

            entity.HasIndex(e => e.UserId, "idx_messages_user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DialogueId).HasColumnName("dialogue_id");
            entity.Property(e => e.HasFiles).HasColumnName("has_files");
            entity.Property(e => e.SentAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("sent_at");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Dialogue).WithMany(p => p.Messages)
                .HasForeignKey(d => d.DialogueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_dialogue");

            entity.HasOne(d => d.User).WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_messages_user");
        });

        modelBuilder.Entity<Session>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("sessions_pkey");

            entity.ToTable("sessions");

            entity.HasIndex(e => new { e.UserId, e.Ip }, "unq_user_id_ip").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .HasColumnName("ip");
            entity.Property(e => e.IsLoggedIn).HasColumnName("is_logged_in");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Sessions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_user_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_unique").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("updated_at");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
            entity.Property(e => e.VerifiedEmail).HasColumnName("verified_email");

            entity.HasMany(d => d.Dialogues).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "DialoguesUser",
                    r => r.HasOne<Dialogue>().WithMany()
                        .HasForeignKey("DialogueId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_dialogues_users_dialogue"),
                    l => l.HasOne<User>().WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fk_dialogues_users_user"),
                    j =>
                    {
                        j.HasKey("UserId", "DialogueId").HasName("dialogues_users_pkey");
                        j.ToTable("dialogues_users");
                        j.HasIndex(new[] { "DialogueId" }, "idx_dialogues_users_dialogue");
                        j.HasIndex(new[] { "UserId" }, "idx_dialogues_users_user");
                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("DialogueId").HasColumnName("dialogue_id");
                    });
        });

        modelBuilder.Entity<VerificationCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("verification_codes_pkey");

            entity.ToTable("verification_codes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('recovery_codes_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(6)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("timestamp with time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.Used)
                .HasDefaultValue(false)
                .HasColumnName("used");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
