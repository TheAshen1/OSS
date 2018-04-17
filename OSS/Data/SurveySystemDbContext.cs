using Microsoft.EntityFrameworkCore;
using OSS.Models.SurveySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Data
{
    public class SurveySystemDbContext : DbContext
    {
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Specialty> Specialties { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Survey> Surveys { get; set; }


        public SurveySystemDbContext(DbContextOptions<SurveySystemDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //many-to-many
            modelBuilder.Entity<LecturerSubject>()
           .HasKey(t => new { t.LecturerId, t.SubjectId });

            modelBuilder.Entity<LecturerSubject>()
                .HasOne(ls => ls.Lecturer)
                .WithMany(l => l.LecturerSubjects)
                .HasForeignKey(ls => ls.LecturerId);

            modelBuilder.Entity<LecturerSubject>()
                .HasOne(ls => ls.Subject)
                .WithMany(s => s.SubjectLecturers)
                .HasForeignKey(ls => ls.SubjectId);

            //many-to-many
            modelBuilder.Entity<SurveyQuestion>()
            .HasKey(t => new { t.QuestionId, t.SurveyId });

            modelBuilder.Entity<SurveyQuestion>()
                .HasOne(sq => sq.Survey)
                .WithMany(q => q.SurveyQuestions)
                .HasForeignKey(sq => sq.SurveyId);

            modelBuilder.Entity<SurveyQuestion>()
                .HasOne(qs => qs.Question)
                .WithMany(s => s.QuestionSurveys)
                .HasForeignKey(qs => qs.QuestionId);
        }

        public DbSet<OSS.Models.SurveySystemModels.LecturerSubject> LecturerSubject { get; set; }
    }

}
