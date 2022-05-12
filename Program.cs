using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

namespace TelegramBotExp
{
	class Program
	{
		static ITelegramBotClient bot = new TelegramBotClient("5377289499:AAFGRgsvMa0629q4PeuzuYpnpXRZINHzjU8");
		public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
		{
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
			if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
			{
				var message = update.Message;
				if (message.Text.ToLower() == "/start")
				{
					await botClient.SendTextMessageAsync(message.Chat, "раз два три");
					return;
				}
				await botClient.SendTextMessageAsync(message.Chat, "Ку");
			}
		}

		public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
		{
			Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));

		}

		static void Main(string[] args)
		{
			Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

			var cts = new CancellationTokenSource();
			var cancellationToken = cts.Token;
			var receiverOptions = new ReceiverOptions
			{
				AllowedUpdates = { },
			};
			bot.StartReceiving(
				HandleUpdateAsync,
				HandleErrorAsync,
				receiverOptions,
				cancellationToken);
			Console.ReadLine();
		}
	}
	

}