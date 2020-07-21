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
using AppUIBasics.Navigation;

namespace AppUIBasics.ControlPages
{
    public partial class HyperLinkButtonPage : Page
    {
        public HyperLinkButtonPage()
        {
            InitializeComponent();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void GoToHyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationRootPage.RootFrame.Navigate(new Uri("/ControlPages/ButtonPage.xaml", UriKind.Relative));
        }

    }
}
