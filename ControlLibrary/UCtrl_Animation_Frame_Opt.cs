﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlLibrary
{
    public partial class UCtrl_Animation_Frame_Opt : UserControl
    {
        private bool setValue; // режим задания параметров

        private List<string> ListImagesFullName = new List<string>(); // перечень путей к файлам с картинками
        public Object _AnimationFrame;
        int comboBox_SelectedIndex = -1;

        public UCtrl_Animation_Frame_Opt()
        {
            InitializeComponent();
            setValue = false;
        }

        /// <summary>Задает индекс выбраной анимации</summary>
        public void SetAnimationIndex(int index)
        {
            setValue = true;
            comboBox_select_anim.SelectedIndex = index;
            setValue = false;
        }

        /// <summary>Задает количество анимаций в выпадающем списке</summary>
        public void SetAnimationCount(int count)
        {
            setValue = true;
            comboBox_select_anim.Items.Clear();
            for(int i = 1; i < count+1; i++)
            {
                comboBox_select_anim.Items.Add(i.ToString());
            }
            if(count>=13)button_add.Enabled = false; 
            else button_add.Enabled = true;
            setValue = false;
        }


        /// <summary>Задает название выбранной картинки</summary>
        public void SetImage(string value)
        {
            setValue = true;
            comboBox_image.Text = value;
            if (comboBox_image.SelectedIndex < 0) comboBox_image.Text = "";
            setValue = false;
        }

        /// <summary>Возвращает название выбранной картинки</summary>
        public string GetImage()
        {
            if (comboBox_image.SelectedIndex < 0) return "";
            return comboBox_image.Text;
        }

        /// <summary>Возвращает SelectedIndex выпадающего списка</summary>
        public int GetSelectedIndexImage()
        {
            return comboBox_image.SelectedIndex;
        }

        /// <summary>Возвращает префикс имени файла анимации</summary>
        public string GetPrefix()
        {
            string prefix = "-";
            if (comboBox_image.SelectedIndex >= 0)
            {
                string images_full_name = ListImagesFullName[comboBox_image.SelectedIndex];
                string fileName = Path.GetFileNameWithoutExtension(images_full_name);
                string dirName = Path.GetDirectoryName(images_full_name);
                int index = fileName.LastIndexOf("_");
                //if (fileName.IndexOf("-") > index) index = fileName.IndexOf("-");
                if (index >= 0)
                {
                    //int count = -1;
                    if (index > 0)
                    {
                        prefix = fileName.Substring(0, index + 1);
                        string[] files_anim = Directory.GetFiles(dirName, prefix + "*.png", SearchOption.AllDirectories);
                        //if (files_anim.Length > 0) count = files_anim.Length;
                        //if (count > 0) numericUpDown_images_count.Value = count;
                        prefix = fileName.Substring(0, index);
                    }
                }
            }
            return prefix;
        }

        [Browsable(true)]
        [Description("Происходит при изменении выбора элемента")]
        public event ValueChangedHandler ValueChanged;
        public delegate void ValueChangedHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при удалении анимации")]
        public event AnimationDelHandler AnimationDel;
        public delegate void AnimationDelHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при добавлении анимации")]
        public event AnimationAddHandler AnimationAdd;
        public delegate void AnimationAddHandler(object sender, EventArgs eventArgs, int index);

        [Browsable(true)]
        [Description("Происходит при изменении выбраной анимации")]
        public event AnimIndexChangedHandler AnimIndexChanged;
        public delegate void AnimIndexChangedHandler(object sender, EventArgs eventArgs, int index);

        private void checkBox_Click(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }

        #region Standard events
        private void comboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Delete) || (e.KeyCode == Keys.Back))
            {
                ComboBox comboBox = sender as ComboBox;
                comboBox.Text = "";
                comboBox.SelectedIndex = -1;
                if (ValueChanged != null && !setValue)
                {
                    EventArgs eventArgs = new EventArgs();
                    ValueChanged(this, eventArgs, comboBox_select_anim.SelectedIndex);
                }
            }
        }

        private void comboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            //if (comboBox.Items.Count < 10) comboBox.DropDownHeight = comboBox.Items.Count * 35;
            //else comboBox.DropDownHeight = 106;
            float size = comboBox.Font.Size;
            Font myFont;
            FontFamily family = comboBox.Font.FontFamily;
            e.DrawBackground();
            int itemWidth = e.Bounds.Height;
            int itemHeight = e.Bounds.Height - 4;

            if (e.Index >= 0)
            {
                try
                {
                    using (FileStream stream = new FileStream(ListImagesFullName[e.Index], FileMode.Open, FileAccess.Read))
                    {
                        Image image = Image.FromStream(stream);
                        float scale = (float)itemWidth / image.Width;
                        if ((float)itemHeight / image.Height < scale) scale = (float)itemHeight / image.Height;
                        float itemWidthRec = image.Width * scale;
                        float itemHeightRec = image.Height * scale;
                        Rectangle rectangle = new Rectangle((int)(itemWidth - itemWidthRec) / 2 + 2,
                            e.Bounds.Top + (int)(itemHeight - itemHeightRec) / 2 + 2, (int)itemWidthRec, (int)itemHeightRec);
                        e.Graphics.DrawImage(image, rectangle);
                    }
                }
                catch { }
            }
            //e.Graphics.DrawImage(imageList1.Images[e.Index], rectangle);
            myFont = new Font(family, size);
            StringFormat lineAlignment = new StringFormat();
            //lineAlignment.Alignment = StringAlignment.Center;
            lineAlignment.LineAlignment = StringAlignment.Center;
            if (e.Index >= 0)
                e.Graphics.DrawString(comboBox.Items[e.Index].ToString(), myFont, System.Drawing.Brushes.Black, new RectangleF(e.Bounds.X + itemWidth, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height), lineAlignment);
            e.DrawFocusRectangle();
        }

        private void comboBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 35;
        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox_SelectedIndex = comboBox_image.SelectedIndex;
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }
        #endregion

        #region Settings Set/Clear
        /// <summary>Добавляет ссылки на картинки в выпадающие списки</summary>
        public void ComboBoxAddItems(List<string> ListImages, List<string> _ListImagesFullName)
        {
            comboBox_image.Items.Clear();

            comboBox_image.Items.AddRange(ListImages.ToArray());

            ListImagesFullName = _ListImagesFullName;

            int count = ListImages.Count;
            if (count == 0)
            {
                comboBox_image.DropDownHeight = 1;
            }
            else if (count < 5)
            {
                comboBox_image.DropDownHeight = 35 * count + 1;
            }
            else
            {
                comboBox_image.DropDownHeight = 106;
            }
        }

        /// <summary>Очищает выпадающие списки с картинками, сбрасывает данные на значения по умолчанию</summary>
        public void SettingsClear()
        {
            setValue = true;

            comboBox_image.Text = null;
            comboBox_select_anim.Text = null;

            numericUpDown_imageX.Value = 0;
            numericUpDown_imageY.Value = 0;

            numericUpDown_images_count.Value = 10;
            numericUpDown_fps.Value = 15;
            numericUpDown_preview_frame.Value = 1;

            checkBox_anim_repeat.Checked = true;
            //checkBox_anim_restart.Checked = false;
            checkBox_visible.Checked = true;

            label_prefix.Text = "-";

            setValue = false;
        }
        #endregion

        #region contextMenu
        private void contextMenuStrip_X_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_X.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_X.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_X.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_X.Items[2].Enabled = false;
            }
        }

        private void contextMenuStrip_Y_Opening(object sender, CancelEventArgs e)
        {
            if ((MouseСoordinates.X < 0) || (MouseСoordinates.Y < 0))
            {
                contextMenuStrip_Y.Items[0].Enabled = false;
            }
            else
            {
                contextMenuStrip_Y.Items[0].Enabled = true;
            }
            decimal i = 0;
            if ((Clipboard.ContainsText() == true) && (decimal.TryParse(Clipboard.GetText(), out i)))
            {
                contextMenuStrip_Y.Items[2].Enabled = true;
            }
            else
            {
                contextMenuStrip_Y.Items[2].Enabled = false;
            }
        }

        private void вставитьКоординатуХToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.X;
                }
            }
        }

        private void вставитьКоординатуYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    numericUpDown.Value = MouseСoordinates.Y;
                }
            }
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a ToolStripItem
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    Clipboard.SetText(numericUpDown.Value.ToString());
                }
            }
        }

        private void вставитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem menuItem = sender as ToolStripItem;
            if (menuItem != null)
            {
                // Retrieve the ContextMenuStrip that owns this ToolStripItem
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    // Get the control that is displaying this context menu
                    Control sourceControl = owner.SourceControl;
                    NumericUpDown numericUpDown = sourceControl as NumericUpDown;
                    //Если в буфере обмен содержится текст
                    if (Clipboard.ContainsText() == true)
                    {
                        //Извлекаем (точнее копируем) его и сохраняем в переменную
                        decimal i = 0;
                        if (decimal.TryParse(Clipboard.GetText(), out i))
                        {
                            if (i > numericUpDown.Maximum) i = numericUpDown.Maximum;
                            if (i < numericUpDown.Minimum) i = numericUpDown.Minimum;
                            numericUpDown.Value = i;
                        }
                    }

                }
            }
        }
        #endregion

        #region numericUpDown
        private void numericUpDown_picturesX_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.X < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.X;
            }
        }

        private void numericUpDown_picturesY_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (MouseСoordinates.Y < 0) return;
            NumericUpDown numericUpDown = sender as NumericUpDown;
            if (e.X <= numericUpDown.Controls[1].Width + 1)
            {
                // Click is in text area
                numericUpDown.Value = MouseСoordinates.Y;
            }
        }

        private void numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (ValueChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                ValueChanged(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }

        #endregion

        private void comboBox_select_anim_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_select_anim.SelectedIndex >= 0) button_del.Enabled = true;
            else button_del.Enabled = false;

            if (AnimIndexChanged != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                AnimIndexChanged(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (AnimationAdd != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                AnimationAdd(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }

        private void button_del_Click(object sender, EventArgs e)
        {
            if (AnimationDel != null && !setValue)
            {
                EventArgs eventArgs = new EventArgs();
                AnimationDel(this, eventArgs, comboBox_select_anim.SelectedIndex);
            }
        }

        private void comboBox_image_DropDownClosed(object sender, EventArgs e)
        {
            setValue = true;
            if (comboBox_image.SelectedIndex == comboBox_SelectedIndex) return;
            string prefix = "-";
            if (comboBox_image.SelectedIndex >= 0)
            {
                string images_full_name = ListImagesFullName[comboBox_image.SelectedIndex];
                string fileName = Path.GetFileNameWithoutExtension(images_full_name);
                string dirName = Path.GetDirectoryName(images_full_name);
                int index = fileName.LastIndexOf("_");
                //if (fileName.IndexOf("-") > index) index = fileName.IndexOf("-");
                if (index >= 0)
                {
                    int count = -1;
                    if (index > 0)
                    {
                        prefix = fileName.Substring(0, index + 1);
                        string[] files_anim = Directory.GetFiles(dirName, prefix + "*.png", SearchOption.AllDirectories);
                        if (files_anim.Length > 0) count = files_anim.Length;
                        if (count > 0) 
                        {
                            numericUpDown_images_count.Value = count;
                            int frame_index = count / 2;
                            if (frame_index < 1) frame_index = 1;
                            numericUpDown_preview_frame.Maximum = 999;
                            numericUpDown_preview_frame.Value = frame_index;
                            numericUpDown_preview_frame.Maximum = count;
                        }
                        prefix = fileName.Substring(0, index);
                    }
                }
            }
            label_prefix.Text = prefix;

            setValue = false;

        }

        private void numericUpDown_image_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down))
            {
                NumericUpDown numericUpDown = sender as NumericUpDown;
                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_imageX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_imageX")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.UpButton();
                }

                if (e.KeyCode == Keys.Up && numericUpDown.Name == "numericUpDown_imageY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.DownButton();
                }
                if (e.KeyCode == Keys.Down && numericUpDown.Name == "numericUpDown_imageY")
                {
                    e.SuppressKeyPress = false;
                    numericUpDown_imageY.UpButton();
                }

                if (e.KeyCode == Keys.Left && (numericUpDown.Name == "numericUpDown_imageX" || numericUpDown.Name == "numericUpDown_imageY"))
                    numericUpDown_imageX.DownButton();
                if (e.KeyCode == Keys.Right && (numericUpDown.Name == "numericUpDown_imageX" || numericUpDown.Name == "numericUpDown_imageY"))
                    numericUpDown_imageX.UpButton();

                e.Handled = true;
            }
        }
    }
}
