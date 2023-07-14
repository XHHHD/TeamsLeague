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
        private readonly MemberModel _memberModel;

        private List<Button> LevelUpButtons { get; set; }


        public MemberLevelUpWindow(IMemberService memberService, MemberModel memberModel)
        {
            _memberService = memberService;
            _memberModel = memberModel;
            LevelUpButtons = new();

            InitializeComponent();
            BuildComponent();
        }


        private void LvlUp_Button_Click(object sender, RoutedEventArgs e)
        {
            if (_memberModel.SkillPoints > 0)
            {
                if (sender is Button button)
                {
                    if (button.Tag is LevelUpTypes type)
                    {
                        //_memberModel = _memberService.LevelUp(type);
                        BuildComponent();
                    }
                }
            }
        }

        private void BuildComponent()
        {
            FreeSkillPointsValue.Content = _memberModel.SkillPoints;

            Attack_LvlUp_Button.Tag = LevelUpTypes.Attack;
            Defense_LvlUp_Button.Tag = LevelUpTypes.Defense;
            MentalPower_LvlUp_Button.Tag = LevelUpTypes.MentalPower;
            MentalHealth_LvlUp_Button.Tag = LevelUpTypes.MentalHealth;
            Energy_LvlUp_Button.Tag = LevelUpTypes.Energy;
            Teamplay_LvlUp_Button.Tag = LevelUpTypes.Teamplay;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
