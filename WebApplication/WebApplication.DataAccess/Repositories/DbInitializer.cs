using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DataAccess.Data;

namespace WebApplication.DataAccess.Repositories
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(Context context)
        {
            // Проверка на наличие данных
            if (await context.ApplicationStatuses.AnyAsync() ||
                await context.Roles.AnyAsync() ||
                await context.EventParticipants.AnyAsync())
            {
                return; // Данные уже есть — ничего не делаем
            }

            var fakeData = new FakeDataFactory();

            // Добавляем роли
            await context.Roles.AddRangeAsync(fakeData.Roles);

            // Добавляем статусы
            await context.ApplicationStatuses.AddRangeAsync(fakeData.ApplicationStatuses);

            // Добавляем участников мероприятий
            await context.EventParticipants.AddRangeAsync(fakeData.EventParticipants);

            // Сохраняем всё
            await context.SaveChangesAsync();
        }
    }

}
