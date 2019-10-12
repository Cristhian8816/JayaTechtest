using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace jayatechTest.Models
{
    public partial class UsersContext : DbContext
    {
        public UsersContext()
        {
        }

        public UsersContext(DbContextOptions<UsersContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<ChatRooms> ChatRooms { get; set; }
        public virtual DbSet<adminAccounts> adminAccounts { get; set; }
        public virtual DbSet<message> message { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {               
                optionsBuilder.UseSqlServer("Data Source = MVMOAN175A\\SQLEXPRESS; Initial Catalog = Jaya_Chat; Integrated Security = True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.id_User);

                entity.ToTable("Users");

                entity.Property(e => e.id_User)
                    .HasColumnName("id_User")
                    .ValueGeneratedNever();

                entity.Property(e => e.NickName)
                    .HasColumnName("NickName")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("Active");             
            });

            modelBuilder.Entity<ChatRooms>(entity =>
            {
                entity.HasKey(e => e.id_Room);

                entity.ToTable("ChatRooms");

                entity.Property(e => e.id_Room)
                    .HasColumnName("id_Room")
                    .ValueGeneratedNever();                               

                entity.Property(e => e.roomName)
                .HasColumnName("roomName")
                .HasMaxLength(50)
                .IsUnicode(false);

                entity.Property(e => e.deleteRoom).HasColumnName("deleteRoom");               

                entity.Property(e => e.created_at)
                    .HasColumnName("created_at")
                    .HasColumnType("date");                
            });

            modelBuilder.Entity<adminAccounts>(entity =>
            {
                entity.HasKey(e => e.id_admin);

                entity.ToTable("adminAccounts");

                entity.Property(e => e.id_admin)
                .HasColumnName("id_admin")
                .ValueGeneratedNever();

                entity.Property(e => e.userName)
                .HasColumnName("userName")
                .HasMaxLength(50)
                .IsUnicode(true);

                entity.Property(e => e.password)
                .HasColumnName("password")
                .HasMaxLength(50)
                .IsUnicode(true);
            });

            modelBuilder.Entity<message>(entity =>
            {
                entity.HasKey(e => e.id_message);

                entity.ToTable("message");

                entity.Property(e => e.id_message)
                    .HasColumnName("id_message")
                    .ValueGeneratedNever();

                entity.Property(e => e.id_User).HasColumnName("id_User");
                   
                entity.Property(e => e.id_Room).HasColumnName("id_Room");

                entity.Property(e => e.messageText)
                .HasColumnName("messageText")
                .HasMaxLength(200);

                entity.Property(e => e.msnDate)
                    .HasColumnName("msnDate")
                    .HasColumnType("date");
            });
        }
    }
}
