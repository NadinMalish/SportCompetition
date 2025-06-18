namespace Infrastructure.EntityFramework
{
    using Domain.Entities;
    using System;
    using System.Collections.Generic;

    public class FakeDataFactory
    {
        private static Random rnd = new Random();

        public List<ApplicationStatus> ApplicationStatuses { get; } = new()
        {
            new ApplicationStatus { Id = rnd.Next(), Name = "Редактируется" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Отклонена администрацией" }
        };

        public List<Potent> Potents { get; }
        public List<EventInfo> Events { get; }
        public List<Competition> Competitions { get; }
        public List<DocType> DocTypes { get; }
        public List<Doc> Docs { get; }
        public List<EventParticipant> EventParticipants { get; }

        public FakeDataFactory()
        {
            // Потенциальные пользователи (организаторы, редакторы)
            Potents = new List<Potent>
            {
                new Potent
                {
                    Id = rnd.Next(),
                    Firstname = "Иван", Lastname = "Иванов", Surname = "Иванович",
                    DateBirth = DateOnly.FromDateTime(new DateTime(1990, 1, 1)),
                    Gender = "М", Email = "ivanov@example.com", Login = "ivanov", Pass = "pass",
                    DatReg = DateTime.UtcNow.AddMonths(-6), Deleted = false
                },
                new Potent
                {
                    Id = rnd.Next(),
                    Firstname = "Петр", Lastname = "Петров", Surname = "Петрович",
                    DateBirth = DateOnly.FromDateTime(new DateTime(1985, 5, 15)),
                    Gender = "М", Email = "petrov@example.com", Login = "petrov", Pass = "pass",
                    DatReg = DateTime.UtcNow.AddMonths(-2), Deleted = false
                }
            };

            // Мероприятия
            Events = new List<EventInfo>
            {
                new EventInfo
                {
                    Id = rnd.Next(),
                    Name = "Весенний фестиваль",
                    BeginDate = DateTime.UtcNow.AddDays(-30),
                    EndDate = DateTime.UtcNow.AddDays(-20),
                    RegistryDate = DateTime.UtcNow.AddMonths(-2),
                    OrganizerId = Potents[0].Id,
                    Organizer = Potents[0],
                    IsDeleted = false
                }
            };

            // Состязания
            Competitions = new List<Competition>
            {
                new Competition
                {
                    Id = rnd.Next(),
                    Name = "Бег 100м",
                    BeginDate = DateTime.UtcNow.AddDays(-25),
                    EndDate = DateTime.UtcNow.AddDays(-24),
                    IsCompleted = true,
                    RegistryDate = DateTime.UtcNow.AddMonths(-2),
                    EventId = Events[0].Id,
                    Event = Events[0],
                    IsDeleted = false
                }
            };

            // Типы документов
            DocTypes = new List<DocType>
            {
                new DocType { Id = rnd.Next(), NameDocType = "Карта" },
                new DocType { Id = rnd.Next(), NameDocType = "Регламент" }
            };

            // Документы
            Docs = new List<Doc>
            {
                new Doc
                {
                    Id = rnd.Next(),
                    NameDoc = "Карта трассы",
                    FileName = "card.pdf",
                    Docum = new byte[] { 0x01, 0x02 },
                    IdDocType = DocTypes[0].Id,
                    DocType = DocTypes[0],
                    IdEvent = Events[0].Id,
                    EventInfo = Events[0],
                    IdCompetition = Competitions[0].Id,
                    Competition = Competitions[0],
                    Deleted = false
                }
            };

            // Участники мероприятия
            EventParticipants = new List<EventParticipant>
            {
                new EventParticipant
                {
                    Id = rnd.Next(),
                    RoleId = 1,
                    StatusId = ApplicationStatuses[0].Id,
                    Status = ApplicationStatuses[0],
                    DateTime = DateTime.UtcNow.AddDays(-10),
                    CompetitionId = Competitions[0].Id,
                    Competition = Competitions[0],
                    IsDeleted = false
                },
                new EventParticipant
                {
                    Id = rnd.Next(),
                    RoleId = 2,
                    StatusId = ApplicationStatuses[1].Id,
                    Status = ApplicationStatuses[1],
                    DateTime = DateTime.UtcNow.AddDays(-5),
                    CompetitionId = Competitions[0].Id,
                    Competition = Competitions[0],
                    IsDeleted = false
                }
            };

            // Связываем статусы с участниками
            foreach (var participant in EventParticipants)
            {
                participant.Status?.EventParticipants?.Add(participant);
            }

            // Добавим связанные сущности в коллекции организаторов/редакторов
            Potents[0].Events.Add(Events[0]);
            Potents[1].Competitions.Add(Competitions[0]);

            // Добавим состязание в мероприятие
            Events[0].Competitions.Add(Competitions[0]);
            Events[0].Docs.Add(Docs[0]);
        }
    }

}
