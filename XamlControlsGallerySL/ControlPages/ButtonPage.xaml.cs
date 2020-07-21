using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;

namespace AppUIBasics.ControlPages
{
    public partial class ButtonPage : Page
    {
        public ButtonPage()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                var b = (Button)sender;
                string name = b.Name;

                switch (name)
                {
                    case "Button1":
                        (this.Example1.FindName("Control1Output") as TextBlock).Text = "You clicked: " + name;
                        break;
                    case "Button2":
                        (this.Example2.FindName("Control2Output") as TextBlock).Text = "You clicked: " + name;
                        break;
                    case "Button3":
                        (this.Example3.FindName("Control3Output") as TextBlock).Text = "You clicked: " + name;
                        break;
                }
            }
        }


        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

    }
}
