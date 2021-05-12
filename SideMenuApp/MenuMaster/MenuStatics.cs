using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

using SD = System.Drawing;

namespace SideMenuApp.MasterMenu
{
    public class MenuStatics
    {
        // MENU
        public static Image MenuHeaderIcon { get; set; } = new Image() { Source = "xamarinLogoOriginal", WidthRequest = 100, HeightRequest = 100 };
        public static Label MenuHeaderLabel1 { get; set; } = new Label() { Text = "AppName", FontAttributes = FontAttributes.Bold, FontSize = 18 };
        public static Label MenuHeaderLabel2 { get; set; } = new Label() { Text = "Status", FontAttributes = FontAttributes.Italic, FontSize = 16 };
        public static int IconWidth { get; set; } = 35;
        public static int IconHeight { get; set; } = 35;
        public static int[] IconMargin { get; set; } = { 20, 0, 0, 0 };
        public static int MenuTitleFontSize { get; set; } = 18;
        public static SD.Color MenuColor = SD.Color.FromArgb(115, 115, 115);
        public static SD.Color MenuColorSelected = SD.Color.FromArgb(3, 169, 244);

        // SUBMENU
        public static int SubMenuTitleFontSize { get; set; } = 18;
        public static SD.Color SubMenuColor = SD.Color.FromArgb(115, 115, 115);
        public static SD.Color SubMenuColorSelected = SD.Color.FromArgb(3, 169, 244);

        // MENU LAST SELECTEDS
        public static StackLayout MainMenu { get; set; }
        public static MenuModel LastSelectedMenu { get; set; }
        public static Label LastSelectedSubMenu { get; set; }
    }
}
