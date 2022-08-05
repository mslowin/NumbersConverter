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

namespace NumbersConverter
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PreviewKeyDown += new KeyEventHandler(HandleEsc);
            this.PreviewKeyDown += new KeyEventHandler(HandleEnter);
        }

        private void HandleEsc(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                Close();
        }

        private void HandleEnter(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return && TextBoxInput.IsKeyboardFocused)
            {
                string output = Converter.ConvertFromASCII(TextBoxInput.Text, ComboBoxType.Text);
                TextBoxOutput.Text = output;
            }

            if (e.Key == Key.Return && TextBoxOutput.IsKeyboardFocused)
            {
                string output = Converter.ConvertToASCII(TextBoxInput.Text, ComboBoxType.Text);
                TextBoxInput.Text = output;
            }
        }

        private void TextBoxInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if(TextBoxInput.Text != "")
            //{
            //    TextBoxOutput.Text = "";
            //    TextBoxOutput.IsReadOnly = true;
            //    TextBoxOutput.Background = Brushes.Gray;
            //}
            //if(TextBoxInput.Text == "")
            //{
            //    TextBoxOutput.IsReadOnly = false;
            //    TextBoxOutput.Background = Brushes.White;
            //}
        }

        private void TextBoxOutput_TextChanged(object sender, TextChangedEventArgs e)
        {
            //if (TextBoxOutput.Text != "")
            //{
            //    TextBoxInput.Text = "";
            //    TextBoxInput.IsReadOnly = true;
            //    TextBoxInput.Background = Brushes.Gray;
            //}
            //if (TextBoxOutput.Text == "")
            //{
            //    TextBoxInput.IsReadOnly = false;
            //    TextBoxInput.Background = Brushes.White;
            //}
        }
    }
}
