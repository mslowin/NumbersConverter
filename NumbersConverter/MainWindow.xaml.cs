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
            if (e.Key == Key.Return && int.TryParse(TextBoxInput.Text, out var input))
            {
                int output = Convert(input, ComboBoxType.Text);
                TextBoxOutput.Text = output.ToString();
            }
        }


        private int Convert(int input, string desiredType)
        {
            var output = 0;
            switch (desiredType)
            {
                case "ASCII":
                    output = ConvertToASCII();
                    break;

                case "Binary":
                    output = ConvertToBinary();
                    break;

                case "Hexadecimal":
                    output = ConvertToHexadecimal();
                    break;

                case "Base64":
                    output = ConvertToBase64();
                    break;
            }
            return output;
        }

        private int ConvertToASCII()
        {
            throw new NotImplementedException();
        }

        private int ConvertToBinary()
        {
            throw new NotImplementedException();
        }

        private int ConvertToHexadecimal()
        {
            throw new NotImplementedException();
        }

        private int ConvertToBase64()
        {
            throw new NotImplementedException();
        }

    }
}
