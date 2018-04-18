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
        }
    }
}
