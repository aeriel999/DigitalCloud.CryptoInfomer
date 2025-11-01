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

namespace DigitalCloud.CryptoInfomer.UI.Views.Pages;
/// <summary>
/// Interaction logic for CoinsListPage.xaml
/// </summary>
public partial class CoinsListPage : Page
{
    private readonly Style _defaultStyle;
    private readonly Style _accentStyle;
    public CoinsListPage()
    {
        InitializeComponent();

        _defaultStyle = (Style)Application.Current.Resources["DefaultButtonStyle"];
        _accentStyle = (Style)Application.Current.Resources["AccentButtonStyle"];

        Top_10_Button.Style = _accentStyle;
    }

    private void Top_10_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_100_Button.Style = _defaultStyle;
        All_Button.Style = _defaultStyle;
    }

    private void Top_100_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_10_Button.Style = _defaultStyle;
        All_Button.Style = _defaultStyle;
    }

    private void All_Button_Click(object sender, RoutedEventArgs e)
    {
        var button = sender as Button;
        if (button == null)
            return;

        button.Style = _accentStyle;

        Top_100_Button.Style = _defaultStyle;
        Top_10_Button.Style = _defaultStyle;
    }
}
