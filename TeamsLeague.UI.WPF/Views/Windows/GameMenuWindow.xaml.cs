using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.UI.WPF.Resources.Constants;
using TeamsLeague.UI.WPF.Views.Pages.Menu.MiniPages;
using Unity;

namespace TeamsLeague.UI.WPF.Windows
{
    public partial class GameWindow : Window
    {
        private readonly IBufferingService _buffer;

        public GameWindow(IBufferingService buffer)
        {
            InitializeComponent();
            _buffer = buffer;
            CreateComponent();
        }

        private void GameWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void CheckedFullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }
        private void UncheckedFullScreenButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }
        private void MinimizedButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CreateComponent()
        {
            foreach (var type in Enum.GetValues<MenuMiniPagesType>())
            {
                PagesMiniBar_StackPanel.Children.Add(new Frame()
                {
                    NavigationUIVisibility = NavigationUIVisibility.Hidden,
                    Height = 120,
                    Width = 240,
                    Content = new MiniPageForMenu(type),
                });
            }
        }
    }
}