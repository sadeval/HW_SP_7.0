using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task<string> LoadUserDataAsync(CancellationToken token)
    {
        try
        {
            Console.WriteLine("Загрузка данных о пользователе...");

            await Task.Delay(15000, token); 

            return "Данные о пользователе: Имя - Джейн, Возраст - 38";
        }
        catch (OperationCanceledException)
        {
            return "Операция была отменена: сервер не отвечает";
        }
        catch (Exception ex)
        {
            return $"Ошибка: {ex.Message}";
        }
    }

    static async Task Main()
    {
        using (CancellationTokenSource cts = new CancellationTokenSource())
        {
            cts.CancelAfter(TimeSpan.FromSeconds(10));

            string result = await LoadUserDataAsync(cts.Token);

            Console.WriteLine(result);
        }
    }
}
