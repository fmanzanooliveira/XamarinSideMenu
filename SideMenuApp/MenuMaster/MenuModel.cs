using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;

namespace SideMenuApp.MasterMenu
{
    public class MenuModel : StackLayout
    {
        public bool IsSelected { get; set; } = false;
        public bool IsSubMenu { get; set; } = false;
        public bool IsAnimationRunning { get; set; } = false;
        public Image Icon { get; set; }
        public Label Title { get; set; }
        public StackLayout MenuHeader { get; set; }
        public StackLayout SubMenu { get; set; }
        public StackLayout[] SubMenus { get; set; }
        public Label[] SubMenuTitles { get; set; }
        public string PageName { get; set; }
        public string[] SubPageNames { get; set; }

        /// <summary>
        /// Create a menu without submenus.  
        /// </summary>
        /// <param name="icon">Set the icon for the menu. Format: menuIcon_"name" (Ex: menuIcon_home)</param>
        /// <param name="title">Set the title for the menu.</param>
        /// <param name="pageName">Set the page redirection by simply entering the name of the page.</param>
        public MenuModel(string icon, string title, string pageName)
        {
            this.Spacing = 0;

            StackLayout menuHeader = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 0 };

            // ICON
            Image iconImage = new Image() { Source = icon, WidthRequest = MenuStatics.IconWidth, HeightRequest = MenuStatics.IconHeight, Margin = new Thickness(20,0,0,0) };
            Icon = iconImage;
            menuHeader.Children.Add(iconImage);
            // TITLE
            Label titleLabel = new Label() { Text = title, FontAttributes = FontAttributes.Bold, FontSize = MenuStatics.MenuTitleFontSize, VerticalOptions = LayoutOptions.Center };
            Title = titleLabel;
            menuHeader.Children.Add(titleLabel);
            MenuHeader = menuHeader;

            this.PageName = pageName;

            this.Children.Add(menuHeader);
        }

        /// <summary>
        /// Create a menu with submenus.  
        /// </summary>
        /// <param name="icon">Set the icon for the menu. Format: menuIcon_"name" (Ex: menuIcon_home)</param>
        /// <param name="title">Set the title for the menu.</param>
        /// <param name="subMenuTitles">Set the titles for the submenu.</param>
        /// <param name="subPageNames">Set the pages redirections.</param>
        public MenuModel(string icon, string title, string[] subMenuTitles, string[] subPageNames)
        {
            this.Spacing = 0;

            StackLayout menuHeader = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 0 };

            // ICON
            Image iconImage = new Image() { Source = icon, WidthRequest = MenuStatics.IconWidth, HeightRequest = MenuStatics.IconHeight, Margin = new Thickness(20, 0, 0, 0) };
            Icon = iconImage;
            menuHeader.Children.Add(iconImage);
            // TITLE
            Label titleLabel = new Label() { Text = title, FontAttributes = FontAttributes.Bold, FontSize = MenuStatics.MenuTitleFontSize, VerticalOptions = LayoutOptions.Center };
            Title = titleLabel;
            menuHeader.Children.Add(titleLabel);
            MenuHeader = menuHeader;

            this.Children.Add(menuHeader);

            // SUB MENU
            this.IsSubMenu = true;
            StackLayout subMenu = new StackLayout() { Spacing = 0 };
            SubMenu = subMenu;
            SubMenus = new StackLayout[subMenuTitles.Length];
            SubMenuTitles = new Label[subMenuTitles.Length];
            SubPageNames = new string[subMenuTitles.Length];

            // TITLE
            for (int i = 0; i < subMenuTitles.Length; i++)
            {
                StackLayout subMenuLabel = new StackLayout();
                Label subTitleLabel = new Label() { Text = subMenuTitles[i], FontSize = MenuStatics.SubMenuTitleFontSize, VerticalOptions = LayoutOptions.Center, Margin = new Thickness(55, 5, 0, 5) };
                SubMenus[i] = subMenuLabel;
                SubMenuTitles[i] = subTitleLabel;
                SubPageNames[i] = subPageNames[i];
                subMenuLabel.Children.Add(subTitleLabel);
                subMenu.Children.Add(subMenuLabel);
            }

            this.Children.Add(subMenu);
        }
    }
}
