using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using System.Threading;
using SideMenuApp.MasterMenu;

namespace SideMenuApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPageMaster : ContentPage
    {
        public static MainPageMaster instance;
        public MainPageMaster()
        {
            InitializeComponent();

            instance = this;

            MenuModel[] menu = new MenuModel[] { new MenuModel("menuIcon_home", "Home", "HomeView"),
                                                 new MenuModel("menuIcon_account", "Account", "AccountView"),
                                                 new MenuModel("menuIcon_company", "Company", new string[] { "History", "Products", "Staff" }, new string[] { "HistoryView", "ProductsView", "StaffView" }),
                                                 new MenuModel("menuIcon_finances", "Finances", "FinancesView"),
                                                 new MenuModel("menuIcon_music", "Music", "MusicView"),
                                                 new MenuModel("menuIcon_settings", "Settings", new string[] { "Language", "Theme" }, new string[] { "LanguageView", "ThemeView" }),
                                                 new MenuModel("menuIcon_shopping", "Shopping", "ShoppingView"),
                                                 new MenuModel("menuIcon_about", "About", "AboutView"),
            };

            Content = MenuMaster.CreateMenu(menu);
        }

        // ANIMATIONS
        public void HideMenuLabels(Label[] labels)
        {
            foreach (var label in labels)
            {
                label.FadeTo(0, 0);
                label.TranslateTo(-50, 0, 0);
            }
        }
        public void AnimateMenuLabels(MenuModel menu)
        {
            int delay = 100;
            foreach (var label in menu.SubMenuTitles)
            {
                label.TranslateTo(0, 0, 500, Easing.SpringOut);
                label.FadeTo(100, 4000, Easing.SinIn);
                Thread.Sleep(delay);
            }
            if (menu.IsAnimationRunning)
                menu.IsAnimationRunning = false;
        }
    }
}