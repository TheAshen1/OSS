using OSS.Models.SurveySystemModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OSS.Data
{
    public static class Seeder
    {
        public static void Initialize(SurveySystemDbContext context)
        {
            if (!context.Faculties.Any())
            {
                context.Faculties.AddRange(
                    new Faculty
                    {
                        FullName = "Факультет экономики и права",
                        ShortName = "ЭП",
                        Specialties = new List<Specialty>
                        {
                            new Specialty { FullName = "Экономика" , SpecialtyCode = "051"},
                            new Specialty { FullName = "Журналистика" , SpecialtyCode = "061"},
                            new Specialty { FullName = "Менеджмент" , SpecialtyCode = "073"},
                            new Specialty { FullName = "Образовательные, педагогические науки" , SpecialtyCode = "011"},
                            new Specialty { FullName = "Общественное управление и администрирование" , SpecialtyCode = "074"}

                        }

                    },
                    new Faculty
                    {
                        FullName = "Факультет экономической информатики",
                        ShortName = "ЭИ",
                        Specialties = new List<Specialty>
                        {
                            new Specialty { FullName = "Экономика" , SpecialtyCode = "051"},
                            new Specialty { FullName = "Инженерия програмного обеспечения" , SpecialtyCode = "121"},
                            new Specialty { FullName = "Компьютерные науки" , SpecialtyCode = "122"},
                            new Specialty { FullName = "Информационные системы и технологии" , SpecialtyCode = "126"},
                            new Specialty { FullName = "Издательство и полиграфия" , SpecialtyCode = "186"}
                        }
                    },
                    new Faculty { FullName = "Факультет менеджмента и маркетинга", ShortName = "МИМ" },
                    new Faculty { FullName = "Факультет международных экономических отношений", ShortName = "МЭО" },
                    new Faculty { FullName = "Факультет консалтинга и международного бизнеса", ShortName = "КИМБ" },
                    new Faculty { FullName = "Финансовый факультет", ShortName = "ФФ" },
                    new Faculty { FullName = "Факультет подготовки иностранных граждан", ShortName = "ФСФ" }
                    );
            }
            context.SaveChanges();
            if (!context.Surveys.Any() )
            {
                context.Surveys.Add(
                    new Survey
                    {
                        Name = "Best lecturer",
                        Questions = new List<Question>()
                        {
                            new Question { Text = "First question" },
                            new Question { Text = "Second question" },
                            new Question { Text = "Third question" },
                            new Question { Text = "Fourth question" },
                            new Question { Text = "Fifth question" }
                        }
                    }
                    );
            }
            context.SaveChanges();



            if (!context.Lecturers.Any() && !context.Subjects.Any())
            {
                var lecturers = new[]
                {
                    new Lecturer() { FirstName = "Иван" },
                    new Lecturer() { FirstName = "Николай" },
                    new Lecturer() { FirstName = "Алексей" },
                    new Lecturer() { FirstName = "Анна" },
                    new Lecturer() { FirstName = "Светлана" },
                    new Lecturer() { FirstName = "Татьяна" }
                };

                var subjects = new[]
                {
                    new Subject() { FullName = "Матан"},
                    new Subject() { FullName = "Физика"},
                    new Subject() { FullName = "Английский" },
                    new Subject() { FullName = "Базы Данных" },
                    new Subject() { FullName = "Экономика" }
                };

                context.LecturerSubject.AddRange(
                    new List<LecturerSubject>()
                    {
                        new LecturerSubject(){ Lecturer = lecturers[0], Subject = subjects[0]},
                        new LecturerSubject(){ Lecturer = lecturers[1], Subject = subjects[1]},
                        new LecturerSubject(){ Lecturer = lecturers[1], Subject = subjects[0]},
                        new LecturerSubject(){ Lecturer = lecturers[2], Subject = subjects[3]},
                        new LecturerSubject(){ Lecturer = lecturers[3], Subject = subjects[2]},
                        new LecturerSubject(){ Lecturer = lecturers[4], Subject = subjects[2]},
                        new LecturerSubject(){ Lecturer = lecturers[5], Subject = subjects[4]}
                    }
                    );

            }

            //var id = context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            //var questions = context.Questions.AsEnumerable();



            context.SaveChanges();
        }
    }
}
