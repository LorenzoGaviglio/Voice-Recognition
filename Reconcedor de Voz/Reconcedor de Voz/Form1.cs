using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Recognition;

namespace Reconcedor_de_Voz
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine escucha = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnEscuchar_Click(object sender, EventArgs e)
        {
            try
            {
                escucha.SetInputToDefaultAudioDevice();
                escucha.LoadGrammar(new DictationGrammar());
                escucha.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(reconocedor);
                escucha.RecognizeAsync(RecognizeMode.Multiple);
                escucha.AudioLevelUpdated += nivel_audio;
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("ya dio acceso al uso del miscrófono");
            }
        }

        public void reconocedor(object sender, SpeechRecognizedEventArgs e)
        {
            foreach (RecognizedWordUnit palabra in e.Result.Words)
            {
                textBox1.Text += palabra.Text + " ";
            }
        }

        public void nivel_audio(object sender, AudioLevelUpdatedEventArgs e)
        {
            int audio = e.AudioLevel;
            progressBar1.Value = audio;
        }
    }
}
