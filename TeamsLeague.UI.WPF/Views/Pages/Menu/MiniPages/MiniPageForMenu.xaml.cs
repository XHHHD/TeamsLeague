using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using TeamsLeague.UI.WPF.Resources.Constants;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu.MiniPages
{
    public partial class MiniPageForMenu : Page
    {
        private readonly MenuMiniPagesType _pageType;
        private StackPanel? _stackPanel;
        private Image? _image;
        private TextBlock? _topText;
        private TextBlock? _subText;


        public MiniPageForMenu(MenuMiniPagesType type)
        {
            InitializeComponent();
            _pageType = type;
            PageConstructor();
        }


        private void PageConstructor()
        {
            switch (_pageType)
            {
                case MenuMiniPagesType.UserTeamStats:
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    _image = new Image()
                    {
                        Name = "Img",
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative)),
                        Height = 70,
                        Width = 70,
                        Margin = new Thickness(3),
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "Team Name",
                        FontSize = 30,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Page_Grid.Children.Add(_image);
                    Page_Grid.Children.Add(_subText);
                    Grid.SetRow(_image, 0);
                    Grid.SetRow(_subText, 0);
                    break;

                case MenuMiniPagesType.LeagueStats:
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    _image = new Image()
                    {
                        Name = "Img",
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative)),
                        Height = 70,
                        Width = 70,
                        Margin = new Thickness(3),
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "League",
                        FontSize = 30,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Grid.SetRow(_image, 0);
                    Grid.SetRow(_subText, 0);
                    Page_Grid.Children.Add(_image);
                    Page_Grid.Children.Add(_subText);
                    break;

                case MenuMiniPagesType.Trainings:
                    _topText = new TextBlock()
                    {
                        Name = "TopText",
                        Text = "TEAM",
                        FontSize = 60,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "TRAININGS",
                        FontSize = 36,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _stackPanel = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Center, };
                    _stackPanel.Children.Add(_topText);
                    _stackPanel.Children.Add(_subText);
                    Page_Grid.Children.Add(_stackPanel);
                    break;

                case MenuMiniPagesType.TopTeams:
                    _topText = new TextBlock()
                    {
                        Name = "TopText",
                        Text = "TOP",
                        FontSize = 60,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "TEAMS",
                        FontSize = 40,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _stackPanel = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Center, };
                    _stackPanel.Children.Add(_topText);
                    _stackPanel.Children.Add(_subText);
                    Page_Grid.Children.Add(_stackPanel);
                    break;

                case MenuMiniPagesType.TopMembers:
                    _topText = new TextBlock()
                    {
                        Name = "TopText",
                        Text = "TOP",
                        FontSize = 60,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "MEMBERS",
                        FontSize = 40,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    _stackPanel = new StackPanel() { HorizontalAlignment = HorizontalAlignment.Center, };
                    _stackPanel.Children.Add(_topText);
                    _stackPanel.Children.Add(_subText);
                    Page_Grid.Children.Add(_stackPanel);
                    break;

                case MenuMiniPagesType.UserStats:
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    Page_Grid.RowDefinitions.Add(new RowDefinition());
                    _image = new Image()
                    {
                        Name = "Img",
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative)),
                        Height = 70,
                        Width = 70,
                        Margin = new Thickness(3),
                    };
                    _subText = new TextBlock()
                    {
                        Name = "SubText",
                        Text = "Statistics",
                        FontSize = 30,
                        FontFamily = new FontFamily("Arial Black"),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    Page_Grid.Children.Add(_image);
                    Page_Grid.Children.Add(_subText);
                    Grid.SetRow(_image, 0);
                    Grid.SetRow(_subText, 0);
                    break;
            }
        }

        private void TButton_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
