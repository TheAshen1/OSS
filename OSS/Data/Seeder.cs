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
                    new Faculty { FullName="Факультет экономики и права", ShortName = "ЭП" },
                    new Faculty { FullName = "Факультет экономической информатики", ShortName = "ЭИ" },
                    new Faculty { FullName = "Факультет менеджмента и маркетинга", ShortName = "МИМ" },
                    new Faculty { FullName = "Факультет международных экономических отношений", ShortName = "МЭО" },
                    new Faculty { FullName = "Факультет консалтинга и международного бизнеса", ShortName = "КИМБ" },
                    new Faculty { FullName = "Финансовый факультет", ShortName = "ФФ" },
                    new Faculty { FullName = "Факультет подготовки иностранных граждан", ShortName = "ФСФ" }
                    );
            }
            context.SaveChanges();
            if (!context.Surveys.Any())
            {
                context.Surveys.Add(
                    new Survey
                    {
                        Name = "Best lecturer"
                    }
                    );
            }
            context.SaveChanges();
            if (!context.Questions.Any())
            {
                context.Questions.AddRange(
                    new Question { Text = "First question" },
                    new Question { Text = "Second question" },
                    new Question { Text = "Third question" },
                    new Question { Text = "Fifth question" },
                    new Question { Text = "Sixth question" }
                    );
            }
            context.SaveChanges();

            var id = context.Surveys.Where(s => s.Name == "Best lecturer").FirstOrDefault().SurveyId;

            var questions = context.Questions.AsEnumerable();

            foreach (Question q in questions)
            {
                context.SurveyQuestion.Add(new SurveyQuestion() { QuestionId = q.QuestionId, SurveyId = id });
            }
                

            context.SaveChanges();
        }
    }
}
