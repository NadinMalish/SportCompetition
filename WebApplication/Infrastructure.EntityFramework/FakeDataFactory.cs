using Domain.Entities;
using System.Reflection;

namespace Infrastructure.EntityFramework
{
    public class FakeDataFactory
    {
        private static Random rnd = new Random();
        // Фиксированные int для согласованности данных
        private static readonly int _userId1 = 1000001;
        private static readonly int _userId2 = 2000002;
        private static readonly int _eventId1 = 3000003;
        private static readonly int _teamId1 = 4000004;

        public List<ApplicationStatus> ApplicationStatuses { get; } = new()
        {
            new ApplicationStatus { Id = 1, Name = "Редактируется" },
            new ApplicationStatus { Id = 2, Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = 3, Name = "Отклонена администрацией" }
        };

        public List<Potent> Potents { get; } = new();
        public List<Domain.Entities.EventInfo> Events { get; } = new();
        public List<Competition> Competitions { get; } = new();
        public List<EventParticipant> EventParticipants { get; } = new();

        public FakeDataFactory()
        {
            Potents = new List<Potent>
            {
                new Potent
                {
                    Id = rnd.Next(),
                    Lastname = "Иванов",
                    Firstname = "Иван",
                    Surname = "Иванович",
                    DateBirth = new DateOnly(2000,1,1),
                    Gender = "M",
                    Email = "",
                    Login = "Ivan",
                    DatReg = DateTime.Now
                }
            };

            Events = new List<Domain.Entities.EventInfo>
            {
                new Domain.Entities.EventInfo
                {
                    Id = rnd.Next(),
                    Name = "Весёлые старты",
                    BeginDate = new DateTime(2025,6,28),
                    EndDate = new DateTime(2025,6,28),
                    RegistrationDate = new DateTime(2025,6,28),
                    RegistryDate = new DateTime(),
                    IsCompleted = true,
                    Organizer = Potents.First(),
                    OrganizerId = Potents.First().Id
                }
            };

            Competitions = new List<Competition>
            {
                new Competition
                {
                    Id = rnd.Next(),
                    Name = "Забег в мешках",
                    CompetitionType = CompetitionTypes.Single,
                    BeginDate = new DateTime(2025,6,28),
                    EndDate = new DateTime(2025,6,28),
                    RegistryDate = new DateTime(),
                    IsCompleted = true,
                    Event = Events.First(),
                    EventId = Events.First().Id
                }
            };


            EventParticipants = new List<EventParticipant>
            {
                // Заявка в статусе редактирования
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[0].Id,
                    DateTime = DateTime.UtcNow.AddDays(-7),
                    Status = ApplicationStatuses[0],
                    ParticipantCompetition = Competitions.First()
                },
            
                // Заявка, подтвержденная администрацией
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[1].Id,
                    DateTime = DateTime.UtcNow.AddDays(-3),
                    Status = ApplicationStatuses[1],
                    ParticipantCompetition = Competitions.First()
                },
            
                // Отклоненная заявка
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[2].Id,
                    DateTime = DateTime.UtcNow.AddDays(-1),
                    Status = ApplicationStatuses[2],
                    ParticipantCompetition = Competitions.First()
                }
            };

            // Связываем участников с ролями и статусами
            foreach (var participant in EventParticipants)
            {
                participant.Status?.EventParticipants?.Add(participant);
            }
        }
    }
}
