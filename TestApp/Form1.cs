using SharpKey;
using System.Windows.Forms;

namespace TestApp
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			hotkeys = new Hotkey[]
			{
				Hotkey.Create(Handle, 0, FSModifiers.Alt, Keys.T),
				Hotkey.Create(Handle, 1, FSModifiers.Alt, Keys.M),
			};

			foreach(Hotkey hotkey in hotkeys)
			{
				hotkey.Hook();
			}
		}

		private Hotkey[] hotkeys;

		protected override void WndProc(ref Message m)
		{
			if (m.Msg == Hotkey.HOTKEY)
			{
				if (m.WParam == hotkeys[0].Id_Ptr)
				{
					if (hotkeys[1].Enabled)
						hotkeys[1].Unhook(false);
					else
						hotkeys[1].Hook(false);

					MessageBox.Show($"Current hotkey state: {(hotkeys[1].Enabled ? "enabled" : "disabled")}.",
						"Current Hotkey State", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
				else if (hotkeys[1].Enabled && m.WParam == hotkeys[1].Id_Ptr)
				{
					MessageBox.Show("You used the hotkey! Good jab!", "Good jab!",
						MessageBoxButtons.OK,
						MessageBoxIcon.Information);
				}
			}

			base.WndProc(ref m);
		}
	}
}
