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
            new ApplicationStatus { Id = rnd.Next(), Name = "Подтверждена администрацией" },
            new ApplicationStatus { Id = rnd.Next(), Name = "Отклонена администрацией" }
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
                    ApplicationStatusId = ApplicationStatuses[0].Id,
                    DateTime = DateTime.UtcNow.AddDays(-7),
                    Status = ApplicationStatuses[0]
                },
            
                // Заявка, подтвержденная администрацией
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[1].Id,
                    DateTime = DateTime.UtcNow.AddDays(-3),
                    IsDeleted = false,
                    Status = ApplicationStatuses[1],
                },
            
                // Отклоненная заявка
                new EventParticipant
                {
                    Id = rnd.Next(),
                    ApplicationStatusId = ApplicationStatuses[2].Id,
                    DateTime = DateTime.UtcNow.AddDays(-1),
                    IsDeleted = false,
                    Status = ApplicationStatuses[2]
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
