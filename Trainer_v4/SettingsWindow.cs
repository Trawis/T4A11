using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Utils = Trainer_v4.Utilities;

namespace Trainer_v4
{
    public class SettingsWindow : MonoBehaviour
    {
        private static string _title = "Trainer Settings, by Trawis " + Main.Version;

        public static GUIWindow Window;
        public static bool Shown = false;

        public static void Show()
        {
            if (Shown)
            {
                Window.Close();
                Shown = false;
                return;
            }

            Init();
            Shown = true;
        }

        public static void Close()
        {
            if (Shown)
            {
                Window.Close();
                Shown = false;
            }
        }

        private static void Init()
        {
            var settings = PropertyHelper.Settings;

            Window = WindowManager.SpawnWindow();
            Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = _title;
            Window.name = "TrainerSettings";
            Window.MainPanel.name = "TrainerSettingsPanel";

            if (Window.name == "TrainerSettings")
            {
                Window.GetComponentsInChildren<Button>()
                  .SingleOrDefault(x => x.name == "CloseButton")
                  .onClick.AddListener(() => Shown = false);
            }

            List<GameObject> column1 = new List<GameObject>();
            List<GameObject> column2 = new List<GameObject>();
            List<GameObject> column3 = new List<GameObject>();
            List<GameObject> column4 = new List<GameObject>();

            Utils.AddInputBox("Product Name Here", new Rect(Constants.FIRST_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                boxText => PropertyHelper.ProductPriceName = boxText, Window);

            Utils.AddButton("Add Money", new Rect(Constants.FIRST_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.IncreaseMoney, Window);

            Utils.AddButton("Add Reputation", new Rect(Constants.SECOND_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddRep, Window);

            Utils.AddButton("Set Product Price", new Rect(Constants.SECOND_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductPrice, Window);

            Utils.AddButton("Set Product Stock", new Rect(Constants.THIRD_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductStock, Window);

            Utils.AddButton("Set Active Users", new Rect(Constants.FOURTH_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddActiveUsers, Window);

            Utils.AddButton("Max Followers", new Rect(Constants.FIRST_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.MaxFollowers, Window);

            Utils.AddButton("Fix Bugs", new Rect(Constants.SECOND_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.FixBugs, Window);

            //Utils.AddButton("Max Code", new Rect(Constants.THIRD_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxCode, Window);

            //Utils.AddButton("Max Art", new Rect(Constants.FOURTH_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxArt, Window);

            Utils.AddButton("Takeover Company", new Rect(Constants.FIRST_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.TakeoverCompany, Window);

            Utils.AddButton("Subsidiary Company", new Rect(Constants.SECOND_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SubDCompany, Window);

            Utils.AddButton("Bankrupt", new Rect(Constants.THIRD_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.ForceBankrupt, Window);

            Utils.AddButton("AI Bankrupt All", TrainerBehaviour.AIBankrupt, ref column1);

            Utils.AddButton("Days per month", TrainerBehaviour.MonthDays, ref column1);

            Utils.AddButton("Clear all loans", TrainerBehaviour.ClearLoans, ref column1);

            Utils.AddButton("Max Skill of employees", TrainerBehaviour.EmployeesToMax, ref column1);

            Utils.AddButton("Remove Products", TrainerBehaviour.RemoveSoft, ref column1);

            Utils.AddButton("Reset age of employees", TrainerBehaviour.ResetAgeOfEmployees, ref column1);

            Utils.AddButton("Sell products stock", TrainerBehaviour.SellProductStock, ref column1);

            Utils.AddButton("Unlock all furniture", TrainerBehaviour.UnlockFurniture, ref column1);

            Utils.AddButton("Unlock all space", TrainerBehaviour.UnlockAllSpace, ref column1);

            //Utils.AddButton("Test", TrainerBehaviour.Test, ref column1);

            Utils.AddToggle("Disable Needs", PropertyHelper.GetProperty(settings, "NoNeeds"),
                a => PropertyHelper.SetProperty(settings, "NoNeeds", !PropertyHelper.GetProperty(settings, "NoNeeds")), ref column2);

            Utils.AddToggle("Disable Stress", PropertyHelper.GetProperty(settings, "NoStress"),
                a => PropertyHelper.SetProperty(settings, "NoStress", !PropertyHelper.GetProperty(settings, "NoStress")), ref column2);

            Utils.AddToggle("Free Employees", PropertyHelper.GetProperty(settings, "FreeEmployees"),
                a => PropertyHelper.SetProperty(settings, "FreeEmployees", !PropertyHelper.GetProperty(settings, "FreeEmployees")), ref column2);

            Utils.AddToggle("Free Staff", PropertyHelper.GetProperty(settings, "FreeStaff"),
                a => PropertyHelper.SetProperty(settings, "FreeStaff", !PropertyHelper.GetProperty(settings, "FreeStaff")), ref column2);

            Utils.AddToggle("Full Efficiency", PropertyHelper.GetProperty(settings, "FullEfficiency"),
                a => PropertyHelper.SetProperty(settings, "FullEfficiency", !PropertyHelper.GetProperty(settings, "FullEfficiency")), ref column2);

            Utils.AddToggle("Full Satisfaction", PropertyHelper.GetProperty(settings, "FullSatisfaction"),
                a => PropertyHelper.SetProperty(settings, "FullSatisfaction", !PropertyHelper.GetProperty(settings, "FullSatisfaction")), ref column2);

            Utils.AddToggle("Lock Age of Employees", PropertyHelper.GetProperty(settings, "LockAge"),
                a => PropertyHelper.SetProperty(settings, "LockAge", !PropertyHelper.GetProperty(settings, "LockAge")), ref column2);

            Utils.AddToggle("No Vacation", PropertyHelper.GetProperty(settings, "NoVacation"),
                a => PropertyHelper.SetProperty(settings, "NoVacation", !PropertyHelper.GetProperty(settings, "NoVacation")), ref column2);

            Utils.AddToggle("No Sickness", PropertyHelper.GetProperty(settings, "NoSickness"),
                a => PropertyHelper.SetProperty(settings, "NoSickness", !PropertyHelper.GetProperty(settings, "NoSickness")), ref column2);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", PropertyHelper.GetProperty(settings, "UltraEfficiency"),
                a => PropertyHelper.SetProperty(settings, "UltraEfficiency", !PropertyHelper.GetProperty(settings, "UltraEfficiency")), ref column2);

            Utils.AddToggle("Full Environment", PropertyHelper.GetProperty(settings, "FullEnvironment"),
                a => PropertyHelper.SetProperty(settings, "FullEnvironment", !PropertyHelper.GetProperty(settings, "FullEnvironment")), ref column3);

            Utils.AddToggle("Full Sun Light", PropertyHelper.GetProperty(settings, "FullRoomBrightness"),
                a => PropertyHelper.SetProperty(settings, "FullRoomBrightness", !PropertyHelper.GetProperty(settings, "FullRoomBrightness")), ref column3);

            Utils.AddToggle("Lock Temperature To 21", PropertyHelper.GetProperty(settings, "TemperatureLock"),
                a => PropertyHelper.SetProperty(settings, "TemperatureLock", !PropertyHelper.GetProperty(settings, "TemperatureLock")), ref column3);

            Utils.AddToggle("No Maintenance", PropertyHelper.GetProperty(settings, "NoMaintenance"),
                a => PropertyHelper.SetProperty(settings, "NoMaintenance", !PropertyHelper.GetProperty(settings, "NoMaintenance")), ref column3);

            Utils.AddToggle("Noise Reduction", PropertyHelper.GetProperty(settings, "NoiseReduction"),
                a => PropertyHelper.SetProperty(settings, "NoiseReduction", !PropertyHelper.GetProperty(settings, "NoiseReduction")), ref column3);

            Utils.AddToggle("Rooms Never Dirty", PropertyHelper.GetProperty(settings, "CleanRooms"),
                a => PropertyHelper.SetProperty(settings, "CleanRooms", !PropertyHelper.GetProperty(settings, "CleanRooms")), ref column3);

            Utils.AddToggle("No Education Cost", PropertyHelper.GetProperty(settings, "NoEducationCost"),
                a => PropertyHelper.SetProperty(settings, "NoEducationCost", !PropertyHelper.GetProperty(settings, "NoEducationCost")), ref column3);

            //Utils.AddToggle("Disable Burglars", PropertyHelper.GetProperty(settings, "DisableBurglars"),
            //    a => PropertyHelper.SetProperty(settings, "DisableBurglars", !PropertyHelper.GetProperty(settings, "DisableBurglars")), ref column3);

            //Utils.AddToggle("Disable Fires", PropertyHelper.GetProperty(settings, "DisableFires"),
            //    a => PropertyHelper.SetProperty(settings, "DisableFires", !PropertyHelper.GetProperty(settings, "DisableFires")), ref column3);

            Utils.AddToggle("Auto Distribution Deals", PropertyHelper.GetProperty(settings, "AutoDistributionDeals"),
                a => PropertyHelper.SetProperty(settings, "AutoDistributionDeals", !PropertyHelper.GetProperty(settings, "AutoDistributionDeals")), ref column4);

            Utils.AddToggle("Free Print", PropertyHelper.GetProperty(settings, "FreePrint"),
                a => PropertyHelper.SetProperty(settings, "FreePrint", !PropertyHelper.GetProperty(settings, "FreePrint")), ref column4);

            Utils.AddToggle("Free Water & Electricity", PropertyHelper.GetProperty(settings, "NoWaterElectricity"),
                a => PropertyHelper.SetProperty(settings, "NoWaterElectricity", !PropertyHelper.GetProperty(settings, "NoWaterElectricity")), ref column4);

            Utils.AddToggle("Increase Bookshelf Skill", PropertyHelper.GetProperty(settings, "IncreaseBookshelfSkill"),
                a => PropertyHelper.SetProperty(settings, "IncreaseBookshelfSkill", !PropertyHelper.GetProperty(settings, "IncreaseBookshelfSkill")), ref column4);

            Utils.AddToggle("Increase Courier Capacity", PropertyHelper.GetProperty(settings, "IncreaseCourierCapacity"),
                a => PropertyHelper.SetProperty(settings, "IncreaseCourierCapacity", !PropertyHelper.GetProperty(settings, "IncreaseCourierCapacity")), ref column4);

            Utils.AddToggle("Increase Print Speed", PropertyHelper.GetProperty(settings, "IncreasePrintSpeed"),
                a => PropertyHelper.SetProperty(settings, "IncreasePrintSpeed", !PropertyHelper.GetProperty(settings, "IncreasePrintSpeed")), ref column4);

            Utils.AddToggle("More Hosting Deals", PropertyHelper.GetProperty(settings, "MoreHostingDeals"),
                a => PropertyHelper.SetProperty(settings, "MoreHostingDeals", !PropertyHelper.GetProperty(settings, "MoreHostingDeals")), ref column4);

            Utils.AddToggle("Reduce Internet Cost", PropertyHelper.GetProperty(settings, "ReduceISPCost"),
                a => PropertyHelper.SetProperty(settings, "ReduceISPCost", !PropertyHelper.GetProperty(settings, "ReduceISPCost")), ref column4);

            //Utils.AddToggle("Disable Skill Decay", PropertyHelper.GetProperty(settings, "DisableSkillDecay"),
            //    a => PropertyHelper.SetProperty(settings, "DisableSkillDecay", !PropertyHelper.GetProperty(settings, "DisableSkillDecay")), ref column4);

            Utils.AddToggle("No Server Cost", PropertyHelper.GetProperty(settings, "NoServerCost"),
                a => PropertyHelper.SetProperty(settings, "NoServerCost", !PropertyHelper.GetProperty(settings, "NoServerCost")), ref column4);

            Utils.AddToggle("Reduce Expansion Cost", PropertyHelper.GetProperty(settings, "ReduceExpansionCost"),
                a => PropertyHelper.SetProperty(settings, "ReduceExpansionCost", !PropertyHelper.GetProperty(settings, "ReduceExpansionCost")), ref column4);

            Utils.CreateGameObjects(Constants.FIRST_COLUMN, Constants.SETTINGS_WINDOW_SKIP_ROWS, column1.ToArray(), Window);
            Utils.CreateGameObjects(Constants.SECOND_COLUMN, Constants.SETTINGS_WINDOW_SKIP_ROWS, column2.ToArray(), Window);
            Utils.CreateGameObjects(Constants.THIRD_COLUMN, Constants.SETTINGS_WINDOW_SKIP_ROWS, column3.ToArray(), Window);
            Utils.CreateGameObjects(Constants.FOURTH_COLUMN, Constants.SETTINGS_WINDOW_SKIP_ROWS, column4.ToArray(), Window);

            int[] columnsCount = new int[]
            {
                column1.Count(), column2.Count(), column3.Count(), column4.Count()
            };

            Utils.SetWindowSize(columnsCount, Constants.X_SETTINGS_WINDOW, Constants.Y_SETTINGS_WINDOW_OFFSET, Window);
        }
    }
}