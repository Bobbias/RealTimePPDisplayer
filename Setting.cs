﻿using Sync.Tools;
using Sync.Tools.ConfigGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace RealTimePPDisplayer
{
    #region Converter

    internal static class ColorConverter
    {
        public static Color StringToColor(string color_str)
        {
            var color = new Color();
            if (color_str[0] == '#')
            {
                color.R = Convert.ToByte(color_str.Substring(1, 2), 16);
                color.G = Convert.ToByte(color_str.Substring(3, 2), 16);
                color.B = Convert.ToByte(color_str.Substring(5, 2), 16);
                color.A = Convert.ToByte(color_str.Substring(7, 2), 16);
            }
            else
            {
                color.A = Convert.ToByte(color_str.Substring(0, 2), 16);
                color.R = Convert.ToByte(color_str.Substring(2, 2), 16);
                color.G = Convert.ToByte(color_str.Substring(4, 2), 16);
                color.B = Convert.ToByte(color_str.Substring(6, 2), 16);
            }
            return color;
        }

        public static string ColorToString(Color c)
        {
            return $"#{c.R:X2}{c.G:X2}{c.B:X2}{c.A:X2}";
        }
    }

    #endregion Converter

    internal class SettingIni : IConfigurable
    {
        [ConfigPath(IsFilePath = true,NeedRestart = true)]
        public ConfigurationElement TextOutputPath
        {
            get => Setting.TextOutputPath;
            set => Setting.TextOutputPath = value;
        }

        [ConfigBool]
        public ConfigurationElement DisplayHitObject
        {
            get=>Setting.DisplayHitObject.ToString();
            set
            {
                Setting.DisplayHitObject = bool.Parse(value);
                Setting.SettingChanged();
            }
        }

        [ConfigFont]
        public ConfigurationElement FontName
        {
            get=>Setting.FontName;
            set
            {
                Setting.FontName = value;
                Setting.SettingChanged();
            }
        }

        [ConfigInteger(MinValue = 10,MaxValue = 150)]
        public ConfigurationElement PPFontSize
        {
            set
            {
                Setting.PPFontSize = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.PPFontSize.ToString();
        }

        [ConfigColor]
        public ConfigurationElement PPFontColor
        {
            set
            {
                Setting.PPFontColor=ColorConverter.StringToColor(value);
                Setting.SettingChanged();
            }
            get=>ColorConverter.ColorToString(Setting.PPFontColor);
        }

        [ConfigInteger(MinValue = 10, MaxValue = 150)]
        public ConfigurationElement HitCountFontSize
        {
            set
            {
                Setting.HitCountFontSize = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.HitCountFontSize.ToString();
        }

        [ConfigColor]
        public ConfigurationElement HitCountFontColor
        {
            set
            {
                Setting.HitCountFontColor = ColorConverter.StringToColor(value);
                Setting.SettingChanged();
            }
            get => ColorConverter.ColorToString(Setting.HitCountFontColor);
        }

        [ConfigColor]
        public ConfigurationElement BackgroundColor
        {
            set
            {
                Setting.BackgroundColor = ColorConverter.StringToColor(value);
                Setting.SettingChanged();
            }
            get => ColorConverter.ColorToString(Setting.BackgroundColor);
        }

        [ConfigInteger(MinValue =100,MaxValue =1080)]
        public ConfigurationElement WindowHeight
        {
            set
            {
                Setting.WindowHeight = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.WindowHeight.ToString();
        }

        [ConfigInteger(MinValue = 100, MaxValue = 1920)]
        public ConfigurationElement WindowWidth
        {
            set
            {
                Setting.WindowWidth = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.WindowWidth.ToString();
        }

        [ConfigInteger(MinValue = 30, MaxValue = 10000)]
        public ConfigurationElement SmoothTime
        {
            set
            {
                Setting.SmoothTime = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.SmoothTime.ToString();
        }

        [ConfigInteger(MinValue = 1, MaxValue = 60)]
        public ConfigurationElement FPS
        {
            set
            {
                Setting.FPS = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.FPS.ToString();
        }

        [ConfigBool]
        public ConfigurationElement Topmost
        {
            set
            {
                Setting.Topmost = bool.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.Topmost.ToString();
        }

        [ConfigBool]
        public ConfigurationElement WindowTextShadow
        {
            set
            {
                Setting.WindowTextShadow = bool.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.WindowTextShadow.ToString();
        }

        [ConfigReflectList(Type =typeof(RealTimePPDisplayerPlugin),ValueListName = "DisplayerCreatorNames", 
            SplitSeparator = ',', AllowMultiSelect = true)]
        public ConfigurationElement OutputMethods
        {
            get => string.Join(",", Setting.OutputMethods);
            set
            {
                Setting.OutputMethods = value.ToString().Split(',').Select(s=>s.Trim());
                Setting.SettingChanged();
            }
        }

        [ConfigBool(NeedRestart = true)]
        public ConfigurationElement DebugMode
        {
            set
            {
                Setting.DebugMode = bool.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.DebugMode.ToString();
        }

        [ConfigString]
        public ConfigurationElement PPFormat
        {
            get => Setting.PPFormat;
            set => Setting.PPFormat = value;
        }

        [ConfigString]
        public ConfigurationElement HitCountFormat
        {
            get => Setting.HitCountFormat;
            set => Setting.HitCountFormat = value;
        }

        [ConfigInteger(MinValue = 0,MaxValue = 15)]
        public ConfigurationElement RoundDigits
        {
            set
            {
                Setting.RoundDigits = int.Parse(value);
                Setting.SettingChanged();
            }
            get => Setting.RoundDigits.ToString();
        }

        public void onConfigurationLoad()
        {
        }

        public void onConfigurationReload()
        {
            Setting.SettingChanged();
        }

        public void onConfigurationSave()
        {
        }
    }

    internal static class Setting
    {
        public static IEnumerable<string> OutputMethods = new[]{ "wpf" };
        public static string TextOutputPath = @"rtpp{0}.txt";
        public static bool DisplayHitObject = true;
        public static string FontName = "Segoe UI";
        public static int PPFontSize = 48;
        public static Color PPFontColor = Colors.White;
        public static int HitCountFontSize = 24;
        public static Color HitCountFontColor = Colors.White;
        public static Color BackgroundColor = ColorConverter.StringToColor("#00FF00FF");
        public static int WindowWidth = 280;
        public static int WindowHeight = 172;
        public static int SmoothTime = 200;
        public static int FPS = 60;
        public static bool Topmost = true;
        public static bool WindowTextShadow = true;
        public static bool DebugMode = false;
        public static int RoundDigits = 2;
        //rtpp rtpp_aim rtpp_speed rtpp_acc fcpp fcpp_aim fcpp_speed fcpp_acc maxpp maxpp_aim maxpp_speed maxpp_acc
        public static string PPFormat = "${rtpp}pp";
        //combo maxcombo fullcombo n300 n100 n50 nmiss
        public static string HitCountFormat = "${n100}x100 ${n50}x50 ${nmiss}xMiss";


        public static event Action OnSettingChanged;

        public static void SettingChanged()
        {
            OnSettingChanged?.Invoke();
        }
    }
}