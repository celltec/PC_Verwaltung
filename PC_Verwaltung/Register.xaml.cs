using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PC_Verwaltung
{
    public partial class Register : UserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            Switcher.Switch(new Login());
        }
    }
}
