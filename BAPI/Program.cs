using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BAPI.Handlers.BOMHandler;
using BAPI.Handlers.OTHandler;
using BAPI.Handlers.NTHandler;
using BAPI.Handlers.DNCHandler;
using BAPI.Handlers.ArrayHandler;
using DSharpPlus;
using DSharpPlus.Entities;

namespace BAPI
{
    class Program
    {
        public static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        public static async Task MainAsync()
        {
            Console.WriteLine("init");

            FileStream fs = File.Create("token.txt");
            fs.Close();
            File.WriteAllText("token.txt", "ODk5ODU4NzU0Mzg2OTUyMTkz.YW44xQ._POifhXFMVUUARa9AQwz_vRqr5E");
            
            DiscordClient client = new DiscordClient(new DiscordConfiguration()
            {
                Token = File.ReadAllText("token.txt"),
                TokenType = TokenType.Bot,
            });

            client.MessageCreated += async (s, e) =>
            {
                if (e.Message.Content.ToLower().StartsWith("lds") && e.Message.Content.ToLower().Contains("ticket"))
                {                  
                    DiscordChannel channel = await e.Guild.CreateChannelAsync(Guid.NewGuid().ToString(), ChannelType.Text);
                    await channel.SendMessageAsync("<@&899443505192112178>" + " <@!" + e.Author.Id + ">");
                }
                
                if(e.Message.Content.ToLower().StartsWith("lds") && e.Message.Content.ToLower().Contains("verse-array"))
                {
                    string verses = e.Message.Content.ToString().Replace("lds verse-array ", "");

                    ArrayHandler aHandler = new ArrayHandler();

                    aHandler.VerseArray(verses);

                    string[] VersesToReturn = aHandler.VerseArray(verses);

                    foreach(string val in VersesToReturn)
                    {
                        if(val == "could not find verse")
                        {
                            return;
                        } else
                        {
                            await e.Message.RespondAsync(val);
                        }

                    }
                }

                if(e.Message.Content.ToLower().StartsWith("lds") && e.Message.Content.ToLower().Contains("verse"))
                {
                    string verse = e.Message.Content.ToString().Replace("lds verse ", "");
                    Console.WriteLine(verse);

                    try
                    {
                        string toReturn = BOM.retreiveVerse(verse).ToString();
                        if(toReturn != "could not find verse")
                        {
                            e.Message.RespondAsync(toReturn);
                        }
                    } catch(Exception ex)
                    {
                        Console.WriteLine("Could not find in BOM");
                    }

                    try
                    {
                        string toReturn = OT.retreiveVerse(verse).ToString();
                        if(toReturn != "could not find verse")
                        {
                            e.Message.RespondAsync(toReturn);
                        }
                    } catch
                    {
                        Console.WriteLine("Couldnt find in OT");
                    }

                    try
                    {
                        string toReturn = NT.retreiveVerse(verse).ToString();
                        if (toReturn != "could not find verse")
                        {
                            e.Message.RespondAsync(toReturn);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Couldnt find in NT");
                    }

                    try
                    {
                        string toReturn = DNC.retreiveVerse(verse).ToString();
                        Console.WriteLine(toReturn);
                        
                        if (toReturn != "could not find verse")
                        {
                            e.Message.RespondAsync(toReturn);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Couldnt find in DNC");
                    }
                }
                if(e.Message.Content.ToLower().StartsWith("lds") && e.Message.Content.ToLower().Contains("invite"))
                {
                    DiscordInvite inv = await e.Message.Channel.CreateInviteAsync(86400, 1);
                    await e.Message.RespondAsync("https://discord.gg/" + inv.Code);
                }
            };

            await client.ConnectAsync();
            await Task.Delay(-1);
        }

        public static string getToken()
        {
            Console.WriteLine(File.ReadAllText("token.txt"));
            return File.ReadAllText("token.txt").Trim().ToString();
        }
    }
}
