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

            Console.WriteLine("Initializing application...");
            while (!SpotifyLocalAPI.IsSpotifyRunning() || !SpotifyLocalAPI.IsSpotifyWebHelperRunning())
            {
                if (!SpotifyLocalAPI.IsSpotifyRunning())    //Make sure the spotify client is running
                {
                    Console.WriteLine("No Spotify instance was found. Attempting to start Spotify...");
                    SpotifyLocalAPI.RunSpotify();
                    while (!SpotifyLocalAPI.IsSpotifyRunning()) { } //Checks until the process is started.
                } else
                {
                    Console.WriteLine("Spotify instance found.");
                }

                if (!SpotifyLocalAPI.IsSpotifyWebHelperRunning())   //Make sure the spotify web helper is running
                {
                    Console.WriteLine("No Spotify Web Helper instance was found.\nAttempting to start Spotify Web Helper...");
                    SpotifyLocalAPI.RunSpotifyWebHelper();
                    while (!SpotifyLocalAPI.IsSpotifyWebHelperRunning()) { }    //Checks until the process is started.
                } else
                {
                    Console.WriteLine("Spotify Web Helper active. Checking connection...");
                }
                System.Threading.Thread.Sleep(4000);
            }

            if (!localAPI.Connect())
            {
                Console.WriteLine("Unable to connect to Spotify. This can occur if the process was just initialized.\nThe system will keep trying to connect. Press any key to exit this application.");
                while (!localAPI.Connect())    //We need to call Connect before fetching infos, this will handle Auth stuff
                {
                    if (Console.KeyAvailable)
                    {
                        return;
                    }
                }
            }

            Console.WriteLine("Connection to Spotify successful.");
            System.Threading.Thread.Sleep(8000);

            Console.Clear();

            Console.WriteLine("Welcome to the Spotify Local API Console Application. Enter \"Help\" for help, or begin entering commands.");







            Console.WriteLine("Press enter to pause spotify.");
            Console.ReadLine();
            localAPI.Pause();

            Console.WriteLine("End run.");
            Console.ReadLine();
        }
    }
}
