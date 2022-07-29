﻿using System;
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
                string output = Converter.Convert(input, ComboBoxType.Text);
                TextBoxOutput.Text = output;
            }
        }
    }
}
