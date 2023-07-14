using System.Security.Policy;
using System.Windows;
using System.Windows.Input;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;
using TeamsLeague.UI.WPF.Configuration;
using TeamsLeague.UI.WPF.Views.Pages.Menu;
using TeamsLeague.UI.WPF.Views.Windows.Member;
using Unity.Resolution;

namespace TeamsLeague.UI.WPF.Views.Windows
{
    public partial class MemberDetailsWindow : Window
    {
        private readonly IMemberService _memberService;
        private readonly MemberModel _memberModel;


        public MemberDetailsWindow(IMemberService memberService, int memberId)
        {
            _memberService = memberService;
            _memberModel = _memberService.GetMember(memberId);

            InitializeComponent();
            BuildComponent();
        }


        private void GameWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuildComponent()
        {
            MemberNameValue.Text = _memberModel.Name;

            RankPointsValue.Text = _memberModel.RankPoints.ToString();

            ExperienceValue.Text = _memberModel.Experience.ToString();

            //MainPositionImg.Text = new BitmapImage(new Uri(ImagesService.GetPositionImageUrl(_memberModel.MainPosition), UriKind.Relative));

            MainPositionValue.Text = _memberModel.MainPosition.ToString();

            AgeValue.Text = _memberModel.Age.ToString();

            AttackValue.Text = _memberModel.Attack.ToString();

            DefenseValue.Text = _memberModel.Defense.ToString();

            MentalPowerValue.Text = _memberModel.MentalPower.ToString();

            MentalHealthValue.Value = _memberModel.MentalHealth;
            MentalHealthValue.Maximum = _memberModel.MaxMentalHealth;

            EnergyValue.Value = _memberModel.Energy;
            EnergyValue.Maximum = _memberModel.MaxEnergy;

            TeamplayValue.Text = _memberModel.Teamplay.ToString();
        }

        private void LvlUp_Button_Click(object sender, RoutedEventArgs e)
        {
            var levelUp = UnityContainerProvider.GetNew<MemberLevelUpWindow>(new ParameterOverride("memberModel", _memberModel));
            levelUp.ShowDialog();
        }
    }
}
