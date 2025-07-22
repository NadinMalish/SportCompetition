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

            // 2. Мероприятия (75 штук, реалистичные названия + даты)
            Events.AddRange(GenerateEvents(Potents.First(), 75));

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
                "Турнир по киберспорту"
            };

            var start = new DateTime(2025, 7, 1);
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
