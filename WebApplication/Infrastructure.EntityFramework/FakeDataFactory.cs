using Domain.Entities;
using EventInfo = Domain.Entities.EventInfo;

namespace Infrastructure.EntityFramework
{
    public class FakeDataFactory
    {
        private static readonly Random rnd = new();

        #region Collections
        public List<ApplicationStatus> ApplicationStatuses { get; } = new()
        {
            new ApplicationStatus { Id = 1, Name = "Редактируется" },
            new ApplicationStatus { Id = 2, Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = 3, Name = "Отклонена администрацией" }
        };

        public List<Potent> Potents { get; } = new();
        public List<EventInfo> Events { get; } = new();
        public List<Competition> Competitions { get; } = new();
        public List<EventParticipant> EventParticipants { get; } = new();
        #endregion

        public FakeDataFactory()
        {
            // 1) Организаторы (10 штук)
            Potents.AddRange(GeneratePotents(10));

            // 2) Мероприятия (100 штук, реалистичные названия + даты + описания, случайные организаторы)
            Events.AddRange(GenerateEvents(Potents, 100));

            // 3) Состязания для каждого мероприятия (1–4 на событие, с описанием)
            Competitions.AddRange(GenerateCompetitionsForEvents(Events));

            // 4) Несколько тестовых заявок, распределённых по разным состязаниям
            EventParticipants.AddRange(GenerateParticipants(Competitions));
        }

        #region Generators

        private static IEnumerable<Potent> GeneratePotents(int count)
        {
            string[] firstnames = { "Иван", "Пётр", "Сергей", "Алексей", "Мария", "Анна", "Ольга", "Дмитрий", "Николай", "Елена", "Юлия", "Татьяна", "Кирилл", "Артур", "Игорь" };
            string[] lastnames = { "Иванов", "Петров", "Сидоров", "Кузнецов", "Смирнов", "Попов", "Соколов", "Михайлов", "Новиков", "Фёдоров", "Егоров", "Васильев", "Поляков", "Гордеев", "Орлов" };
            string[] surnames = { "Иванович", "Петрович", "Сергеевич", "Алексеевич", "Дмитриевич", "Николаевич", "Анатольевич", "Владимирович", "Юрьевич", "Борисович", "Михайлович", "Олегович", "Викторович", "Васильевич", "Геннадьевич" };

            for (int i = 0; i < count; i++)
            {
                var fn = firstnames[i % firstnames.Length];
                var ln = lastnames[i % lastnames.Length];
                var sur = surnames[i % surnames.Length];

                yield return new Potent
                {
                    Id = rnd.Next(),
                    Lastname  = ln,
                    Firstname = fn,
                    Surname   = sur,
                    DateBirth = new DateOnly(1985 + rnd.Next(0, 15), rnd.Next(1, 13), rnd.Next(1, 28)),
                    Gender    = (i % 2 == 0) ? "M" : "F",
                    Email     = $"{fn.ToLower()}.{ln.ToLower()}@example.com",
                    Login     = $"{fn}{i + 1}",
                    DatReg    = DateTime.Now.AddDays(-rnd.Next(0, 720))
                };
            }
        }

        private static IEnumerable<EventInfo> GenerateEvents(List<Potent> organizers, int count)
        {
            string[] baseNames =
            {
                "Шахматный турнир",
                "Турнир по волейболу",
                "Кросс нации",
                "Летний заплыв",
                "Математический конкурс",
                "Олимпийские надежды (лёгкая атлетика)",
                "Фестиваль настольных игр",
                "Велоралли",
                "Соревнования по стрельбе",
                "Турнир по киберспорту",
                "Зимний марафон",
                "Кубок по настольному теннису",
                "Фестиваль науки",
                "Слёт юных техников",
                "Турнир по баскетболу",
                "Фестиваль уличного футбола",
                "Гонки на велосипедах",
                "Турнир по бадминтону",
                "Конкурс юных художников",
                "Фестиваль робототехники",
                "Весенний забег",
                "Турнир по дзюдо",
                "Лига интеллектуалов",
                "Фестиваль дронов",
                "Соревнования по спортивному ориентированию",
                "Летний лагерь чемпионов",
                "Кубок по мини-футболу",
                "Открытая олимпиада по программированию",
                "Осенний турнир по настольным играм",
                "Спартакиада школьников",
                "Битва умов (интеллектуальные игры)",
                "Соревнования по плаванию",
                "Городской велопробег",
                "Открытый турнир по шашкам",
                "Фестиваль современного танца",
                "Инженерный хакатон",
                "Научные бои",
                "Фестиваль дрон-рейсинга",
                "Кубок по армрестлингу",
                "Молодёжный турнир по регби",
                "Конкурс ораторского мастерства",
                "Интеллектуальный квест",
                "Гонка героев (полоса препятствий)",
                "Соревнования по спортивной гимнастике",
                "Фестиваль моделизма",
                "Открытый кубок по скалолазанию"
            };

            var startBase = DateTime.Now.AddDays(-10); // базовая точка для разброса дат
            var list = new List<EventInfo>(count);

            for (int i = 0; i < count; i++)
            {
                var name = baseNames[rnd.Next(baseNames.Length)];
                var begin = startBase.AddDays(rnd.Next(0, 120)) // ближайшие 4 месяца
                                     .Date
                                     .AddHours(rnd.Next(8, 19)) // старт между 08:00 и 18:00
                                     .AddMinutes(rnd.Next(0, 4) * 15);
                var end = begin.AddHours(rnd.Next(2, 10)); // 2–9 часов длительность
                var regOpen = begin.AddDays(-rnd.Next(15, 60)); // регистрация за 15–60 дней
                var organizer = organizers[rnd.Next(organizers.Count)];

                var ev = new EventInfo
                {
                    Id = rnd.Next(),
                    Name = name,
                    Description = $"Описание мероприятия «{name}». Организатор: {organizer.Lastname} {organizer.Firstname}. " +
                                  $"Регистрация открыта с {regOpen:d}. Мероприятие пройдёт {begin:d}.",
                    BeginDate = begin,
                    EndDate = end,
                    RegistrationDate = regOpen,
                    RegistryDate = DateTime.Now,
                    IsCompleted = end < DateTime.Now,
                    Organizer = organizer,
                    OrganizerId = organizer.Id
                };

                list.Add(ev);
            }

            return list.OrderBy(e => e.BeginDate);
        }

        private static IEnumerable<Competition> GenerateCompetitionsForEvents(IEnumerable<EventInfo> events)
        {
            string[] compNames =
            {
                // Лёгкая атлетика
                "Забег в мешках",
                "Эстафета 4×100",
                "Бег на 100 м",
                "Бег на 400 м",
                "Бег на 1500 м",
                "Бег на 5000 м",
                "Марафон",
                "Полумарафон",
                "Спортивная ходьба",
                "Бег с препятствиями",
                "Тройной прыжок",
                "Прыжки в длину",
                "Прыжки в высоту",
                "Метание диска",
                "Метание копья",
                "Метание молота",
                "Толкание ядра",

                // Водные виды
                "Плавание 100 м",
                "Плавание 200 м",
                "Плавание 400 м",
                "Плавание на спине",
                "Плавание баттерфляем",
                "Эстафета по плаванию",
                "Водное поло",
                "Синхронное плавание",
                "Прыжки в воду",
                "Заплыв на открытой воде",

                // Игровые виды
                "Футбол (мини-турнир)",
                "Баскетбол 3×3",
                "Волейбол (пляжный)",
                "Волейбол классический",
                "Гандбол",
                "Хоккей на траве",
                "Настольный теннис (одиночные)",
                "Настольный теннис (парные)",
                "Бадминтон одиночный",
                "Бадминтон парный",
                "Теннис одиночный",
                "Теннис парный",
                "Рэгби-7",
                "Американский футбол",
                "Флорбол",

                // Единоборства
                "Дзюдо до 60 кг",
                "Дзюдо свыше 60 кг",
                "Карате (ката)",
                "Карате (кумитэ)",
                "Тхэквондо",
                "Самбо",
                "Бокс любительский",
                "Кикбоксинг",
                "Рукопашный бой",
                "ММА любительские бои",
                "Сумо любительское",

                // Силовые виды
                "Гиревой жим",
                "Жим лёжа",
                "Становая тяга",
                "Приседания со штангой",
                "Кроссфит-комплекс",
                "Турник подтягивания",
                "Отжимания на брусьях",
                "Армрестлинг правой рукой",
                "Армрестлинг левой рукой",

                // Экстремальные/уличные
                "Скалолазание (трудность)",
                "Скалолазание (скорость)",
                "Паркур",
                "Гонка с препятствиями",
                "Соревнование дронов (скорость)",
                "Соревнование дронов (фристайл)",
                "BMX-фристайл",
                "Скейтбординг",
                "Роллер-гонка",
                "Велозаезд на время",
                "Маунтинбайк кросс-кантри",

                // Стрелковые
                "Стрельба из лука",
                "Стрельба из пневматической винтовки",
                "Стрельба из пистолета",
                "Лазертаг-турнир",
                "Пейнтбол-командный бой",

                // Интеллектуальные
                "Блиц-шахматы",
                "Шахматы классические",
                "Шашки русские",
                "Шашки международные",
                "Го",
                "Интеллектуальная викторина",
                "Квиз-командный",
                "Судоку-турнир",
                "Робототехнический квест",
                "Программирование (алгоритмы)",

                // Творческие и прочие
                "Танцевальный батл",
                "Художественная гимнастика",
                "Академический хор",
                "Конкурс ораторского мастерства",
                "Фотоконкурс",
                "Конкурс рисунков",
                "Фестиваль настольных игр",
                "Лего-чемпионат",
                "Инженерный хакатон",
                "Фестиваль научных проектов"
            };


            var result = new List<Competition>();

            foreach (var ev in events)
            {
                int compCount = rnd.Next(1, 5); // 1–4 состязания на мероприятие
                for (int i = 0; i < compCount; i++)
                {
                    var name = compNames[rnd.Next(compNames.Length)];

                    // Немного «двигаем» времена состязаний внутри интервала мероприятия
                    var spanHours = Math.Max(1, (int)(ev.EndDate - ev.BeginDate).TotalHours - 1);
                    var offset = rnd.Next(0, Math.Max(1, spanHours));
                    var duration = rnd.Next(1, Math.Max(2, Math.Min(6, spanHours - offset)));

                    var begin = ev.BeginDate.AddHours(offset);
                    var end = begin.AddHours(duration);

                    var comp = new Competition
                    {
                        Id = rnd.Next(),
                        Name = name,
                        Description = $"Описание состязания «{name}» в рамках мероприятия «{ev.Name}». " +
                                      $"Проводится {begin:g}–{end:g}.",
                        CompetitionType = (CompetitionTypes)rnd.Next(0, 3),
                        BeginDate = begin,
                        EndDate = end,
                        RegistryDate = DateTime.Now,
                        IsCompleted = end < DateTime.Now,
                        Event = ev,
                        EventId = ev.Id
                    };

                    // Заполним навигационную коллекцию для удобства отладки
                    ev.Competitions.Add(comp);
                    result.Add(comp);
                }
            }

            return result;
        }

        private IEnumerable<EventParticipant> GenerateParticipants(List<Competition> competitions)
        {
            var list = new List<EventParticipant>();
            if (competitions.Count == 0) return list;

            // Сгенерируем 30–60 заявок, распределив по случайным состязаниям
            int appCount = rnd.Next(30, 61);
            for (int i = 0; i < appCount; i++)
            {
                var status = ApplicationStatuses[rnd.Next(ApplicationStatuses.Count)];
                var comp = competitions[rnd.Next(competitions.Count)];

                list.Add(new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = status.Id,
                    DateTime = comp.BeginDate.AddDays(-rnd.Next(2, 30)).AddHours(rnd.Next(0, 24)),
                    Status = status,
                    ParticipantCompetition = comp,
                    ParticipantCompetitionId = comp.Id
                });
            }

            return list;
        }

        #endregion
    }
}
