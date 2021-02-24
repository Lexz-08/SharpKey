using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace TestApp
{
	public partial class UrMomForm : Form
	{
		private IContainer components = null;

		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			SuspendLayout();
			closeButton = new Button();
			closeButton.SuspendLayout();
			// 
			// closeButton
			// 
			closeButton.Text = "Close the Form";
			closeButton.Name = "closeButton";
			closeButton.Size = new Size(141, 43);
			closeButton.Location = new Point(126, 111);
			closeButton.Anchor = AnchorStyles.None;
			closeButton.Click += new EventHandler(closeButton_Click);
			// 
			// Form1
			// 
			Controls.Add(closeButton);
			AutoScaleDimensions = new SizeF(6F, 13F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(392, 264);
			FormBorderStyle = FormBorderStyle.FixedSingle;
			MaximizeBox = false;
			Name = "Form1";
			StartPosition = FormStartPosition.CenterScreen;
			Text = "TestApp";
			ResumeLayout(false);
			closeButton.ResumeLayout(false);
		}

		private Button closeButton;

		public static UrMomForm Instance => new UrMomForm();

		public UrMomForm()
		{
			InitializeComponent();
		}

		private void closeButton_Click(object sender, EventArgs e) => Close();
	}
}
