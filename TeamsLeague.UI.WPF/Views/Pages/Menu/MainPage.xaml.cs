﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.UI.WPF.Resources.Constants;

namespace TeamsLeague.UI.WPF.Views.Pages.Menu
{
    public partial class MainPage : Page
    {
        private readonly IBufferingService _buffer;
        private readonly MenuPagesType _pageType;
        private readonly Grid _innerGrid;
        private readonly StackPanel _stackPanel;
        private readonly ScrollViewer _scrollViewer;


        public MainPage(IBufferingService buffer, MenuPagesType pageType)
        {
            _buffer = buffer;
            _pageType = pageType;
            _innerGrid = new();
            _stackPanel = new();
            _scrollViewer = new();
            InitializeComponent();
            PageConstructor();
        }

        private void PageConstructor()
        {
            switch (_pageType)
            {
                case MenuPagesType.UserTeamStats:
                    Page_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.5, GridUnitType.Star) });
                    Page_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

                    _innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    _innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.25, GridUnitType.Star) });

                    _stackPanel.Name = "InformationStack";
                    _stackPanel.Orientation = Orientation.Vertical;

                    var teamName = new TextBlock
                    {
                        Name = "TeamName",
                        Text = "Team Name",
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48
                    };
                    var teamInformation = new TextBlock
                    {
                        Name = "TeamInformation",
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 24,
                        Text = "Team Stats Information"
                    };

                    var teamLogo = new Image
                    {
                        Name = "TeamLogo",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative))
                    };

                    var membersStackPanel = new StackPanel
                    {
                        Name = "Members",
                        Orientation = Orientation.Horizontal,
                    };
                    membersStackPanel.SetValue(Grid.RowProperty, 1);

                    var addMemberButton = new Button
                    {
                        Name = "AddMemberButton",
                        MinWidth = 160,
                        Margin = new Thickness(0),
                        Background = null,
                        Foreground = new SolidColorBrush(Color.FromRgb(189, 189, 189)),
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 120,
                        Content = "+",
                    };
                    addMemberButton.Click += AddMemberButton_Click;

                    Grid.SetColumn(_stackPanel, 0);

                    _stackPanel.Children.Add(teamName);
                    _stackPanel.Children.Add(teamInformation);

                    membersStackPanel.Children.Add(addMemberButton);
                    AddMembersViewsOnPage(membersStackPanel);

                    _innerGrid.Children.Add(_stackPanel);
                    Page_Grid.Children.Add(_innerGrid);
                    Page_Grid.Children.Add(teamLogo);
                    Page_Grid.Children.Add(membersStackPanel);
                    break;

                case MenuPagesType.LeagueStats:
                    Page_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                    Page_Grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                    _innerGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto, });
                    _innerGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(6, GridUnitType.Star), });

                    _scrollViewer.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
                    _scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                    _scrollViewer.Content = _stackPanel;

                    _stackPanel.Orientation = Orientation.Vertical;

                    var leagueName = new TextBlock
                    {
                        Name = "LeagueName",
                        HorizontalAlignment = HorizontalAlignment.Left,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48,
                        Text = "League Name"
                    };

                    var teamLogoImg = new Image
                    {
                        Name = "TeamLogoImg",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        Width = 60,
                        MinHeight = 60,
                        Margin = new Thickness(3),
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative))
                    };

                    var teamFrame = new Frame
                    {
                        Name = "Team",
                        NavigationUIVisibility = NavigationUIVisibility.Hidden,
                        Height = 60,
                        Margin = new Thickness(3)
                    };

                    _stackPanel.Children.Add(teamFrame);
                    _innerGrid.Children.Add(leagueName);
                    _innerGrid.Children.Add(teamLogoImg);
                    _innerGrid.Children.Add(_scrollViewer);

                    var stackPanel2 = new StackPanel
                    {
                        Margin = new Thickness(3)
                    };

                    var leagueLogo = new Image
                    {
                        Name = "LeagueLogo",
                        Margin = new Thickness(3),
                        Height = 200,
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative))
                    };

                    var nextRank = new Frame
                    {
                        Name = "NextRank",
                        Height = 100
                    };

                    var currentRank = new Frame
                    {
                        Name = "CurrentRank",
                        Height = 100
                    };

                    var previousRank = new Frame
                    {
                        Name = "PreviousRank",
                        Height = 100
                    };

                    stackPanel2.Children.Add(leagueLogo);
                    stackPanel2.Children.Add(nextRank);
                    stackPanel2.Children.Add(currentRank);
                    stackPanel2.Children.Add(previousRank);

                    Grid.SetColumn(_innerGrid, 0);
                    Grid.SetRow(leagueName, 0);
                    Grid.SetRow(teamLogoImg, 0);
                    Grid.SetRow(_scrollViewer, 1);
                    Grid.SetColumn(stackPanel2, 1);

                    Page_Grid.Children.Add(_innerGrid);
                    Page_Grid.Children.Add(stackPanel2);
                    break;
                case MenuPagesType.Trainings:
                    var logoImage = new Image()
                    {
                        Name = "MemberLogo",
                        Width = 200,
                        Height = 200,
                        Margin = new Thickness(3),
                        Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-white.png", UriKind.Relative))
                    };
                    Grid.SetColumn(logoImage, 0);
                    Grid.SetRow(logoImage, 0);
                    Grid00.Children.Add(logoImage);

                    var teamNameTextBlock = new TextBlock()
                    {
                        Name = "TeamName",
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48,
                        Text = "Team Name"
                    };

                    var teamShortNameTextBlock = new TextBlock()
                    {
                        Name = "TeamShortName",
                        TextWrapping = TextWrapping.Wrap,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48,
                        Text = "ONE"
                    };

                    var teamLevelTextBlock = new TextBlock()
                    {
                        Name = "TeamLevel",
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36,
                        Text = "0"
                    };

                    var teamEnergyTextBlock = new TextBlock()
                    {
                        Name = "TeamEnergy",
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36,
                        Text = "0"
                    };

                    var teamMaxEnergyTextBlock = new TextBlock()
                    {
                        Name = "TeamMaxEnergy",
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36,
                        Text = "0"
                    };

                    var teamInfoTextBlock = new TextBlock()
                    {
                        Name = "TeamInfo",
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Right,
                        FontFamily = new FontFamily("Arial"),
                        FontSize = 30,
                        Text = "Other short information about team."
                    };

                    var teamNameStackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    teamNameStackPanel.Children.Add(teamNameTextBlock);
                    teamNameStackPanel.Children.Add(new TextBlock()
                    {
                        Text = "[",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48
                    });
                    teamNameStackPanel.Children.Add(teamShortNameTextBlock);
                    teamNameStackPanel.Children.Add(new TextBlock()
                    {
                        Text = "]",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 48
                    });

                    var teamLevelStackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    teamLevelStackPanel.Children.Add(new TextBlock()
                    {
                        Text = "Level: ",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36
                    });
                    teamLevelStackPanel.Children.Add(teamLevelTextBlock);

                    var teamEnergyStackPanel = new StackPanel()
                    {
                        Orientation = Orientation.Horizontal,
                        HorizontalAlignment = HorizontalAlignment.Right
                    };
                    teamEnergyStackPanel.Children.Add(new TextBlock()
                    {
                        Text = "Team Energy: ",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36
                    });
                    teamEnergyStackPanel.Children.Add(teamEnergyTextBlock);
                    teamEnergyStackPanel.Children.Add(new TextBlock()
                    {
                        Text = "/",
                        HorizontalAlignment = HorizontalAlignment.Right,
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 36
                    });
                    teamEnergyStackPanel.Children.Add(teamMaxEnergyTextBlock);

                    Grid00.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
                    Grid00.ColumnDefinitions.Add(new ColumnDefinition());
                    Grid00.Children.Add(teamNameStackPanel);
                    Grid00.Children.Add(teamLevelStackPanel);
                    Grid00.Children.Add(teamEnergyStackPanel);
                    Grid00.Children.Add(teamInfoTextBlock);

                    // Action buttons
                    Action1.Content = new TextBlock()
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        Text = "Solo Ranked Games"
                    };

                    Action2.Content = new TextBlock()
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        Text = "Group Trainings"
                    };

                    Action3.Content = new TextBlock()
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        TextWrapping = TextWrapping.Wrap,
                        TextAlignment = TextAlignment.Center,
                        Text = "Scrimmages with other teams in league"
                    };

                    Action4.Content = new TextBlock()
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        Text = "Ranked match"
                    };

                    Action5.Content = new TextBlock()
                    {
                        FontFamily = new FontFamily("Arial Black"),
                        FontSize = 28,
                        Text = "Current tournament"
                    };

                    // Characteristics
                    Expiriance.Text = "Value 1";
                    Health.Text = "Value 2";
                    Power.Text = "Value 3";
                    Teamplay.Text = "Value 4";
                    RankPoints.Text = "Value 5";
                    Rank.Content = "Team rank";
                    Trail.Text = "Team trail";

                    // Members
                    TopLiner.Content = "There is no topliner";
                    Jungler.Content = "There is no jungler";
                    Midliner.Content = "There is no midliner";
                    Bottliner.Content = "There is no botliner";
                    Support.Content = "There is no support";
                    break;
                case MenuPagesType.TopTeams:
                    break;
                case MenuPagesType.TopMembers:
                    break;
                case MenuPagesType.UserStats:
                    Page_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.6, GridUnitType.Star) });
                    Page_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                    Page_Grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(0.2, GridUnitType.Star) });

                    _innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    _innerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0.3, GridUnitType.Star) });

                    _stackPanel.Children.Add(new TextBlock
                    {
                        Name = "UserName",
                        Margin = new Thickness(10),
                        FontFamily = new FontFamily("Castellar"),
                        FontSize = 48,
                        FontWeight = FontWeights.Bold,
                        Text = "Adm",
                    });
                    _stackPanel.Children.Add(new ListBox
                    {
                        Name = "UserStatsList",
                        Margin = new Thickness(10),
                        FontFamily = new FontFamily("Castellar"),
                        FontSize = 18,
                        FontWeight = FontWeights.Bold,
                        MinHeight = 140,
                    });

                    var border = new Border
                    {
                        Margin = new Thickness(10),
                        Background = new SolidColorBrush(Color.FromArgb(25, 0, 0, 0)),
                        Child = new Image
                        {
                            Name = "UserRankImg",
                            Margin = new Thickness(3),
                            Source = new BitmapImage(new Uri("/Resources/Img/Default/icons8-ос-free-bsd-100-black.png", UriKind.Relative))
                        },
                    };

                    var groupBox = new GroupBox
                    {
                        Margin = new Thickness(5),
                        Header = new TextBlock
                        {
                            FontFamily = new FontFamily("Castellar"),
                            Margin = new Thickness(5),
                            FontSize = 24,
                            FontWeight = FontWeights.Bold,
                            Text = "Matches History"
                        },
                    };

                    var scrollViewer = new ScrollViewer
                    {
                        HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                        VerticalScrollBarVisibility = ScrollBarVisibility.Hidden,
                    };

                    var matchHistoryStack = new StackPanel { Name = "MatchHistoryStack" };

                    var matchHistoryList = new ListBox
                    {
                        Name = "MatchHistoryList",
                        Margin = new Thickness(3),
                        MinHeight = 50,
                    };

                    matchHistoryStack.Children.Add(matchHistoryList);
                    scrollViewer.Content = matchHistoryStack;
                    groupBox.Content = scrollViewer;

                    _innerGrid.Children.Add(_stackPanel);
                    _innerGrid.Children.Add(border);
                    Grid.SetColumn(_stackPanel, 0);
                    Grid.SetColumn(border, 1);
                    Grid.SetRow(groupBox, 1);

                    Page_Grid.Children.Add(_innerGrid);
                    Page_Grid.Children.Add(groupBox);
                    break;
            }
        }

        private void AddMemberButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddMembersViewsOnPage(StackPanel membersStack)
        {
            var members = _buffer.User?.Team.Members;

            if (members != null)
            {
                foreach (var member in members)
                {
                    membersStack.Children.Add(GetMemberViews(member));
                }
            }
        }

        private Frame GetMemberViews(MemberModel member)
        {
            return new Frame
            {
                Name = member.Name,
                NavigationUIVisibility = NavigationUIVisibility.Hidden,
                MinWidth = 160,
            };
        }
    }
}
