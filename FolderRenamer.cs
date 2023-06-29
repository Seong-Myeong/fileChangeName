using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FolderRename
{
	public class FolderRenamer : Form
	{
		private OpenFileDialog opendialog = new OpenFileDialog();

		private ArrayList list_filepath = new ArrayList();

		private string open_directory = null;

		private ArrayList old_name = new ArrayList();

		private ArrayList new_name = new ArrayList();

		private IContainer components = null;

		private OpenFileDialog openFileDialog1;

		private Button btnOpen;

		private ListBox listFile;

		private TextBox firstNumber;

		private TextBox lastNumber;

		private Label label1;

		private Label label2;

		private Button btnChange;

		private Button file_list_reset_btn;

		private Button btnReturn;

		public FolderRenamer()
		{
			this.InitializeComponent();
		}

		private void btnChange_Click(object sender, EventArgs e)
		{
			string str;
			int num;
			string directoryName = Path.GetDirectoryName(this.list_filepath[0].ToString());
			int num1 = Convert.ToInt32(this.firstNumber.Text);
			int num2 = 0;
			if (this.list_filepath.Count == 0)
			{
				MessageBox.Show("변환할 파일이 없습니다.");
			}
			if (string.IsNullOrEmpty(this.lastNumber.Text))
			{
				num = 0;
			}
			else
			{
				num = Convert.ToInt32(this.lastNumber.Text);
				if (this.list_filepath.Count != num - num1 + 1)
				{
					MessageBox.Show("파일 개수와 번호 개수가 일치하지 않습니다.");
					this.firstNumber.Clear();
					this.lastNumber.Clear();
				}
			}
			for (int i = num1; i < num1 + this.list_filepath.Count; i++)
			{
				if (File.Exists(string.Concat(directoryName, "\\", i.ToString(), Path.GetExtension(this.list_filepath[num2].ToString()))))
				{
					MessageBox.Show("파일이 이미 존재합니다");
					this.firstNumber.Clear();
					this.lastNumber.Clear();
					i = num1 + this.list_filepath.Count - 1;
				}
				num2++;
			}
			if ((string.IsNullOrEmpty(this.firstNumber.Text) ? false : !string.IsNullOrEmpty(this.lastNumber.Text)))
			{
				this.old_name.Clear();
				this.new_name.Clear();
				num2 = 0;
				for (int j = num1; j <= num; j++)
				{
					str = string.Concat(directoryName, "\\", j.ToString(), Path.GetExtension(this.list_filepath[num2].ToString()));
					this.old_name.Add(this.list_filepath[num2].ToString());
					this.new_name.Add(str);
					File.Move(this.list_filepath[num2].ToString(), str);
					num2++;
				}
				MessageBox.Show("변환 성공!");
				this.btnReturn.Enabled = true;
				this.list_filepath.Clear();
				this.listFile.Items.Clear();
				this.firstNumber.Clear();
				this.lastNumber.Clear();
			}
			else if ((string.IsNullOrEmpty(this.firstNumber.Text) ? false : string.IsNullOrEmpty(this.lastNumber.Text)))
			{
				this.old_name.Clear();
				this.new_name.Clear();
				num2 = 0;
				for (int k = num1; k < num1 + this.list_filepath.Count; k++)
				{
					str = string.Concat(directoryName, "\\", k.ToString(), Path.GetExtension(this.list_filepath[num2].ToString()));
					this.old_name.Add(this.list_filepath[num2].ToString());
					this.new_name.Add(str);
					File.Move(this.list_filepath[num2].ToString(), str);
					num2++;
				}
				int count = num1 + this.list_filepath.Count - 1;
				MessageBox.Show(string.Concat(num1.ToString(), "부터 ", count.ToString(), "까지 변환 성공! "));
				this.btnReturn.Enabled = true;
				this.list_filepath.Clear();
				this.listFile.Items.Clear();
				this.firstNumber.Clear();
				this.lastNumber.Clear();
			}
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			string str;
			str = (!string.IsNullOrEmpty(this.open_directory) ? this.open_directory : Environment.CurrentDirectory);
			this.opendialog.InitialDirectory = str;
			this.opendialog.RestoreDirectory = true;
			this.opendialog.Title = "파일 선택";
			this.opendialog.DefaultExt = "*";
			this.opendialog.FileName = "";
			this.opendialog.Filter = "모든 파일 (*.*) | *.*";
			this.opendialog.Multiselect = true;
			if (this.opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				string[] fileNames = this.opendialog.FileNames;
				for (int i = 0; i < (int)fileNames.Length; i++)
				{
					string str1 = fileNames[i];
					if (!this.list_filepath.Contains(str1))
					{
						this.listFile.Items.Add(Path.GetFileNameWithoutExtension(str1));
						this.list_filepath.Add(str1);
					}
				}
			}
			this.open_directory = Path.GetDirectoryName(this.list_filepath[0].ToString());
		}

		private void btnReturn_Click(object sender, EventArgs e)
		{
			string str;
			string str1;
			Path.GetDirectoryName(this.old_name[0].ToString());
			for (int i = 0; i < this.old_name.Count; i++)
			{
				File.Move(this.new_name[i].ToString(), this.old_name[i].ToString());
			}
			object item = this.new_name[0];
			if (item != null)
			{
				str = item.ToString();
			}
			else
			{
				str = null;
			}
			object obj = this.new_name[this.old_name.Count - 1];
			if (obj != null)
			{
				str1 = obj.ToString();
			}
			else
			{
				str1 = null;
			}
			MessageBox.Show(string.Concat(str, "부터 ", str1, "까지를 되돌렸습니다."));
			this.old_name.Clear();
			this.new_name.Clear();
			this.btnReturn.Enabled = false;
		}

		protected override void Dispose(bool disposing)
		{
			if ((!disposing ? false : this.components != null))
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void file_list_reset_btn_Click(object sender, EventArgs e)
		{
			this.list_filepath.Clear();
			this.listFile.Items.Clear();
		}

		private void InitializeComponent()
		{
			this.openFileDialog1 = new OpenFileDialog();
			this.btnOpen = new Button();
			this.listFile = new ListBox();
			this.firstNumber = new TextBox();
			this.lastNumber = new TextBox();
			this.label1 = new Label();
			this.label2 = new Label();
			this.btnChange = new Button();
			this.file_list_reset_btn = new Button();
			this.btnReturn = new Button();
			base.SuspendLayout();
			this.openFileDialog1.FileName = "openFileDialog1";
			this.btnOpen.Location = new Point(26, 22);
			this.btnOpen.Name = "btnOpen";
			this.btnOpen.Size = new System.Drawing.Size(173, 64);
			this.btnOpen.TabIndex = 0;
			this.btnOpen.Text = "바꿀 파일 업로드";
			this.btnOpen.UseVisualStyleBackColor = true;
			this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
			this.listFile.AllowDrop = true;
			this.listFile.FormattingEnabled = true;
			this.listFile.ItemHeight = 15;
			this.listFile.Location = new Point(26, 112);
			this.listFile.Name = "listFile";
			this.listFile.ScrollAlwaysVisible = true;
			this.listFile.Size = new System.Drawing.Size(269, 229);
			this.listFile.TabIndex = 1;
			this.listFile.DragDrop += new DragEventHandler(this.listFile_DragDrop);
			this.listFile.DragEnter += new DragEventHandler(this.listFile_DragEnter);
			this.firstNumber.ForeColor = SystemColors.ActiveCaptionText;
			this.firstNumber.Location = new Point(318, 143);
			this.firstNumber.Name = "firstNumber";
			this.firstNumber.Size = new System.Drawing.Size(100, 25);
			this.firstNumber.TabIndex = 2;
			this.lastNumber.ForeColor = SystemColors.ActiveCaptionText;
			this.lastNumber.Location = new Point(492, 143);
			this.lastNumber.Name = "lastNumber";
			this.lastNumber.Size = new System.Drawing.Size(100, 25);
			this.lastNumber.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("굴림", 10.2f, FontStyle.Bold, GraphicsUnit.Point, 129);
			this.label1.Location = new Point(321, 114);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 17);
			this.label1.TabIndex = 3;
			this.label1.Text = "시작 숫자";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("굴림", 10.2f, FontStyle.Bold, GraphicsUnit.Point, 129);
			this.label2.Location = new Point(495, 114);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "끝 숫자";
			this.btnChange.Font = new System.Drawing.Font("굴림", 16.2f, FontStyle.Regular, GraphicsUnit.Point, 129);
			this.btnChange.Location = new Point(318, 200);
			this.btnChange.Name = "btnChange";
			this.btnChange.Size = new System.Drawing.Size(280, 141);
			this.btnChange.TabIndex = 4;
			this.btnChange.Text = "이름 바꾸기";
			this.btnChange.UseVisualStyleBackColor = true;
			this.btnChange.Click += new EventHandler(this.btnChange_Click);
			this.file_list_reset_btn.Location = new Point(442, 22);
			this.file_list_reset_btn.Name = "file_list_reset_btn";
			this.file_list_reset_btn.Size = new System.Drawing.Size(150, 41);
			this.file_list_reset_btn.TabIndex = 5;
			this.file_list_reset_btn.Text = "파일 리스트 초기화";
			this.file_list_reset_btn.UseVisualStyleBackColor = true;
			this.file_list_reset_btn.Click += new EventHandler(this.file_list_reset_btn_Click);
			this.btnReturn.Enabled = false;
			this.btnReturn.Location = new Point(318, 22);
			this.btnReturn.Name = "btnReturn";
			this.btnReturn.Size = new System.Drawing.Size(89, 41);
			this.btnReturn.TabIndex = 6;
			this.btnReturn.Text = "되돌리기";
			this.btnReturn.UseVisualStyleBackColor = true;
			this.btnReturn.Click += new EventHandler(this.btnReturn_Click);
			base.AutoScaleDimensions = new SizeF(8f, 15f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(647, 362);
			base.Controls.Add(this.btnReturn);
			base.Controls.Add(this.file_list_reset_btn);
			base.Controls.Add(this.btnChange);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.lastNumber);
			base.Controls.Add(this.firstNumber);
			base.Controls.Add(this.listFile);
			base.Controls.Add(this.btnOpen);
			base.Name = "FolderRenamer";
			base.StartPosition = FormStartPosition.CenterScreen;
			this.Text = "파일 이름 바꾸기";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		private void listFile_DragDrop(object sender, DragEventArgs e)
		{
			string[] data = (string[])e.Data.GetData(DataFormats.FileDrop, false);
			for (int i = 0; i < (int)data.Length; i++)
			{
				if (!this.list_filepath.Contains(data[i]))
				{
					this.listFile.Items.Add(Path.GetFileNameWithoutExtension(data[i]));
					this.list_filepath.Add(data[i]);
				}
			}
		}

		private void listFile_DragEnter(object sender, DragEventArgs e)
		{
			if (!e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.None;
			}
			else
			{
				e.Effect = DragDropEffects.All;
			}
		}
	}
}