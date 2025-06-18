using Infrastructure.EntityFramework;

namespace Infrastructure.Repositories.ImplementationInfrastructure.EntityFrameworks
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(Context context)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            var fakeData = new FakeDataFactory();

            // Добавляем пользователей
            await context.Potents.AddRangeAsync(fakeData.Potents);

            // Добавляем статусы заявок
            await context.ApplicationStatuses.AddRangeAsync(fakeData.ApplicationStatuses);

            // Добавляем мероприятия
            await context.Events.AddRangeAsync(fakeData.Events);

            // Добавляем состязания
            await context.Competitions.AddRangeAsync(fakeData.Competitions);

            // Добавляем типы документов
            await context.DocTypes.AddRangeAsync(fakeData.DocTypes);

            // Добавляем документы
            await context.Docs.AddRangeAsync(fakeData.Docs);

            // Добавляем участников мероприятий (заявки)
            await context.EventParticipants.AddRangeAsync(fakeData.EventParticipants);

            await context.SaveChangesAsync();
        }
    }

}
