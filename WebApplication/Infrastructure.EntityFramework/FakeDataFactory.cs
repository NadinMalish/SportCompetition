using Domain.Entities;
<<<<<<< HEAD
using System.Reflection;
=======
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3

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
<<<<<<< HEAD
            new ApplicationStatus { Id = 1, Name = "Редактируется" },
            new ApplicationStatus { Id = 2, Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = 3, Name = "Отклонена администрацией" }
        };

        public List<Potent> Potents { get; } = new();
        public List<Domain.Entities.EventInfo> EventsInfo { get; } = new();
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
                    Pass = "Ivan",
                    DatReg = DateTime.Now,
                    Deleted = false
                }
            };

            EventsInfo = new List<Domain.Entities.EventInfo>
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
                    OrganizerId = Potents.First().Id,
                    IsDeleted = false
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
                    Event = EventsInfo.First(),
                    EventId = EventsInfo.First().Id,
                    IsDeleted = false
                }
            };


=======
            new ApplicationStatus { Id = rnd.Next(), Name = "Редактируется" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Подтверждена капитаном команды" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Отклонена капитаном" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Отклонена администрацией" }
        };

        public List<Role> Roles { get; } = new()
        {
            new Role { Id = rnd.Next(), Name = "Участник" },
            new Role { Id = rnd.Next(), Name = "Судья" },
            new Role { Id = rnd.Next(), Name = "Секретарь" },
            new Role { Id = rnd.Next(), Name = "Спортсмен" }
        };

        public List<EventParticipant> EventParticipants { get; }

        public FakeDataFactory()
        {
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
            EventParticipants = new List<EventParticipant>
            {
                // Заявка в статусе редактирования
                new EventParticipant
                {
                    Id = rnd.Next(),
<<<<<<< HEAD
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
                    IsDeleted = false,
                    Status = ApplicationStatuses[1],
                    ParticipantCompetition = Competitions.First()
=======
                    RoleId = Roles[0].Id,
                    StatusId = ApplicationStatuses[0].Id,
                    IsCaptainConfirmed = false,
                    IsActual = false,
                    Comment = null,
                    DateTime = DateTime.UtcNow.AddDays(-7),
                    IsDeleted = false,
                    Role = Roles[0],
                    Status = ApplicationStatuses[0],
                    // Будущие поля (пока не используются):
                    // PotentId = _userId1,
                    // EventCompetitionId = _eventId1,
                    // TeamId = _teamId1,
                    SetStatusId = _userId1
                },
            
                // Заявка, подтвержденная капитаном
                new EventParticipant
                {
                    Id = rnd.Next(),
                    RoleId = Roles[0].Id,
                    StatusId = ApplicationStatuses[1].Id,
                    IsCaptainConfirmed = true,
                    IsActual = true,
                    Comment = null,
                    DateTime = DateTime.UtcNow.AddDays(-5),
                    IsDeleted = false,
                    Role = Roles[0],
                    Status = ApplicationStatuses[1],
                    SetStatusId = _userId2
                },
            
                // Заявка судьи, подтвержденная администрацией
                new EventParticipant
                {
                    Id = rnd.Next(),
                    RoleId = Roles[1].Id,
                    StatusId = ApplicationStatuses[2].Id,
                    IsCaptainConfirmed = false,
                    IsActual = true,
                    Comment = null,
                    DateTime = DateTime.UtcNow.AddDays(-3),
                    IsDeleted = false,
                    Role = Roles[1],
                    Status = ApplicationStatuses[2],
                    SetStatusId = _userId2
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                },
            
                // Отклоненная заявка
                new EventParticipant
                {
                    Id = rnd.Next(),
<<<<<<< HEAD
                    ApplicationStatusId = ApplicationStatuses[2].Id,
                    DateTime = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false,
                    Status = ApplicationStatuses[2],
                    ParticipantCompetition = Competitions.First()
=======
                    RoleId = Roles[3].Id,
                    StatusId = ApplicationStatuses[3].Id,
                    IsCaptainConfirmed = false,
                    IsActual = false,
                    Comment = "Не соответствует требованиям",
                    DateTime = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false,
                    Role = Roles[3],
                    Status = ApplicationStatuses[3],
                    SetStatusId = _userId1
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                }
            };

            // Связываем участников с ролями и статусами
            foreach (var participant in EventParticipants)
            {
<<<<<<< HEAD
=======
                participant.Role?.EventParticipants?.Add(participant);
>>>>>>> 58758fae546987d020c423c087ef4ea0f96087c3
                participant.Status?.EventParticipants?.Add(participant);
            }
        }
    }
}
