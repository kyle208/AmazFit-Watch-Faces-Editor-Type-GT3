﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Watch_Face_Editor
{
    public partial class Form1 : Form
    {
        private WATCH_FACE TextToJson(string text)
        {
            WATCH_FACE Watch_Face_return = null;
            WATCH_FACE Watch_Face_temp = null;
            try
            {
                Watch_Face_temp = JsonConvert.DeserializeObject<WATCH_FACE>(text, new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                    Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (Watch_Face_temp == null) return Watch_Face_return;
            
            Watch_Face_return = new WATCH_FACE();
            if (Watch_Face_temp.WatchFace_Info != null) Watch_Face_return.WatchFace_Info = Watch_Face_temp.WatchFace_Info;
            if (Watch_Face_temp.ScreenNormal != null)
            {
                Watch_Face_return.ScreenNormal = new ScreenNormal();
                if (Watch_Face_temp.ScreenNormal.Background != null)
                    Watch_Face_return.ScreenNormal.Background = Watch_Face_temp.ScreenNormal.Background;
                if (Watch_Face_temp.ScreenNormal.Elements != null)
                {
                    Watch_Face_return.ScreenNormal.Elements = new List<object>();
                    List<object> NewElements = ObjectsToElements(Watch_Face_temp.ScreenNormal.Elements);
                    Watch_Face_return.ScreenNormal.Elements = NewElements;
                }
            }

            if (Watch_Face_temp.ScreenAOD != null)
            {
                Watch_Face_return.ScreenAOD = new ScreenAOD();
                if (Watch_Face_temp.ScreenAOD.Background != null)
                    Watch_Face_return.ScreenAOD.Background = Watch_Face_temp.ScreenAOD.Background;
                if (Watch_Face_temp.ScreenAOD.Elements != null)
                {
                    Watch_Face_return.ScreenAOD.Elements = new List<object>();
                    List<object> NewElements = ObjectsToElements(Watch_Face_temp.ScreenAOD.Elements);
                    Watch_Face_return.ScreenAOD.Elements = NewElements;
                }
            }

            return Watch_Face_return;
        }

        /// <summary>Распознаем конкретный тип объекта</summary>
        private List<object> ObjectsToElements(List<object> elements)
        {
            List<object> NewElements = new List<object>();
            foreach (object element in elements)
            {
                string elementStr = element.ToString();
                string type = GetTypeFromSring(elementStr);
                switch (type)
                {
                    #region DigitalTime
                    case "DigitalTime":
                        ElementDigitalTime DigitalTime = null;
                        try
                        {
                            DigitalTime = JsonConvert.DeserializeObject<ElementDigitalTime>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DigitalTime != null) NewElements.Add(DigitalTime);
                        break;
                    #endregion

                    #region AnalogTime
                    case "AnalogTime":
                        ElementAnalogTime AnalogTime = null;
                        try
                        {
                            AnalogTime = JsonConvert.DeserializeObject<ElementAnalogTime>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (AnalogTime != null) NewElements.Add(AnalogTime);
                        break;
                    #endregion

                    #region DateDay
                    case "DateDay":
                        ElementDateDay DateDay = null;
                        try
                        {
                            DateDay = JsonConvert.DeserializeObject<ElementDateDay>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateDay != null) NewElements.Add(DateDay);
                        break;
                    #endregion

                    #region DateMonth
                    case "DateMonth":
                        ElementDateMonth DateMonth = null;
                        try
                        {
                            DateMonth = JsonConvert.DeserializeObject<ElementDateMonth>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateMonth != null) NewElements.Add(DateMonth);
                        break;
                    #endregion

                    #region DateYear
                    case "DateYear":
                        ElementDateYear DateYear = null;
                        try
                        {
                            DateYear = JsonConvert.DeserializeObject<ElementDateYear>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateYear != null) NewElements.Add(DateYear);
                        break;
                    #endregion

                    #region DateWeek
                    case "DateWeek":
                        ElementDateWeek DateWeek = null;
                        try
                        {
                            DateWeek = JsonConvert.DeserializeObject<ElementDateWeek>(elementStr, new JsonSerializerSettings
                            {
                                //DefaultValueHandling = DefaultValueHandling.Ignore,
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(Properties.FormStrings.Message_JsonError_Text + Environment.NewLine + ex,
                                Properties.FormStrings.Message_Error_Caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        if (DateWeek != null) NewElements.Add(DateWeek);
                        break;
                        #endregion
                }
            }
            return NewElements;
        }
        /// <summary>Выделяем тип элемента из строки с параметрами</summary>
        private string GetTypeFromSring(string str)
        {
            string name = "elementName";
            string returnStr = "";
            int indexOf = str.IndexOf("elementName");
            if (indexOf >= 0)
            {
                str = str.Substring(indexOf + name.Length + 2);
                indexOf = str.IndexOf("\"");
                str = str.Substring(indexOf + 1);
                indexOf = str.IndexOf("\"");
                str = str.Substring(0, indexOf);
                returnStr = str;
            }
            return returnStr;
        }

        /// <summary>Читаем настройки для фона</summary>
        private void Read_Background_Options(Background background, string preview = "", int id = 0)
        {
            PreviewView = false;
            userCtrl_Background_Options.SettingsClear();

            if (preview != null && preview.Length > 0) userCtrl_Background_Options.SetPreview(preview);
            if (id > 999 && id < 10000000) userCtrl_Background_Options.SetID(id);
            if (background == null) 
            {
                PreviewView = true;
                return;
            }
            userCtrl_Background_Options.Visible = true;
            if (background == null)
            {
                PreviewView = true;
                return;
            }
            if (background.BackgroundColor != null)
            {
                userCtrl_Background_Options.SetColorBackground(StringToColor(background.BackgroundColor.color));
                userCtrl_Background_Options.Switch_ImageColor(1);
            }
            if (background.BackgroundImage != null)
            {
                userCtrl_Background_Options.SetBackground(background.BackgroundImage.src);
                userCtrl_Background_Options.Switch_ImageColor(0);
            }
            userCtrl_Background_Options._Background = background;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения числа картинками</summary>
        private void Read_ImgNumber_Options(hmUI_widget_IMG_NUMBER img_number, bool _dastance, bool _follow, string _followText,
            bool _imageError, bool _optionalSymbol, bool _padingZero)
        {
            PreviewView = false;

            uCtrl_Text_Opt.SettingsClear();

            uCtrl_Text_Opt.Distance = _dastance;
            uCtrl_Text_Opt.Follow = _follow;
            uCtrl_Text_Opt.FollowText = _followText;
            uCtrl_Text_Opt.ImageError = _imageError;
            uCtrl_Text_Opt.OptionalSymbol = _optionalSymbol;
            uCtrl_Text_Opt.PaddingZero = _padingZero;
            uCtrl_Text_Opt.Visible = true;

            //userCtrl_Background_Options.SettingsClear();

            if (img_number == null)
            {
                PreviewView = true; 
                return; 
            }
            if (img_number.img_First != null) 
                uCtrl_Text_Opt.SetImage(img_number.img_First);
            uCtrl_Text_Opt.numericUpDown_imageX.Value = img_number.imageX;
            uCtrl_Text_Opt.numericUpDown_imageY.Value = img_number.imageY;

            uCtrl_Text_Opt.SetIcon(img_number.icon);
            uCtrl_Text_Opt.numericUpDown_iconX.Value = img_number.iconPosX;
            uCtrl_Text_Opt.numericUpDown_iconY.Value = img_number.iconPosY;

            uCtrl_Text_Opt.SetUnit(img_number.unit);
            //uCtrl_Text_Opt.SetImageDecimalPointOrMinus
            uCtrl_Text_Opt.numericUpDown_spacing.Value = img_number.space;

            uCtrl_Text_Opt.SetAlignment(img_number.align);
            //uCtrl_Text_Opt.SetImageError

            uCtrl_Text_Opt.checkBox_addZero.Checked = img_number.zero;
            uCtrl_Text_Opt.checkBox_follow.Checked = img_number.follow;

            //uCtrl_Text_Opt.SetUnitMile

            uCtrl_Text_Opt._ElementWithText = img_number;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения стрелочного указателя</summary>
        private void Read_ImgPointer_Options(hmUI_widget_IMG_POINTER img_pointer, bool _showBackground)
        {
            PreviewView = false;

            uCtrl_Pointer_Opt.SettingsClear();

            uCtrl_Pointer_Opt.ShowBackground = _showBackground;
            uCtrl_Pointer_Opt.Visible = true;

            //userCtrl_Background_Options.SettingsClear();

            if (img_pointer == null)
            {
                PreviewView = true;
                return;
            }
            if (img_pointer.src != null)
                uCtrl_Pointer_Opt.SetPointerImage(img_pointer.src);
            uCtrl_Pointer_Opt.numericUpDown_pointer_X.Value = img_pointer.center_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_Y.Value = img_pointer.center_y;
            uCtrl_Pointer_Opt.numericUpDown_pointer_offset_X.Value = img_pointer.pos_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_offset_Y.Value = img_pointer.pos_y;

            if (img_pointer.cover_path != null)
                uCtrl_Pointer_Opt.SetPointerImageCentr(img_pointer.cover_path);
            uCtrl_Pointer_Opt.numericUpDown_pointer_centr_X.Value = img_pointer.cover_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_centr_Y.Value = img_pointer.cover_y;

            if (img_pointer.scale != null)
                uCtrl_Pointer_Opt.SetPointerImageBackground(img_pointer.scale);
            uCtrl_Pointer_Opt.numericUpDown_pointer_background_X.Value = img_pointer.scale_x;
            uCtrl_Pointer_Opt.numericUpDown_pointer_background_Y.Value = img_pointer.scale_y;

            uCtrl_Pointer_Opt.numericUpDown_pointer_startAngle.Value = img_pointer.start_angle;
            uCtrl_Pointer_Opt.numericUpDown_pointer_endAngle.Value = img_pointer.end_angle;


            uCtrl_Pointer_Opt._ElementWithPointer = img_pointer;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения набора картинок</summary>
        private void Read_ImgLevel_Options(hmUI_widget_IMG_LEVEL img_level, int imagesCount, bool imagesCountEnable)
        {
            PreviewView = false;

            uCtrl_Images_Opt.SettingsClear();

            uCtrl_Images_Opt.ImagesCount = imagesCount;
            uCtrl_Images_Opt.ImagesCountEnable = imagesCountEnable;

            uCtrl_Images_Opt.Visible = true;

            //userCtrl_Background_Options.SettingsClear();

            if (img_level == null)
            {
                PreviewView = true;
                return;
            }
            if (img_level.img_First != null)
                uCtrl_Images_Opt.SetImage(img_level.img_First);
            uCtrl_Images_Opt.numericUpDown_imageX.Value = img_level.X;
            uCtrl_Images_Opt.numericUpDown_imageY.Value = img_level.Y;

            uCtrl_Images_Opt._ElementWithImages = img_level;

            PreviewView = true;
        }

        /// <summary>Читаем настройки для отображения AM PM</summary>
        private void Read_AM_PM_Options(hmUI_widget_IMG_TIME_am_pm am_pm)
        {
            PreviewView = false;

            uCtrl_AmPm_Opt.SettingsClear();

            if (am_pm == null)
            {
                PreviewView = true;
                return;
            }
            if (am_pm.am_img != null)
                uCtrl_AmPm_Opt.Set_AM_Image(am_pm.am_img);
            uCtrl_AmPm_Opt.numericUpDown_AM_X.Value = am_pm.am_x;
            uCtrl_AmPm_Opt.numericUpDown_AM_Y.Value = am_pm.am_y;

            if (am_pm.pm_img != null)
                uCtrl_AmPm_Opt.Set_PM_Image(am_pm.pm_img);
            uCtrl_AmPm_Opt.numericUpDown_PM_X.Value = am_pm.pm_x;
            uCtrl_AmPm_Opt.numericUpDown_PM_Y.Value = am_pm.pm_y;

            uCtrl_AmPm_Opt._AmPm = am_pm;

            PreviewView = true;
        }

        /// <summary>Меняем настройки фона</summary>
        private void userCtrl_Background_Options_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            Background background = (Background)userCtrl_Background_Options._Background;
            string backgroundImg = userCtrl_Background_Options.GetBackground();
            string preview = userCtrl_Background_Options.GetPreview();
            if (preview.Length > 0)
            {
                if (Watch_Face.WatchFace_Info == null) Watch_Face.WatchFace_Info = new WatchFace_Info();
                Watch_Face.WatchFace_Info.Preview = preview;
            }

            if (userCtrl_Background_Options.radioButton_Background_image.Checked)
            {
                if (backgroundImg.Length > 0)
                {
                    if (background == null) 
                    {
                        if (radioButton_ScreenNormal.Checked)
                        {
                            if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                            if (Watch_Face.ScreenNormal.Background == null)
                                Watch_Face.ScreenNormal.Background = new Background();
                            background = Watch_Face.ScreenNormal.Background;
                        }
                        else
                        {
                            if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                            if (Watch_Face.ScreenAOD.Background == null)
                                Watch_Face.ScreenAOD.Background = new Background();
                            background = Watch_Face.ScreenAOD.Background;
                        }
                    }

                    //background = new Background();
                    if (background.BackgroundImage == null)
                        background.BackgroundImage = new hmUI_widget_IMG();
                    background.BackgroundImage.src = backgroundImg;
                    background.BackgroundImage.x = 0;
                    background.BackgroundImage.y = 0;
                    background.BackgroundImage.h = 454;
                    background.BackgroundImage.w = 454;
                    if (radioButton_GTR3_Pro.Checked)
                    {
                        background.BackgroundImage.h = 480;
                        background.BackgroundImage.w = 480;
                    }
                    //background.BackgroundImage.show_level = "ONLY_NORMAL";
                    background.BackgroundColor = null;
                }
            }
            else
            {
                if (background == null)
                {
                    if (radioButton_ScreenNormal.Checked)
                    {
                        if (Watch_Face.ScreenNormal == null) Watch_Face.ScreenNormal = new ScreenNormal();
                        if (Watch_Face.ScreenNormal.Background == null)
                            Watch_Face.ScreenNormal.Background = new Background();
                        background = Watch_Face.ScreenNormal.Background;
                    }
                    else
                    {
                        if (Watch_Face.ScreenAOD == null) Watch_Face.ScreenAOD = new ScreenAOD();
                        if (Watch_Face.ScreenAOD.Background == null)
                            Watch_Face.ScreenAOD.Background = new Background();
                        background = Watch_Face.ScreenAOD.Background;
                    }
                }

                if (background.BackgroundColor == null)
                    background.BackgroundColor = new hmUI_widget_FILL_RECT();
                background.BackgroundColor.color = ColorToString(userCtrl_Background_Options.GetColorBackground());
                background.BackgroundColor.x = 0;
                background.BackgroundColor.y = 0;
                background.BackgroundColor.h = 454;
                background.BackgroundColor.w = 454;
                if (radioButton_GTR3_Pro.Checked)
                {
                    background.BackgroundColor.h = 480;
                    background.BackgroundColor.w = 480;
                }
                background.BackgroundImage = null;
            }
            background.visible = userCtrl_Background_Options.Visible;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Text_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_NUMBER img_number = (hmUI_widget_IMG_NUMBER)uCtrl_Text_Opt._ElementWithText;
            if (img_number == null) return;

            img_number.align = uCtrl_Text_Opt.GetAlignment();
            img_number.follow = uCtrl_Text_Opt.checkBox_follow.Checked;
            img_number.icon = uCtrl_Text_Opt.GetIcon();
            img_number.iconPosX = (int)uCtrl_Text_Opt.numericUpDown_iconX.Value;
            img_number.iconPosY = (int)uCtrl_Text_Opt.numericUpDown_iconY.Value;
            img_number.img_First = uCtrl_Text_Opt.GetImage();
            img_number.imageX = (int)uCtrl_Text_Opt.numericUpDown_imageX.Value;
            img_number.imageY = (int)uCtrl_Text_Opt.numericUpDown_imageY.Value;
            img_number.space = (int)uCtrl_Text_Opt.numericUpDown_spacing.Value;
            img_number.unit = uCtrl_Text_Opt.GetUnit();
            img_number.zero = uCtrl_Text_Opt.checkBox_addZero.Checked;


            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_AmPm_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_TIME_am_pm am_pm = (hmUI_widget_IMG_TIME_am_pm)uCtrl_AmPm_Opt._AmPm;
            if (am_pm == null) return;

            am_pm.am_img = uCtrl_AmPm_Opt.Get_AM_Image();
            am_pm.am_x = (int)uCtrl_AmPm_Opt.numericUpDown_AM_X.Value;
            am_pm.am_y = (int)uCtrl_AmPm_Opt.numericUpDown_AM_Y.Value;

            am_pm.pm_img = uCtrl_AmPm_Opt.Get_PM_Image();
            am_pm.pm_x = (int)uCtrl_AmPm_Opt.numericUpDown_PM_X.Value;
            am_pm.pm_y = (int)uCtrl_AmPm_Opt.numericUpDown_PM_Y.Value;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Pointer_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_POINTER img_pointer = (hmUI_widget_IMG_POINTER)uCtrl_Pointer_Opt._ElementWithPointer;
            if (img_pointer == null) return;

            img_pointer.src = uCtrl_Pointer_Opt.GetPointerImage();
            img_pointer.center_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_X.Value;
            img_pointer.center_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_Y.Value;

            img_pointer.pos_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_offset_X.Value;
            img_pointer.pos_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_offset_Y.Value;

            img_pointer.start_angle = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_startAngle.Value;
            img_pointer.end_angle = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_endAngle.Value;

            img_pointer.cover_path = uCtrl_Pointer_Opt.GetPointerImageCentr();
            img_pointer.cover_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_centr_X.Value;
            img_pointer.cover_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_centr_Y.Value;

            img_pointer.scale = uCtrl_Pointer_Opt.GetPointerImageBackground();
            img_pointer.scale_x = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_background_X.Value;
            img_pointer.scale_y = (int)uCtrl_Pointer_Opt.numericUpDown_pointer_background_Y.Value;


            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private void uCtrl_Images_Opt_ValueChanged(object sender, EventArgs eventArgs)
        {
            if (!PreviewView) return;
            if (Watch_Face == null) return;
            hmUI_widget_IMG_LEVEL img_level = (hmUI_widget_IMG_LEVEL)uCtrl_Images_Opt._ElementWithImages;
            if (img_level == null) return;

            img_level.img_First = uCtrl_Images_Opt.GetImage();
            img_level.X = (int)uCtrl_Images_Opt.numericUpDown_imageX.Value;
            img_level.Y = (int)uCtrl_Images_Opt.numericUpDown_imageY.Value;
            img_level.image_length = (int)uCtrl_Images_Opt.numericUpDown_pictures_count.Value;

            JSON_Modified = true;
            PreviewImage();
            FormText();
        }

        private Color StringToColor(string color)
        {
            if (color.Length == 18) color = color.Remove(2, 8);
            Color old_color = ColorTranslator.FromHtml(color);
            Color new_color = Color.FromArgb(255, old_color.R, old_color.G, old_color.B);
            return new_color;
        }

        private string ColorToString(Color color)
        {
            Color new_color = Color.FromArgb(0, color.R, color.G, color.B);
            string colorStr = ColorTranslator.ToHtml(new_color);
            colorStr = colorStr.Replace("#", "0xFF");
            return colorStr;
        }

    }
}
