using SpotifyAPI.Local;
using SpotifyAPI.Local.Enums;
using SpotifyAPI.Local.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotConsole
{
    class Program
    {
        private static SpotifyLocalAPI localAPI;
        static void Main(string[] args)
        {
            localAPI = new SpotifyLocalAPI();
            localAPI.ListenForEvents = true;

            Console.WriteLine(SpotifyLocalAPI.IsSpotifyRunning());

            while (!SpotifyLocalAPI.IsSpotifyRunning() && !SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                Console.WriteLine("Checking connection readiness...");
                if (!SpotifyLocalAPI.IsSpotifyRunning())    //Make sure the spotify client is running
                {
                    Console.WriteLine("No Spotify instance was found.\nAttempting to start Spotify...");
                    SpotifyLocalAPI.RunSpotify();
                    while (!SpotifyLocalAPI.IsSpotifyRunning()) { } //Checks until the process is started.
                                
                }

                if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())   //Make sure the spotify web helper is running
                {
                    Console.WriteLine("No Spotify Web Helper instance was found.\nAttempting to start Spotify Web Helper...");
                    SpotifyLocalAPI.RunSpotifyWebHelper();
                    while (!SpotifyLocalAPI.IsSpotifyWebHelperRunning()) { }    //Checks until the process is started.
                }
            }

            if (!localAPI.Connect())
            {
                Console.WriteLine("Unable to connect to Spotify. System will keep trying. Press any key to end.");
                while (!localAPI.Connect())    //We need to call Connect before fetching infos, this will handle Auth stuff
                {
                    if (Console.KeyAvailable)
                    {
                        return;
                    }
                }
            }

            Console.WriteLine("Connection to Spotify successful.");
                
                
           





            Console.WriteLine("Press enter to pause spotify.");
            Console.ReadLine();
            localAPI.Pause();

            Console.WriteLine("End run.");
            Console.ReadLine();
        }
    }
}
