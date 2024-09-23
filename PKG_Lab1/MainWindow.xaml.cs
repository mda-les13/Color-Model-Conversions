using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PKG_Lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            //mySolidColorBrush.Color = Color.FromRgb(40, 0, 180);
            this.MinWidth = 800;
            this.MinHeight = 450;
            //.Background = mySolidColorBrush;
            InitializeComponent();
            DataContext = new ColorViewModel();
        }

        private void CButton_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TrueColor.Text);
            MessageBox.Show(this, "Цвет скопирован", "Message");

        }

        private void RGB_TextChanged(object sender, TextChangedEventArgs e)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            if (R != null
                && G != null
                && B != null
                && R.Text != ""
                && G.Text != ""
                && B.Text != "")
            {
                if (Convert.ToInt32(R.Text) > 255)
                {
                    R.Text = "255";
                }

                if (Convert.ToInt32(G.Text) > 255)
                {
                    G.Text = "255";
                }

                if (Convert.ToInt32(B.Text) > 255)
                {
                    B.Text = "255";
                }

                mySolidColorBrush.Color =
                Color.FromArgb(
                    255,
                    Convert.ToByte(Convert.ToInt32(R.Text)),
                    Convert.ToByte(Convert.ToInt32(G.Text)),
                    Convert.ToByte(Convert.ToInt32(B.Text)));
            }

            if (CButton != null)
            {
                CButton.Background = mySolidColorBrush;
                TrueColor.Text = mySolidColorBrush.ToString();
            }

        }

        private void CMYK_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RGBSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void CMYKSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void XYZ_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void XYZSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color?> e)
        {
            if (e.NewValue.HasValue)
            {
                R.Text = e.NewValue.Value.R.ToString();
                G.Text = e.NewValue.Value.G.ToString();
                B.Text = e.NewValue.Value.B.ToString();
            }
        }
    }
}