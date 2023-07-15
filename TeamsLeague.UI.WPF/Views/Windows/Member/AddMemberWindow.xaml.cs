using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.BLL.Models.TeamParts;
using TeamsLeague.DAL.Constants;

namespace TeamsLeague.UI.WPF.Views.Windows.Member
{
    public partial class AddMemberWindow : Window
    {
        private readonly ITeamService _teamService;
        private readonly IMemberService _memberService;
        private readonly IMemberBuilder _memberBuilder;
        private readonly IEnumerable<MemberModel> _potentialMembers;
        private readonly List<PositionType> _potentialPositions;
        private readonly TeamModel _teamModel;


        public AddMemberWindow(ITeamService teamService, IMemberService memberService, IMemberBuilder memberBuilder, int teamId, List<PositionType> potentialPositions)
        {
            _teamService = teamService;
            _memberService = memberService;
            _memberBuilder = memberBuilder;
            _potentialPositions = potentialPositions;
            _teamModel = _teamService.GetTeam(teamId);
            _potentialMembers = _memberService.GetAllFreeMembers().Where(m => potentialPositions.Contains(m.MainPosition));

            InitializeComponent();
            BuildComponent();
        }


        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if (IsMemberThisExist.IsChecked == true && PotentialMembers.SelectedItem != null)
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
            if (PositionsList.SelectedItem is ComboBoxItem selectedItem)
            {
                if (selectedItem.Tag is PositionType type)
                {
                    var member = MemberName.Text.IsNullOrEmpty()
                        ? _memberBuilder.GenerateBasicStats().AddPosition(type).Build()
                        : _memberBuilder.GenerateBasicStats(MemberName.Text).AddPosition(type).Build();
                    _memberService.CreateMember(member);

                    _teamService.AddMemberToTheTeam(_teamModel.Id, member.Id);

                    _teamModel.Members.Add(member);

                    this.Close();
                }
            }
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuildComponent()
        {
            IsMemberThisExist.IsChecked = false;

            foreach (var type in _potentialPositions)
            {
                PositionsList.Items.Add(new ComboBoxItem
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
