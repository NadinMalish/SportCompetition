using Domain.Entities;

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
            EventParticipants = new List<EventParticipant>
            {
                // Заявка в статусе редактирования
                new EventParticipant
                {
                    Id = rnd.Next(),
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
                },
            
                // Отклоненная заявка
                new EventParticipant
                {
                    Id = rnd.Next(),
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
                }
            };

            // Связываем участников с ролями и статусами
            foreach (var participant in EventParticipants)
            {
                participant.Role?.EventParticipants?.Add(participant);
                participant.Status?.EventParticipants?.Add(participant);
            }
        }
    }
}
