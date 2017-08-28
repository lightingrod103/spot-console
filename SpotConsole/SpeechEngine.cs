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
        SpeechRecognitionEngine sre = new SpeechRecognitionEngine();
        SpeechSynthesizer ss = new SpeechSynthesizer();

        public SpeechEngine()
        {
            sre.SetInputToDefaultAudioDevice();
            BuildLoadGrammars();
            
            ss.SetOutputToDefaultAudioDevice();
            ss.SelectVoiceByHints(VoiceGender.Female);
        }

        private void BuildLoadGrammars()
        {
            Choices assets = new Choices();
            assets.Add(new string[] { "what song is playing", "what song is this", "play", "pause", "exit" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(assets);

            Grammar g = new Grammar(gb);

            sre.LoadGrammar(g);
        }

        public void StartLoopListen()
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
                }
                else
                {
                    Program.CompleteAction(e.Result.Text);
                }
            }

            void sre_SpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
            {
                ss.Speak("Speech not recognized. Please try again.");
            }
        }

        public void StopLoopListen()
        {
            ss.Speak("Exiting recognition loop.");
            sre.Dispose();
        }

        public void Speak(String text)
        {
            ss.Speak(text);
        }
    }
}
