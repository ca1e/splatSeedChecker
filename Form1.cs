using splatoon3Tester.editor;
using splatoon3Tester.type;
using System.Linq.Expressions;

namespace splatSeedChecker;

public partial class Form1 : Form
{
    private readonly SplatEditor editor = new();
    private readonly List<GearItem> gearItems = new();

    public Form1()
    {
        InitializeComponent();
    }

    private void refreshData()
    {
        gearItems.Clear();
        for (var i = 0; i < 3; i++)
        {
            gearItems.AddRange(editor.getGears(i));
        }
    }

    public void showGear()
    {
        gearlistBox.DataSource = gearItems.Where(i => (int)i.type == gearTypeControl.SelectedIndex).ToArray();

        label2.Text = $"总数: {gearlistBox.Items.Count}";
    }

    private async void buttonConnect_Click(object sender, EventArgs e)
    {
        buttonConnect.Enabled = false;

        try
        {
            await Task.Run(() => {
                editor.connect(ipTextBox.Text);
                editor.init();
            });
        }
        catch (Exception ex)
        {
            MessageBox.Show($"connect error {ex.Message}");
            buttonConnect.Enabled = true;
            return;
        }

        buttonDisconnect.Enabled = true;
        buttonRefresh.Enabled = true;
        refreshData();
        showGear();
    }

    private void buttonDisconnect_Click(object sender, EventArgs e)
    {
        editor.close();
    }

    private async void buttonRefresh_Click(object sender, EventArgs e)
    {
        buttonRefresh.Enabled = false;
        await Task.Run(() => {
            refreshData();
        });
        buttonRefresh.Enabled = true;
        gearTypeControl.SelectedIndex = 0;
    }

    private void gearTypeControl_SelectedIndexChanged(object sender, EventArgs e)
    {
        showGear();
    }

    private void gearlistBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var item = gearlistBox.SelectedItem as GearItem;
        gearItemCtrl1.setGear(item);
    }

    private void buttonSeedWrite_Click(object sender, EventArgs e)
    {
        var item = (GearItem)gearlistBox.SelectedItem;
        try
        {

            item.gearSeed = gearItemCtrl1.getSeed();
            editor.setGear(item);
            MessageBox.Show($"success");
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }
}
