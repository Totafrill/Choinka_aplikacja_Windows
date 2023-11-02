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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Choinka
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IkonaWZasobniku ikonaWZasobniku;
        public MainWindow()
        {
            InitializeComponent();

            ikonaWZasobniku = new IkonaWZasobniku(this);
        }

        #region Przenoszenie okna
        private bool czyPrzenoszenie = false;
        private Cursor kursor = null;
        private Point punktPoczątkowy;
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ButtonState == Mouse.LeftButton)
            {
                czyPrzenoszenie = true;
                kursor = this.Cursor;
                Cursor = Cursors.Hand;
                punktPoczątkowy = e.GetPosition(this);
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if(czyPrzenoszenie)
            {
                Vector przesunięcie = e.GetPosition(this) - punktPoczątkowy;
                Left += przesunięcie.X;
                Top += przesunięcie.Y;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if(czyPrzenoszenie)
            {
                Cursor = kursor;
                czyPrzenoszenie = false;
            }
        }
        #endregion

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape) this.Close();
        }

        bool zakończonaAnimacjaZnikania = false;
        private void okno_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!zakończonaAnimacjaZnikania)
            {
                Storyboard scenorysZnikaniaOkna = this.Resources["scenorysZnikaniaOkna"] as Storyboard;
                scenorysZnikaniaOkna.Begin();
                e.Cancel = true;
            }

        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            zakończonaAnimacjaZnikania = true;
            Close();
        }
 
    }
}
