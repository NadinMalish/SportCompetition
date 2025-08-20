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
            // 1. Организаторы
            Potents.AddRange(GeneratePotents());

            // 2. Мероприятия (100 штук, реалистичные названия + даты)
            Events.AddRange(GenerateEvents(Potents.First(), 100));

            // 3. Одно соревнование, связанное с самым первым мероприятием
            Competitions.AddRange(GenerateCompetitions(Events.First()));

            // 4. Несколько тестовых заявок
            EventParticipants.AddRange(GenerateParticipants());
        }

        #region Generators
        private static IEnumerable<Potent> GeneratePotents()
        {
            yield return new Potent
            {
                Id = rnd.Next(),
                Lastname = "Иванов",
                Firstname = "Иван",
                Surname = "Иванович",
                DateBirth = new DateOnly(2000, 1, 1),
                Gender = "M",
                Email = "ivan@example.com",
                Login = "Ivan",
                DatReg = DateTime.Now
            };
        }

        private static IEnumerable<EventInfo> GenerateEvents(Potent organizer, int count)
        {
            string[] baseNames =
            {
                "Шахматный турнир",
                "Турнир по волейболу",
                "Кросс нации",
                "Летний заплыв",
                "Математический конкурс",
                "Олимпийские надежды (легкая атлетика)",
                "Фестиваль настольных игр",
                "Велоралли",
                "Соревнования по стрельбе",
                "Турнир по киберспорту",
                "Зимний марафон",
                "Кубок по настольному теннису",
                "Фестиваль науки",
                "Слет юных техников",
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
                "Молодежный турнир по регби",
                "Конкурс ораторского мастерства",
                "Интеллектуальный квест",
                "Гонка героев (спортивная полоса препятствий)",
                "Соревнования по спортивной гимнастике",
                "Фестиваль моделизма",
                "Открытый кубок по скалолазанию"
            };


            var start = DateTime.Now.AddDays(-3);
            var list = new List<EventInfo>(count);

            for (int i = 0; i < count; i++)
            {
                var date = start.AddDays(rnd.Next(0, 90)); // интервал 90 дней
                var name = baseNames[rnd.Next(baseNames.Length)];

                list.Add(new EventInfo
                {
                    Id = rnd.Next(),
                    Name = name,
                    BeginDate = date,
                    EndDate = date,
                    RegistrationDate = date.AddDays(-rnd.Next(15, 60)), // окно регистрации 15‑60 дней до начала
                    RegistryDate = DateTime.Now,
                    IsCompleted = date < DateTime.Today,
                    Organizer = organizer,
                    OrganizerId = organizer.Id
                });
            }

            return list.OrderBy(e => e.BeginDate);
        }

        private static IEnumerable<Competition> GenerateCompetitions(EventInfo relatedEvent)
        {
            yield return new Competition
            {
                Id = rnd.Next(),
                Name = "Забег в мешках",
                CompetitionType = CompetitionTypes.Single,
                BeginDate = relatedEvent.BeginDate,
                EndDate = relatedEvent.EndDate,
                RegistryDate = DateTime.Now,
                IsCompleted = false,
                Event = relatedEvent,
                EventId = relatedEvent.Id
            };
        }

        private IEnumerable<EventParticipant> GenerateParticipants()
        {
            return new List<EventParticipant>
            {
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[0].Id,
                    DateTime = DateTime.UtcNow.AddDays(-7),
                    Status = ApplicationStatuses[0],
                    ParticipantCompetition = Competitions.First()
                },
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[1].Id,
                    DateTime = DateTime.UtcNow.AddDays(-3),
                    Status = ApplicationStatuses[1],
                    ParticipantCompetition = Competitions.First()
                },
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[2].Id,
                    DateTime = DateTime.UtcNow.AddDays(-1),
                    Status = ApplicationStatuses[2],
                    ParticipantCompetition = Competitions.First()
                }
            };
        }
        #endregion
    }
}
