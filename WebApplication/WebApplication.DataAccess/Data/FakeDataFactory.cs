using WebApplication.Core.Domain;

namespace WebApplication.DataAccess.Data
{
    internal class FakeDataFactory
    {
        public List<ApplicationStatus> ApplicationStatuses { get; } = new()
    {
        new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Name = "Редактируется",
            EventParticipants = new List<EventParticipant>()
        },
        new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Name = "Подтверждена капитаном команды",
            EventParticipants = new List<EventParticipant>()
        },
        new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Name = "Подтверждена администрацией",
            EventParticipants = new List<EventParticipant>()
        },
        new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Name = "Отклонена капитаном",
            EventParticipants = new List<EventParticipant>()
        },
        new ApplicationStatus
        {
            Id = Guid.NewGuid(),
            Name = "Отклонена администрацией",
            EventParticipants = new List<EventParticipant>()
        }
    };

         public List<Role> Roles { get; } = new()
    {
        new Role
        {
            Id = Guid.NewGuid(),
            Name = "Участник",
            EventParticipants = new List<EventParticipant>()
        },
        new Role
        {
            Id = Guid.NewGuid(),
            Name = "Судья",
            EventParticipants = new List<EventParticipant>()
        },
        new Role
        {
            Id = Guid.NewGuid(),
            Name = "Секретарь",
            EventParticipants = new List<EventParticipant>()
        },
        new Role
        {
            Id = Guid.NewGuid(),
            Name = "Спортсмен",
            EventParticipants = new List<EventParticipant>()
        }
    };


        public List<EventParticipant> EventParticipants { get; }

        public FakeDataFactory()
        {
            var potentId = Guid.NewGuid();       
            var adminId = Guid.NewGuid();          
            var captainId = Guid.NewGuid();        
            var eventId = Guid.NewGuid();
            var teamId = Guid.NewGuid();

            EventParticipants = new List<EventParticipant>
        {
            new EventParticipant
            {
                Id = Guid.NewGuid(),
                Comment = "",
                DateTime = DateTime.UtcNow.AddDays(-7),
                IsDeleted = false,
                PotentId = potentId,
                SetStatusId = potentId,
                EventCompletitionId = eventId,
                TeamId = teamId,
                Role = Roles[0],
                RoleId = Roles[0].Id,
                Status = ApplicationStatuses[0],
                StatusId = ApplicationStatuses[0].Id
            },
            new EventParticipant
            {
                Id = Guid.NewGuid(),
                Comment = "",
                DateTime = DateTime.UtcNow.AddDays(-6),
                IsDeleted = false,
                PotentId = potentId,
                SetStatusId = captainId,
                EventCompletitionId = eventId,
                TeamId = teamId,
                Role = Roles[0],
                RoleId = Roles[0].Id,
                Status = ApplicationStatuses[1],
                StatusId = ApplicationStatuses[1].Id
            },
            new EventParticipant
            {
                Id = Guid.NewGuid(),
                Comment = "",
                DateTime = DateTime.UtcNow.AddDays(-5),
                IsDeleted = false,
                PotentId = potentId,
                SetStatusId = adminId,
                EventCompletitionId = eventId,
                TeamId = teamId,
                Role = Roles[1],
                RoleId = Roles[1].Id,
                Status = ApplicationStatuses[2],
                StatusId = ApplicationStatuses[2].Id
            },
            new EventParticipant
            {
                Id = Guid.NewGuid(),
                Comment = "",
                DateTime = DateTime.UtcNow.AddDays(-4),
                IsDeleted = false,
                PotentId = potentId,
                SetStatusId = captainId,
                EventCompletitionId = eventId,
                TeamId = teamId,
                Role = Roles[2],
                RoleId = Roles[2].Id,
                Status = ApplicationStatuses[3],
                StatusId = ApplicationStatuses[3].Id
            },
            new EventParticipant
            {
                Id = Guid.NewGuid(),
                Comment = "Недостаточно информации в заявке.",
                DateTime = DateTime.UtcNow.AddDays(-3),
                IsDeleted = false,
                PotentId = potentId,
                SetStatusId = adminId,
                EventCompletitionId = eventId,
                TeamId = teamId,
                Role = Roles[0],
                RoleId = Roles[0].Id,
                Status = ApplicationStatuses[4],
                StatusId = ApplicationStatuses[4].Id
            }
        };

            foreach (var participant in EventParticipants)
            {
                participant.Role.EventParticipants.Add(participant);
                participant.Status.EventParticipants.Add(participant);
            }
        }
    }

}
