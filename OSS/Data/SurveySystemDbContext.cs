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

        public DbSet<Student> Students { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public SurveySystemDbContext(DbContextOptions<SurveySystemDbContext> options) : base(options)
        {
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //many-to-many
            // composite PK
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


            //do not cascade when Specialty is deleted
            modelBuilder.Entity<Student>()
                .HasOne(s=>s.Specialty)
                .WithMany(s => s.Students)
                .OnDelete(DeleteBehavior.SetNull);
            //do not cascade when Survey is deleted
            modelBuilder.Entity<Answer>()
                .HasOne(a => a.Survey)
                .WithMany(s => s.Answers)
                .OnDelete(DeleteBehavior.Restrict);
            //many-to-many
            // composite PK
            //modelBuilder.Entity<SurveyQuestion>()
            //    .HasKey(t => new { t.QuestionId, t.SurveyId });

            //modelBuilder.Entity<SurveyQuestion>()
            //    .HasOne(sq => sq.Survey)
            //    .WithMany(q => q.SurveyQuestions)
            //    .HasForeignKey(sq => sq.SurveyId);

            //modelBuilder.Entity<SurveyQuestion>()
            //    .HasOne(qs => qs.Question)
            //    .WithMany(s => s.QuestionSurveys)
            //    .HasForeignKey(qs => qs.QuestionId);

            // composite key
            modelBuilder.Entity<Answer>()
                .HasKey(t => new { t.StudentId, t.SurveyId, t.QuestionId, t.LecturerId, t.SubjectId});

        }

        public DbSet<OSS.Models.SurveySystemModels.LecturerSubject> LecturerSubject { get; set; }

       // public DbSet<OSS.Models.SurveySystemModels.SurveyQuestion> SurveyQuestion { get; set; }

        public DbSet<OSS.Models.SurveySystemModels.QuestionAnswer> QuestionAnswer { get; set; }
    }

}
