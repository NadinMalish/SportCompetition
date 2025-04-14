using Domain.Entities;

namespace Infrastructure.EntityFramework
{
    public class FakeDataFactory
    {
        // Фиксированные GUID для согласованности данных
        private static readonly Guid _userId1 = Guid.Parse("00000001-0000-0000-0000-000000000000");
        private static readonly Guid _userId2 = Guid.Parse("00000002-0000-0000-0000-000000000000");
        private static readonly Guid _eventId1 = Guid.Parse("00000003-0000-0000-0000-000000000000");
        private static readonly Guid _teamId1 = Guid.Parse("00000004-0000-0000-0000-000000000000");

        public List<ApplicationStatus> ApplicationStatuses { get; } = new()
        {
            new ApplicationStatus { Id = Guid.NewGuid(), Name = "Редактируется" },
            new ApplicationStatus { Id = Guid.NewGuid(), Name = "Подтверждена капитаном команды" },
            new ApplicationStatus { Id = Guid.NewGuid(), Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = Guid.NewGuid(), Name = "Отклонена капитаном" },
            new ApplicationStatus { Id = Guid.NewGuid(), Name = "Отклонена администрацией" }
        };

        public List<Role> Roles { get; } = new()
        {
            new Role { Id = Guid.NewGuid(), Name = "Участник" },
            new Role { Id = Guid.NewGuid(), Name = "Судья" },
            new Role { Id = Guid.NewGuid(), Name = "Секретарь" },
            new Role { Id = Guid.NewGuid(), Name = "Спортсмен" }
        };

        public List<EventParticipant> EventParticipants { get; }

        public FakeDataFactory()
        {
            EventParticipants = new List<EventParticipant>
            {
                // Заявка в статусе редактирования
                new EventParticipant
                {
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
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
                    Id = Guid.NewGuid(),
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
