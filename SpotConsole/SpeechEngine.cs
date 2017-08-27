using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace SpotConsole
{
    class SpeechEngine
    {
        public SpeechEngine()
        {
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
            sre.SetInputToDefaultAudioDevice();
            BuildLoadGrammars(sre);
            
            SpeechSynthesizer ss = new SpeechSynthesizer();
            ss.SetOutputToDefaultAudioDevice();
            ss.SelectVoiceByHints(VoiceGender.Female);
        }

        public void BuildLoadGrammars(SpeechRecognitionEngine sre)
        {
            Choices assets = new Choices();
            assets.Add(new string[] { "artist", "album", "what song is playing", "what song is this", "play", "pause", "exit" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(assets);

            Grammar g = new Grammar(gb);

            sre.LoadGrammar(g);
        }

        public void Listen(SpeechRecognitionEngine sre)
        {
            bool looping = true;

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            sre.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(sre_SpeechRejected);

            while (looping)
            {
                sre.Recognize();
            }

            void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
            {
                if (e.Result.Text.Equals("exit"))
                {
                    looping = false;
                    Console.WriteLine("Exiting recognition loop...");
                    //ss.Speak("Exiting recognition loop");                                             TODO: Add synthesis.
                    sre.Dispose();
                }
                else
                {
                    Console.WriteLine("Speech recognized: " + e.Result.Text);
                    Program.CompleteAction(e.Result.Text);
                }

            }

            void sre_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
            {
                Console.WriteLine("Speech not recognized. Please try again.");
            }
        }
    }
}
