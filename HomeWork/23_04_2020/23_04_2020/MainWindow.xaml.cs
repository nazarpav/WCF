using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _23_04_2020
{
    public partial class MainWindow : Window
    {
        Translator.Service1Client _t = new Translator.Service1Client();
        public MainWindow()
        {
            InitializeComponent();
            History.Items.Add("# HISTORY #");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //add
            _t.AddNewWord_WordTranslate(Word.Text, Translate.Text);
            History.Items.Add("# Word: " + Word.Text + " | Translate: " + Translate.Text);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //get
            TranslationList.Items.Clear();
            foreach (var item in _t.GetTranslationsWords(WordTranslation.Text))
            {
                TranslationList.Items.Add("#Word: " + item);
            }
        }
    }
}
