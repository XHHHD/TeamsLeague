using Azure;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Views.Pages.Events.SoloRank;
using Unity.Injection;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Windows.TeamEvents
{
    public partial class SoloRankWindow : Window
    {
        private readonly IGameMech _gameMech;
        private readonly IMemberService _memberService;
        private readonly int _teamId;

        private IEnumerable<MemberModel> Members { get; set; }
        private int MatchId { get; set; }

        public SoloRankWindow(IGameMech gameMech, IMemberService memberService, int teamId)
        {
            _gameMech = gameMech;
            _memberService = memberService;
            _teamId = teamId;

            Members = new List<MemberModel>();

            InitializeComponent();

            ShowPreview();
        }


        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Next_Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is MemberModel member)
                {
                    ShowMatch(member.Id);
                }
                else
                if (button.Tag is int memberId)
                {
                    MatchId = memberId;
                    ShowResults();
                }
                else
                if (button.Tag is SRMatchPage match)
                {
                    match.Match();
                }
                else
                if (button.Tag is null)
                {
                    Close();
                }
            }

            //var nextButton = FindNextButton();
            //MoveButtonts.Children.Remove(nextButton);
        }

        internal protected void CreateAndShowNextButton(object? tag, bool toShow, string buttonName = "NEXT")
        {
            var oldButton = FindNextButton();
            if (oldButton is not null)
            {
                MoveButtonts.Children.Remove(oldButton);
            }

            var nextButton = new Button
            {
                Content = buttonName,
                Margin = Back_Button.Margin,
                Width = Back_Button.Width,
                Height = Back_Button.Height,
                FontFamily = Back_Button.FontFamily,
                FontSize = Back_Button.FontSize,
                FontWeight = Back_Button.FontWeight,
                Background = Back_Button.Background,
                Foreground = Back_Button.Foreground,
                HorizontalContentAlignment = Back_Button.HorizontalContentAlignment,
                VerticalContentAlignment = Back_Button.VerticalContentAlignment,
                Tag = tag,
            };
            nextButton.Click += Next_Button_Click;
            Grid.SetColumn(nextButton, 0);

            if (toShow)
            {
                MoveButtonts.Children.Add(nextButton);
            }
        }

        private Button? FindNextButton()
        {
            foreach (var child in MoveButtonts.Children)
            {
                if (child is Button button && button.Content.ToString() == "NEXT")
                {
                    return button;
                }
            }

            return null;
        }

        internal void DeleteBackButton()
        {
            Button? buttonForRemove = null;

            foreach (var child in MoveButtonts.Children)
            {
                if (child is Button button && button.Content.ToString() == "BACK")
                {
                    buttonForRemove = button;
                }
            }

            if (buttonForRemove is not null)
            {
                MoveButtonts.Children.Remove(buttonForRemove);
            }
        }

        private void ShowPreview()
        {
            var preview = UnityContainerProvider.GetNew<SRPreviewPage>(new ParameterOverride("teamId", _teamId), new ParameterOverride("eventWindow", this));
            SetPage(preview);
        }

        private void ShowMatch(int memberId)
        {
            var match = UnityContainerProvider.GetNew<SRMatchPage>(new ParameterOverride("memberId", memberId), new ParameterOverride("eventWindow", this));
            SetPage(match);
        }

        private void ShowResults()
        {
            var results = UnityContainerProvider.GetNew<SRResultsPage>(new ParameterOverride("matchId", MatchId), new ParameterOverride("eventWindow", this));
            SetPage(results);
        }

        private void SetPage(Page page)
        {
            Event_Frame.Content = page;
            EventStadiaName.Content = page.Title;
        }
    }
}
