using splatoon3Tester.data;
using splatoon3Tester.type;
using System.Globalization;

namespace splatoon3Tester
{
    public partial class GearItemCtrl : UserControl
    {
        private GearItem gear;

        public GearItemCtrl()
        {
            InitializeComponent();
        }

        private void GearItemCtrl_Load(object sender, EventArgs e)
        {
            fillMAbilities(gearMAComboBox);
            fillAbilities(gearS1ComboBox);
            fillAbilities(gearS2ComboBox);
            fillAbilities(gearS3ComboBox);
            gearMAComboBox.SelectedIndex = 0;
            gearS1ComboBox.SelectedIndex = 0;
            gearS2ComboBox.SelectedIndex = 0;
            gearS3ComboBox.SelectedIndex = 0;
        }

        private void fillAbilities(ComboBox ctrl)
        {
            ctrl.Items.Add("无技能");
            ctrl.Items.AddRange(AbilityDex.Abilities());
        }

        private void fillMAbilities(ComboBox ctrl)
        {
            ctrl.Items.AddRange(AbilityDex.Abilities());
            ctrl.Items.AddRange(AbilityDex.MAbilities());
        }

        public void setGear(GearItem gear)
        {
            this.gear = gear;
            label1.Text = $"[{gear.TypeName}]{gear.GearName}【稀有度 {gear.star}】";
            gearExpNumUpDown.Value = gear.gearExp;
            gearMAComboBox.SelectedIndex = gear.mainAbility;
            gearS1ComboBox.SelectedIndex = gear.sub1Ability;
            gearS2ComboBox.SelectedIndex = gear.sub2Ability;
            gearS3ComboBox.SelectedIndex = gear.sub3Ability;
            seedBox.Text = $"0x{gear.gearSeed:X08}";
        }

        public uint getSeed() => Convert.ToUInt32(seedBox.Text, 16);
    }
}
