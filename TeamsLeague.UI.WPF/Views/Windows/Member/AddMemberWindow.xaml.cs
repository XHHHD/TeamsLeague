using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants.Member;

namespace TeamsLeague.UI.WPF.Views.Windows.Member
{
    public partial class AddMemberWindow : Window
    {
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;
        private readonly IEnumerable<MemberModel> _potentialMembers;
        private readonly List<PositionType> _potentialPositions;
        private readonly List<PlaystyleType> _potentialPlayStyle;
        private readonly TeamModel _teamModel;


        private IMemberBuilder MemberBuilder { get; set; }


        public AddMemberWindow(ITeamService teamService, IMemberService memberService, IMemberBuilder memberBuilder, int teamId, List<PositionType> potentialPositions)
        {
            _teamService = teamService;
            _memberService = memberService;
            _potentialPositions = potentialPositions;

            MemberBuilder = memberBuilder;

            _potentialPlayStyle = Enum.GetValues<PlaystyleType>().Where(s => s != PlaystyleType.Initiator).ToList();
            _teamModel = _teamService.GetTeam(teamId);
            _potentialMembers = _memberService.GetAllFreeMembers().Where(m => potentialPositions.Contains(m.MainPosition));

            InitializeComponent();
            BuildComponent();
        }


        private void IsMemberExist_Checked(object sender, RoutedEventArgs e)
        {
            if (IsMemberExist.IsChecked is true)
            {
                PotentialMembers.IsEnabled = true;
                MemberName.IsEnabled = false;
                PositionsList.IsEnabled = false;
            }
            else
            {
                PotentialMembers.IsEnabled = false;
                MemberName.IsEnabled = true;
                PositionsList.IsEnabled = true;
            }
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsMemberExist.IsChecked == true)
            {
                if (PotentialMembers.SelectedItem != null)
                {
                    if (PotentialMembers.SelectedItem is ComboBoxItem selectedItem)
                    {
                        if (selectedItem.Tag is MemberModel member)
                        {
                            _teamService.AddMemberToTheTeam(_teamModel.Id, member.Id);

                            _teamModel.Members.Add(member);

                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please choose existed member!");
                }
            }
            else
            {
                MemberBuilder = MemberName.Text.IsNullOrEmpty()
                    ? MemberBuilder.GenerateBasicStats() : MemberBuilder.GenerateBasicStats(MemberName.Text);

                MemberBuilder = PlayStylesList.SelectedItem is ComboBoxItem playStyleSelectedItem && playStyleSelectedItem.Tag is PlaystyleType playStyleType
                    ? MemberBuilder.ChoosePlaystyle(playStyleType) : MemberBuilder;

                MemberBuilder = PositionsList.SelectedItem is ComboBoxItem positionSelectedItem && positionSelectedItem.Tag is PositionType positionType
                    ? MemberBuilder.AddPosition(positionType) : MemberBuilder.AddPosition();

                var member = MemberBuilder.Build();

                _memberService.CreateMember(member);

                _teamService.AddMemberToTheTeam(_teamModel.Id, member.Id);

                _teamModel.Members.Add(member);

                this.Close();
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuildComponent()
        {
            IsMemberExist.IsChecked = false;
            PotentialMembers.IsEnabled = false;

            foreach (var type in _potentialPositions)
            {
                PositionsList.Items.Add(new ComboBoxItem
                {
                    Content = type.ToString(),
                    Tag = type,
                });
            }

            foreach (var type in _potentialPlayStyle)
            {
                PlayStylesList.Items.Add(new ComboBoxItem
                {
                    Content = type.ToString(),
                    Tag = type,
                });
            }

            foreach (var member in _potentialMembers)
            {
                PotentialMembers.Items.Add(new ComboBoxItem
                {
                    Content = member.Name,
                    Tag = member,
                });
            }
        }
    }
}
