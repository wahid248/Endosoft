using System;
using System.Collections.Generic;
using System.IO;
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

namespace Endosoft
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            try
            {
                // load doctors
                using (var file = new StreamReader("Data\\Doctors.txt"))
                {
                    var line = string.Empty;
                    while ((line = file.ReadLine()) != null)
                    {
                        CmbDoc.Items.Add(new ComboBoxItem { Content = line, IsSelected = CmbDoc.Items.Count == 0 });
                    }
                }

                // load images
                var images = Directory.EnumerateFiles("Data\\Images", "*.*", SearchOption.TopDirectoryOnly)
                    .Where(s => 
                        s.ToLower().EndsWith(".jpg") ||
                        s.ToLower().EndsWith(".jpeg") ||
                        s.ToLower().EndsWith(".png") ||
                        s.ToLower().EndsWith(".bmp")
                    ).ToList();

                if (images.Count() == 0) throw new Exception("Could not find images in directory /Data/Images. Supported images are jpg, jpeg, png, bmp.");
                
                for(int i = 0; i < images.Count() && i < 5; i++)
                {
                    if (i == 0) Img1.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\{images[i]}"));
                    if (i == 1) Img2.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\{images[i]}"));
                    if (i == 2) Img3.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\{images[i]}"));
                    if (i == 3) Img4.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\{images[i]}"));
                    if (i == 4) Img5.Source = new BitmapImage(new Uri($"{Directory.GetCurrentDirectory()}\\{images[i]}"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
                Environment.Exit(-1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // redesign controls
                TxtName.BorderThickness = new Thickness(0, 0, 0, 0);
                TxtAge.BorderThickness = new Thickness(0, 0, 0, 0);

                LblSexValue.Content = CmbSex.Text;
                LblSexValue.Visibility = Visibility.Visible;
                CmbSex.Visibility = Visibility.Hidden;

                LblDocValue.Content = CmbDoc.Text;
                LblDocValue.Visibility = Visibility.Visible;
                CmbDoc.Visibility = Visibility.Hidden;

                BtnPrint.Visibility = Visibility.Hidden;

                // print
                PrintDialog printDialog = new PrintDialog();
                printDialog.PrintVisual(print, "Endoscope");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error");
            }
            finally
            {
                TxtName.BorderThickness = new Thickness(0, 0, 0, 1);
                TxtAge.BorderThickness = new Thickness(0, 0, 0, 1);

                LblSexValue.Visibility = Visibility.Hidden;
                CmbSex.Visibility = Visibility.Visible;

                LblDocValue.Visibility = Visibility.Hidden;
                CmbDoc.Visibility = Visibility.Visible;

                BtnPrint.Visibility = Visibility.Visible;
            }
        }
    }
}
