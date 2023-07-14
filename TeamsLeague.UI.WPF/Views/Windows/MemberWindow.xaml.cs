using System.Windows;
using System.Windows.Input;
using TeamsLeague.BLL.Interfaces;
using TeamsLeague.BLL.Models.MemberParts;

namespace TeamsLeague.UI.WPF.Views.Windows
{
    public partial class MemberWindow : Window
    {
        private readonly IMemberService _memberService;
        private readonly MemberModel _memberModel;


        public MemberWindow(IMemberService memberService, int memberId)
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
    }
}
