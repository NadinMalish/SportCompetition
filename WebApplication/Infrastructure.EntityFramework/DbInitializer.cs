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

            //// Добавляем пользователей
            await context.Potents.AddRangeAsync(fakeData.Potents);

            // Добавляем статусы заявок
            await context.ApplicationStatuses.AddRangeAsync(fakeData.ApplicationStatuses);

            //// Добавляем мероприятия
            await context.Events.AddRangeAsync(fakeData.Events);

            //// Добавляем состязания
            await context.Competitions.AddRangeAsync(fakeData.Competitions);

            //// Добавляем типы документов
            //await context.DocTypes.AddRangeAsync(fakeData.DocTypes);

            //// Добавляем документы
            //await context.Docs.AddRangeAsync(fakeData.Docs);

            // Добавляем участников мероприятий (заявки)
            await context.EventParticipants.AddRangeAsync(fakeData.EventParticipants);


            // Сохраняем всё
            await context.SaveChangesAsync();
        }
    }

}
