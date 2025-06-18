using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.ImplementationInfrastructure.EntityFrameworks
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(Context context)
        {
            //// Проверка на наличие данных
            //if (await context.ApplicationStatuses.AnyAsync() ||
            //    await context.Roles.AnyAsync() ||
            //    await context.EventParticipants.AnyAsync())
            //{
            //    return; // Данные уже есть — ничего не делаем
            //}

            var fakeData = new FakeDataFactory();

            // Добавляем статусы
            await context.ApplicationStatuses.AddRangeAsync(fakeData.ApplicationStatuses);

            // Добавляем участников мероприятий
            await context.EventParticipants.AddRangeAsync(fakeData.EventParticipants);

            // Сохраняем всё
            await context.SaveChangesAsync();
        }
    }

}
