using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using POEClientLibrary;
using System.Collections.Generic;

namespace PoEApi
{

    /// <summary>
    /// Main goal of this program is to pass an account name and return total time played, most played character.
    /// work in progress
    /// </summary>
    class Program
    {
        static async Task Main(string[] args)
        {

            POEClient PoeClient = new POEClient();
            StashTab stashtab = await PoeClient.GetPublicStashTabsASync();

            List<League> GetLeagues = await PoeClient.GetLeaguesAsync(type: "event", id: "Standard");


            Console.WriteLine(stashtab);
            Console.ReadLine();



        }
    }
}
