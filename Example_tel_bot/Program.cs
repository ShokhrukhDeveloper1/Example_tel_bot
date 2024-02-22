using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

public class Program
{
    private static TelegramBotClient telegramBot;
    private const string startCommand = "/start";
    private const string aboutMeCommand = "About me";
    private const string skillsCommand = "Skills";
    private const string interestsCommand = "Interests";
    private const string contactMeCommand = "Contact me";

    private static void Main(string[] args)
    {
        string token = @"7061994101:AAHva25FBkagCSQ_Uep4K5-EW1GSHjNVESY";
        telegramBot = new TelegramBotClient(token);

        telegramBot.StartReceiving(updateHandler, errorHandler);

        Console.ReadLine();
    }

    private static async Task updateHandler(ITelegramBotClient client, Update update, CancellationToken token)
    {
        if(update.Message?.Type is MessageType.Text)
        {
            if(update.Message.Text is startCommand)
            {
                var markup = MenuMarkup();

                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Welcome to Shokhrukh's resume.",
                    replyMarkup: markup);
            }
            if(update.Message.Text is aboutMeCommand)
            {
                var photoPath = @"D:\Pictures\IMG_20240217_153428.jpg";

                using (var photoStream = new FileStream(photoPath, FileMode.Open, FileAccess.Read))
                {
                await client.SendPhotoAsync(
                    chatId: update.Message.Chat.Id,
                    photo: new InputFileStream(photoStream),
                    caption: "I'm a student at the national university of Uzbekistan. " +
                    "These days I'm learning different kind of technology " +
                    "and I want to be a programmer in the future. " +
                    "So, My motto in life is to never give up! ");
                }
            }
            if(update.Message.Text is skillsCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "C#\nJava\nPython");
            }
            if (update.Message.Text is interestsCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "Playing basketall 🏀, football ⚽️, chess ♟," +
                    "\nreading books 📚 and sometimes playing musical instruments 🎹");
            }
            if (update.Message.Text is contactMeCommand)
            {
                await client.SendTextMessageAsync(
                    chatId: update.Message.Chat.Id,
                    text: "@shokhrukhdeveloper1\n+998993214576");
            }
        }
        else
        {
            await client.SendTextMessageAsync(
                chatId: update.Message.Chat.Id,
                text: "Please send only text message!");
        }
    }

    private static ReplyKeyboardMarkup MenuMarkup()
    {
        return new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
               new KeyboardButton[]{new KeyboardButton("About me"), new KeyboardButton("Skills")},
               new KeyboardButton[]{new KeyboardButton("Interests"),new KeyboardButton("Contact me")}
            })
        {
            ResizeKeyboard = true
        };
    }

    private static async Task errorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        await client.SendTextMessageAsync(
            chatId: 6766208954,
            text: $"Error: {exception.Message}");
    }
}