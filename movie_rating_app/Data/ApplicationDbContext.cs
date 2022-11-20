﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using movie_rating_app.Models;

namespace movie_rating_app.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Actor> Actors { get; set; } = null!;
        public virtual DbSet<Creator> Creators { get; set; } = null!;
        public virtual DbSet<Favourite> Favourites { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        public virtual DbSet<MovieCreator> MovieCreators { get; set; } = null!;
        public virtual DbSet<MoviesCast> MoviesCasts { get; set; } = null!;
        public virtual DbSet<Nationality> Nationalities { get; set; } = null!;
        public virtual DbSet<Review> Reviews { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.Property(e => e.RoleName).HasMaxLength(256);

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Actors)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_Actors_Nationalities");
            });

            modelBuilder.Entity<Creator>(entity =>
            {
                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.CustomRole).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(256);

                entity.Property(e => e.LastName).HasMaxLength(256);

                entity.HasOne(d => d.Nationality)
                    .WithMany(p => p.Creators)
                    .HasForeignKey(d => d.NationalityId)
                    .HasConstraintName("FK_Creators_Nationalities");
            });

            modelBuilder.Entity<Favourite>(entity =>
            {
                entity.HasIndex(e => e.MovieId, "IX_Favourites_MovieId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.MovieId);

     /*           entity.HasOne(d => d.User)
                    .WithMany(p => p.Favourites)
                    .HasForeignKey(d => d.UserId);*/
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Movies)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK_Movies_GenreId");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Movie)
                    .HasPrincipalKey<MoviesCast>(p => p.MovieId)
                    .HasForeignKey<Movie>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movies_MoviesCasts");
            });

            modelBuilder.Entity<MovieCreator>(entity =>
            {
                entity.HasOne(d => d.Creator)
                    .WithMany(p => p.MovieCreators)
                    .HasForeignKey(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieCreators_Creators1");

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.MovieCreators)
                    .HasForeignKey(d => d.MovieId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MovieCreators_Movies1");
            });

            modelBuilder.Entity<MoviesCast>(entity =>
            {
                entity.HasIndex(e => e.MovieId, "IX_MoviesCast")
                    .IsUnique();

                entity.HasOne(d => d.Actor)
                    .WithMany(p => p.MoviesCasts)
                    .HasForeignKey(d => d.ActorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MoviesCasts_Actors");
            });

            modelBuilder.Entity<Nationality>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasIndex(e => e.MovieId, "IX_Reviews_MovieId");

                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Movie)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.MovieId);

  /*              entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId);*/
            });

            
        }
    }
}