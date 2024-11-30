﻿// <auto-generated />
using System;
using AnimalsShelterBackend.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AnimalsShelterBackend.Migrations
{
    [DbContext(typeof(ShelterAppContext))]
    partial class ShelterAppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.Property<Guid>("FavouriteAnimalsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FavouritedByUsersId")
                        .HasColumnType("uuid");

                    b.HasKey("FavouriteAnimalsId", "FavouritedByUsersId");

                    b.HasIndex("FavouritedByUsersId");

                    b.ToTable("AnimalUser");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Animals.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Color")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("HealthConditions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ImagesSources")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LivingCondition")
                        .HasColumnType("integer");

                    b.Property<string>("MainImageSource")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ReceiptDate")
                        .HasColumnType("integer");

                    b.Property<int>("Sex")
                        .HasColumnType("integer");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Size")
                        .HasColumnType("integer");

                    b.Property<string>("TemperFeatures")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Wool")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Animals");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.ArticleViews.View", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ArticleId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("Views");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Articles.Article", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BodyMarkDown")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastUpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("MainImageSrc")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Tag")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("authorId");

                    b.Property<int>("ViewsCount")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Contributors.Contributor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("ContributorType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Contributors");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Notifications.Notification", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("Clickable")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("LinkTo")
                        .HasColumnType("text");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.ShelterUser.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AvatarSrc")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("Phone")
                        .HasColumnType("bigint");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Tokens.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("ExpiresAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("NotificationUser", b =>
                {
                    b.Property<Guid>("NotificationsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uuid");

                    b.HasKey("NotificationsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("NotificationUser");
                });

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.HasOne("AnimalsShelterBackend.Domain.Animals.Animal", null)
                        .WithMany()
                        .HasForeignKey("FavouriteAnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalsShelterBackend.Domain.ShelterUser.User", null)
                        .WithMany()
                        .HasForeignKey("FavouritedByUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.ArticleViews.View", b =>
                {
                    b.HasOne("AnimalsShelterBackend.Domain.Articles.Article", null)
                        .WithMany("Views")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalsShelterBackend.Domain.ShelterUser.User", null)
                        .WithMany("ArticleViews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Articles.Article", b =>
                {
                    b.HasOne("AnimalsShelterBackend.Domain.ShelterUser.User", "User")
                        .WithMany("Articles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Tokens.RefreshToken", b =>
                {
                    b.HasOne("AnimalsShelterBackend.Domain.ShelterUser.User", null)
                        .WithOne("RefreshToken")
                        .HasForeignKey("AnimalsShelterBackend.Domain.Tokens.RefreshToken", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("NotificationUser", b =>
                {
                    b.HasOne("AnimalsShelterBackend.Domain.Notifications.Notification", null)
                        .WithMany()
                        .HasForeignKey("NotificationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimalsShelterBackend.Domain.ShelterUser.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.Articles.Article", b =>
                {
                    b.Navigation("Views");
                });

            modelBuilder.Entity("AnimalsShelterBackend.Domain.ShelterUser.User", b =>
                {
                    b.Navigation("ArticleViews");

                    b.Navigation("Articles");

                    b.Navigation("RefreshToken");
                });
#pragma warning restore 612, 618
        }
    }
}
