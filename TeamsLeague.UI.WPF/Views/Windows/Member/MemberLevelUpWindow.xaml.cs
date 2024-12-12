using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models;
using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.UI.WPF.Views.Windows.Member
{
    public partial class MemberLevelUpWindow : Window
    {
        private readonly IMemberService _memberService;

        private MemberModel MemberModel { get; set; }
        private List<Button> LevelUpButtons { get; set; }


        public MemberLevelUpWindow(IMemberService memberService, MemberModel memberModel)
        {
            _memberService = memberService;
            MemberModel = memberModel;
            LevelUpButtons = new();

            InitializeComponent();
            BuildComponent();
        }


        private void LvlUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MemberModel.SkillPoints > 0)
            {
                if (sender is Button button)
                {
                    if (button.Tag is UsingSkillPointsTypes type)
                    {
                        MemberModel = _memberService.UseSkillPoints(MemberModel.Id, type);
                        BuildComponent();
                    }
                }
            }
        }

        private void BuildComponent()
        {
            FreeSkillPointsValue.Content = MemberModel.SkillPoints;

            Attack_LvlUp_Button.Tag = UsingSkillPointsTypes.Attack;
            Defense_LvlUp_Button.Tag = UsingSkillPointsTypes.Defense;
            MentalPower_LvlUp_Button.Tag = UsingSkillPointsTypes.MentalPower;
            MentalHealth_LvlUp_Button.Tag = UsingSkillPointsTypes.MentalHealth;
            Energy_LvlUp_Button.Tag = UsingSkillPointsTypes.Energy;
            Teamplay_LvlUp_Button.Tag = UsingSkillPointsTypes.Teamplay;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
