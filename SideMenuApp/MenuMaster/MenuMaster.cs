using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Forms;
using DF = System.Drawing;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace SideMenuApp.MasterMenu
{
    public class MenuMaster
    {
        static MasterDetailPage RootPage { get => Application.Current.MainPage as MasterDetailPage; }

        public static ScrollView CreateMenu(MenuModel[] menu)
        {
            // LAYOUT
            ScrollView sv = new ScrollView();
            StackLayout mainMenu = new StackLayout() { Spacing = 0 };
            MenuStatics.MainMenu = mainMenu;

            #region HEADER
            // HEADER LAYOUT
            Grid grid = new Grid
            {
                RowDefinitions =
            {
                new RowDefinition { Height = new GridLength(135) }
            },
                ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(160) },
                new ColumnDefinition()
            }
            };

            // HEADER BACKGROUND
            Image background = new Image() { Source = "menuBackground", Aspect = Aspect.Fill };

            // ADD BACKGROUND TO LAYOUT
            Grid.SetRow(background, 0);
            Grid.SetColumnSpan(background, 2);

            grid.Children.Add(background);

            // ADD LOGO TO LAYOUT
            Grid.SetRow(MenuStatics.MenuHeaderIcon, 0);
            Grid.SetColumn(MenuStatics.MenuHeaderIcon, 0);

            grid.Children.Add(MenuStatics.MenuHeaderIcon);

            // ADD TAP GESTURE RECOGNIZER TO LOGO
            TapGestureRecognizer logoTap = new TapGestureRecognizer();
            logoTap.Tapped += FlipLogo;
            MenuStatics.MenuHeaderIcon.GestureRecognizers.Add(logoTap);

            // ADD LABEL1 AND LABEL2 TO LAYOUT ALSO CREATING A STACKLAYOUT
            StackLayout headerLabelsStackLayout = new StackLayout();
            headerLabelsStackLayout.VerticalOptions = LayoutOptions.Center;
            headerLabelsStackLayout.Spacing = 0;

            Grid.SetRow(headerLabelsStackLayout, 0);
            Grid.SetColumn(headerLabelsStackLayout, 1);

            headerLabelsStackLayout.Children.Add(MenuStatics.MenuHeaderLabel1);
            headerLabelsStackLayout.Children.Add(MenuStatics.MenuHeaderLabel2);

            grid.Children.Add(headerLabelsStackLayout);

            // ADD HEADER TO MAIN MENU LAYOUT
            mainMenu.Children.Add(grid);

            // ADD A LINE SEPARATOR TO THE END OF HEADER LAYOUT
            mainMenu.Children.Add(new BoxView() { HeightRequest = 0.5, Color = Color.LightGray, Margin = new Thickness(10, 5, 10, 5) });
            #endregion HEADER

            #region MAIN_MENU

            // INSTANTIATE THE FIRST MENU AS THE SELECTED MENU
            menu[0].Icon.Source = FlipIcon(menu[0].Icon.Source.ToString());
            menu[0].Title.TextColor = FlipColor(menu[0].Title.TextColor);
            menu[0].IsSelected = true;

            // ADD TAP GESTURE RECOGNIZER
            TapGestureRecognizer menuTap = new TapGestureRecognizer();
            menuTap.Tapped += MenuHandle;
            menu[0].MenuHeader.GestureRecognizers.Add(menuTap);

            // VERIFY SUBMENUS
            if (menu[0].IsSubMenu)
            {
                // ADD THE ARROW
                menu[0].MenuHeader.Children.Add(new Image() { Source = "menuIcon_arrowUp", WidthRequest = 15, HeightRequest = 15, Margin = new Thickness(5, 0, 0, 0) });

                // ADD SUBMENU TAP GESTURE RECOGNIZER
                TapGestureRecognizer menuTap4 = new TapGestureRecognizer();
                menuTap4.Tapped += SubMenuHandle;
                for (int j = 0; j < menu[0].SubMenu.Children.Count; j++)
                {
                    menu[0].SubMenus[j].GestureRecognizers.Add(menuTap4);
                }
                menu[0].SubMenuTitles[0].TextColor = FlipColor(menu[0].SubMenuTitles[0].TextColor);
                MenuStatics.LastSelectedSubMenu = menu[0].SubMenuTitles[0];
                Views.MainPageMaster.instance.HideMenuLabels(menu[0].SubMenuTitles);
            }
            // SET THE LAST SELECTED MENU
            MenuStatics.LastSelectedMenu = menu[0];
            // ADD THE MENU TO LAYOUT
            mainMenu.Children.Add(menu[0]);

            // INSTANTIATE THE MENUS
            for (int i = 1; i < menu.Length; i++)
            {
                if (menu[i].IsSubMenu)
                {
                    // ADD THE ARROW
                    menu[i].MenuHeader.Children.Add(new Image() { Source = "menuIcon_arrowDown", WidthRequest = 15, HeightRequest = 15, Margin = new Thickness(5, 0, 0, 0) });

                    // ADD TAP GESTURE RECOGNIZER
                    TapGestureRecognizer menuTap3 = new TapGestureRecognizer();
                    menuTap3.Tapped += MenuHandle;
                    menu[i].MenuHeader.GestureRecognizers.Add(menuTap3);
                    menu[i].SubMenu.IsVisible = false;
                    mainMenu.Children.Add(menu[i]);

                    // ADD SUBMENU TAP GESTURE RECOGNIZER
                    TapGestureRecognizer menuTap5 = new TapGestureRecognizer();
                    menuTap5.Tapped += SubMenuHandle;
                    for (int j = 0; j < menu[i].SubMenu.Children.Count; j++)
                    {
                        menu[i].SubMenus[j].GestureRecognizers.Add(menuTap5);
                    }
                    Views.MainPageMaster.instance.HideMenuLabels(menu[i].SubMenuTitles);
                }
                else
                {
                    // ADD TAP GESTURE RECOGNIZER
                    TapGestureRecognizer menuTap2 = new TapGestureRecognizer();
                    menuTap2.Tapped += MenuHandle;
                    menu[i].MenuHeader.GestureRecognizers.Add(menuTap2);
                    mainMenu.Children.Add(menu[i]);
                }
            }

            // RETURN THE MAIN MENU
            sv.Content = mainMenu;
            return sv;
            #endregion MAIN_MENU
        }

        public static void FlipLogo(object sender, EventArgs e)
        {
            Image img = (Image)sender;
            if (img.Source.ToString() == "File: menuUserIcon")
            {
                img.Source = "xamarinLogoOriginal";
            }
            else
            {
                img.Source = "menuUserIcon";
            }
            
        }

        public static void FlipArrow(MenuModel menu)
        {
            Image arrow = (Image)menu.MenuHeader.Children[2];
            if (arrow.Source.ToString() == "File: menuIcon_arrowDown")
            {
                arrow.Source = "menuIcon_arrowUp";
            }
            else
            {
                arrow.Source = "menuIcon_arrowDown";
            }
        }

        public static string FlipIcon(string icon)
        {
            if (icon.Contains("Selected"))
            {
                icon = icon.Replace("File:", "");
                icon = icon.Replace("Selected", "");
                icon = icon.Trim();
            }
            else
            {
                icon = icon.Replace("File:", "");
                icon = icon.Insert(icon.Length, "Selected");
                icon = icon.Trim();
            }
            return icon;
        }

        public static DF.Color FlipColor(DF.Color color)
        {
            if (color == MenuStatics.MenuColorSelected)
            {
                return MenuStatics.MenuColor;
            }
            else
            {
                return MenuStatics.MenuColorSelected;
            }
        }

        public static void MenuHandle(object sender, EventArgs e)
        {
            StackLayout menu = (StackLayout)sender;
            MenuModel parent = (MenuModel)menu.Parent;

            // FLIP THE SUBMENU VISIBILITY
            if (parent.IsSubMenu)
            {
                StackLayout subMenu = (StackLayout)parent.Children[1];

                // ANIMATION
                if (subMenu.IsVisible)
                {
                    if (parent.IsAnimationRunning) { }
                    else
                    {
                        Views.MainPageMaster.instance.HideMenuLabels(parent.SubMenuTitles);
                        subMenu.IsVisible = !subMenu.IsVisible;
                    }
                }
                else
                {
                    if (parent.IsAnimationRunning) { }
                    else
                    {
                        Task.Run(() => Views.MainPageMaster.instance.AnimateMenuLabels(parent));
                        parent.IsAnimationRunning = true;
                        subMenu.IsVisible = !subMenu.IsVisible;
                    }
                }

                FlipArrow(parent);
                return;
            }

            if (parent.IsSelected)
            {
                return;
            }

            // RESET THE LAST SELECTED MENUS
            MenuStatics.LastSelectedMenu.Icon.Source = FlipIcon(MenuStatics.LastSelectedMenu.Icon.Source.ToString());
            MenuStatics.LastSelectedMenu.Title.TextColor = FlipColor(MenuStatics.LastSelectedMenu.Title.TextColor);
            MenuStatics.LastSelectedMenu.IsSelected = false;
            if (MenuStatics.LastSelectedMenu.IsSubMenu)
            {
                MenuStatics.LastSelectedSubMenu.TextColor = FlipColor(MenuStatics.LastSelectedSubMenu.TextColor);
                StackLayout subMenu = (StackLayout)MenuStatics.LastSelectedMenu.Children[1];
                if (subMenu.IsVisible)
                {
                    FlipArrow(MenuStatics.LastSelectedMenu);
                }
                MenuStatics.LastSelectedMenu.SubMenu.IsVisible = false;
                Views.MainPageMaster.instance.HideMenuLabels(MenuStatics.LastSelectedMenu.SubMenuTitles);
            }

            // FLIP SELECTED

            parent.Icon.Source = FlipIcon(parent.Icon.Source.ToString());
            parent.Title.TextColor = FlipColor(parent.Title.TextColor);
            parent.IsSelected = true;
            

            // SET THE NEW LAST SELECTED
            MenuStatics.LastSelectedMenu = parent;
            MenuStatics.LastSelectedSubMenu = null;

            // CHANGE PAGE
            ChangePage(parent.PageName);

        }

        public static void SubMenuHandle(object sender, EventArgs e)
        {
            StackLayout subMenu = (StackLayout)sender;
            StackLayout parent = (StackLayout)subMenu.Parent;
            MenuModel menu = (MenuModel)parent.Parent;

            Label subTitle = (Label)subMenu.Children[0];

            if (MenuStatics.LastSelectedSubMenu == subTitle)
            {
                return;
            }

            // RESET THE LAST SELECTED MENUS
            MenuStatics.LastSelectedMenu.Icon.Source = FlipIcon(MenuStatics.LastSelectedMenu.Icon.Source.ToString());
            MenuStatics.LastSelectedMenu.Title.TextColor = FlipColor(MenuStatics.LastSelectedMenu.Title.TextColor);
            MenuStatics.LastSelectedMenu.IsSelected = false;
            if (MenuStatics.LastSelectedMenu.SubMenu != null)
            {
                MenuStatics.LastSelectedSubMenu.TextColor = FlipColor(MenuStatics.LastSelectedSubMenu.TextColor);
                MenuStatics.LastSelectedMenu.SubMenu.IsVisible = false;
                if (MenuStatics.LastSelectedMenu != menu)
                {
                    Views.MainPageMaster.instance.HideMenuLabels(MenuStatics.LastSelectedMenu.SubMenuTitles);
                    Image arrow = (Image)MenuStatics.LastSelectedMenu.MenuHeader.Children[2];
                    if (subMenu.IsVisible && arrow.Source.ToString() == "File: menuIcon_arrowUp")
                    {
                        FlipArrow(MenuStatics.LastSelectedMenu);
                    }
                }
            }

            // FLIP SELECTED
            menu.Icon.Source = FlipIcon(menu.Icon.Source.ToString());
            menu.Title.TextColor = FlipColor(menu.Title.TextColor);
            menu.IsSelected = true;
            menu.SubMenu = parent;
            subTitle.TextColor = FlipColor(subTitle.TextColor);
            menu.SubMenu.IsVisible = true;

            // SET THE NEW LAST SELECTED
            MenuStatics.LastSelectedMenu = menu;
            MenuStatics.LastSelectedSubMenu = subTitle;

            // CHANGE PAGE
            Console.WriteLine(GetSubMenuIndex(subTitle));
            ChangePage(menu.SubPageNames[GetSubMenuIndex(subTitle)]);
        }

        public static int GetSubMenuIndex(Label subMenuLabel)
        {
            StackLayout subMenu = (StackLayout)subMenuLabel.Parent;
            StackLayout subMenuHeader = (StackLayout)subMenu.Parent;

            return subMenuHeader.Children.IndexOf(subMenu);
        }

        public static void ChangePage(string pageName)
        {
            // FIND PAGE
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().First(t => t.Name == pageName);

            RootPage.Detail = new NavigationPage(((Page)Activator.CreateInstance(type)));
            RootPage.IsPresented = false;
        }

    }
}
