using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var botClient = new TelegramBotClient("6094205005:AAELz2PhDqVQPFEcUQB2IFgkOWttVRdryrU");
            botClient.StartReceiving(updateHandler: HandleUpdateAsync, pollingErrorHandler: HandlePollingErrorAsync);
            Console.ReadLine();
        }

        private static Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        async static Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken token)
        {

            var message = update.Message;

            if (message.Text != "null")
            {
                Console.WriteLine($"{message.Chat.FirstName}   -  {message.Text}");
            }


                if (message.Text == "/start")
            {

                client.SendTextMessageAsync(message.Chat.Id, ("Доступные команды:\n" +
                    "/start - Начать работу бота\n" +
                    "/file - Отправить файл в формате FRX и FPX и вернуть в PDF.\n" +
                    "/help - Информация и возможности бота."));
                
            }




            if (message.Text == "/file")
            {
                
                client.SendTextMessageAsync(message.Chat.Id, "Отправьте файл");
                
            }

            if (message.Document != null)

            {
                Console.WriteLine($"{message.Chat.FirstName}   -  Отправил файл");

                client.SendTextMessageAsync(message.Chat.Id, "Файл принят");


                var fileId = update.Message.Document.FileId;
                var fileInfo = await client.GetFileAsync(fileId);
                var filePath = fileInfo.FilePath;


                string destinationFilePath = $@"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}/{message.Document.FileName}";
                await using FileStream fileStream = System.IO.File.OpenWrite(destinationFilePath);
                await client.DownloadFileAsync(filePath, fileStream);
                fileStream.Close();
            }

            if (message.Text == "/help")
            {
              
                client.SendTextMessageAsync(message.Chat.Id, ("Телеграм бот для работы FastReport Cloud,умеет получать файлы в формате FRX или FPX и возвращать PDF."));


            }







        } 
    } 
} 