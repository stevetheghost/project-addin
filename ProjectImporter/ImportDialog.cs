using System;
using System.Windows.Forms;

namespace ProjectImporter
{
    public class ImportDialog : Form
    {
        private readonly TargetEnvironment environment;
        private readonly RadioButton projectRadio = new RadioButton { Text = "Project", Checked = true, AutoSize = true };
        private readonly RadioButton templateRadio = new RadioButton { Text = "Template", AutoSize = true };
        private readonly ComboBox calendarCombo = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
        private readonly TextBox projectIdBox = new TextBox { Dock = DockStyle.Fill };
        private readonly Button importButton = new Button { Text = "Import", AutoSize = true };

        public ImportDialog(TargetEnvironment environment)
        {
            this.environment = environment;

            Text = "Import (" + environment + ")";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            AcceptButton = importButton;

            calendarCombo.Items.AddRange(new object[] { "5 Day Calendar", "7 Day Calendar", "24 Hour Calendar" });
            calendarCombo.SelectedIndex = 0;

            var typePanel = new FlowLayoutPanel { AutoSize = true, Margin = Padding.Empty };
            typePanel.Controls.Add(projectRadio);
            typePanel.Controls.Add(templateRadio);

            var layout = new TableLayoutPanel
            {
                ColumnCount = 2,
                AutoSize = true,
                Dock = DockStyle.Fill,
                Padding = new Padding(12)
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 220));

            AddRow(layout, "Type", typePanel);
            AddRow(layout, "Calendar", calendarCombo);
            AddRow(layout, "Project ID", projectIdBox);

            importButton.Anchor = AnchorStyles.Right;
            importButton.Click += OnImportClick;
            layout.Controls.Add(importButton, 1, 3);

            Controls.Add(layout);
        }

        private static void AddRow(TableLayoutPanel layout, string label, Control input)
        {
            int row = layout.RowCount++;
            layout.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left, Margin = new Padding(3, 6, 12, 6) }, 0, row);
            layout.Controls.Add(input, 1, row);
        }

        private void OnImportClick(object sender, EventArgs e)
        {
            string projectId = projectIdBox.Text.Trim();
            if (projectId.Length == 0)
            {
                MessageBox.Show(this, "Please enter a Project ID.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                projectIdBox.Focus();
                return;
            }
            if (calendarCombo.SelectedIndex < 0)
            {
                MessageBox.Show(this, "Please select a calendar.", "Import", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                calendarCombo.Focus();
                return;
            }

            try
            {
                Importer.Import(
                    environment,
                    templateRadio.Checked,
                    (string)calendarCombo.SelectedItem,
                    projectId);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Import failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
