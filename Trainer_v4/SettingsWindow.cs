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
                Shown = false;
            }
        }

        private static void Init()
        {
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
                boxText => PropertyHelper.Product_PriceName = boxText);


            Utils.AddButton("Add Money", new Rect(Constants.FIRST_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.IncreaseMoney);

            Utils.AddButton("Add Reputation", new Rect(Constants.SECOND_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddRep);

            Utils.AddButton("Set Product Price", new Rect(Constants.SECOND_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductPrice);

            Utils.AddButton("Set Product Stock", new Rect(Constants.THIRD_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductStock);

            Utils.AddButton("Set Active Users", new Rect(Constants.FOURTH_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddActiveUsers);

            Utils.AddButton("Max Followers", new Rect(Constants.FIRST_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.MaxFollowers);

            Utils.AddButton("Fix Bugs", new Rect(Constants.SECOND_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.FixBugs);

            //Utils.AddButton("Max Code", new Rect(Constants.THIRD_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxCode);

            //Utils.AddButton("Max Art", new Rect(Constants.FOURTH_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxArt);

            Utils.AddButton("Takeover Company", new Rect(Constants.FIRST_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.TakeoverCompany);

            Utils.AddButton("Subsidiary Company", new Rect(Constants.SECOND_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SubDCompany);

            Utils.AddButton("Bankrupt", new Rect(Constants.THIRD_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.ForceBankrupt);

            Utils.AddButton("AI Bankrupt All", TrainerBehaviour.AIBankrupt, ref column1);

            Utils.AddButton("Days per month", TrainerBehaviour.MonthDays, ref column1);

            Utils.AddButton("Clear all loans", TrainerBehaviour.ClearLoans, ref column1);

            //Utils.AddButton("HR Leaders", TrainerBehaviour.HREmployees, ref column1);

            Utils.AddButton("Max Skill of employees", TrainerBehaviour.EmployeesToMax, ref column1);

            Utils.AddButton("Remove Products", TrainerBehaviour.RemoveSoft, ref column1);

            Utils.AddButton("Reset age of employees", TrainerBehaviour.ResetAgeOfEmployees, ref column1);

            Utils.AddButton("Sell products stock", TrainerBehaviour.SellProductStock, ref column1);

            Utils.AddButton("Unlock all furniture", TrainerBehaviour.UnlockFurniture, ref column1);

            Utils.AddButton("Unlock all space", TrainerBehaviour.UnlockAllSpace, ref column1);

            Utils.AddButton("Employee skill settings", TrainerBehaviour.DisplayEmployeesWindow, ref column1);

            //Utils.AddButton("Test", TrainerBehaviour.Test, ref column1);


            Utils.AddToggle("Disable Needs", PropertyHelper.GetProperty("NoNeeds"),
                a => PropertyHelper.SetProperty("NoNeeds", !PropertyHelper.GetProperty("NoNeeds")), ref column2);

            Utils.AddToggle("Disable Stress", PropertyHelper.GetProperty("NoStress"),
                a => PropertyHelper.SetProperty("NoStress", !PropertyHelper.GetProperty("NoStress")), ref column2);

            Utils.AddToggle("Free Employees", PropertyHelper.GetProperty("FreeEmployees"),
                a => PropertyHelper.SetProperty("FreeEmployees", !PropertyHelper.GetProperty("FreeEmployees")), ref column2);

            Utils.AddToggle("Free Staff", PropertyHelper.GetProperty("FreeStaff"),
                a => PropertyHelper.SetProperty("FreeStaff", !PropertyHelper.GetProperty("FreeStaff")), ref column2);

            Utils.AddToggle("Full Efficiency", PropertyHelper.GetProperty("FullEfficiency"),
                a => PropertyHelper.SetProperty("FullEfficiency", !PropertyHelper.GetProperty("FullEfficiency")), ref column2);

            Utils.AddToggle("Full Satisfaction", PropertyHelper.GetProperty("FullSatisfaction"),
                a => PropertyHelper.SetProperty("FullSatisfaction", !PropertyHelper.GetProperty("FullSatisfaction")), ref column2);

            Utils.AddToggle("Lock Age of Employees", PropertyHelper.GetProperty("LockAge"),
                a => PropertyHelper.SetProperty("LockAge", !PropertyHelper.GetProperty("LockAge")), ref column2);

            Utils.AddToggle("No Vacation", PropertyHelper.GetProperty("NoVacation"),
                a => PropertyHelper.SetProperty("NoVacation", !PropertyHelper.GetProperty("NoVacation")), ref column2);

            Utils.AddToggle("No Sickness", PropertyHelper.GetProperty("NoSickness"),
                a => PropertyHelper.SetProperty("NoSickness", !PropertyHelper.GetProperty("NoSickness")), ref column2);

            Utils.AddToggle("Ultra Efficiency (Tick Full Eff first)", PropertyHelper.GetProperty("UltraEfficiency"),
                a => PropertyHelper.SetProperty("UltraEfficiency", !PropertyHelper.GetProperty("UltraEfficiency")), ref column2);

            Utils.AddToggle("Full Environment", PropertyHelper.GetProperty("FullEnvironment"),
                a => PropertyHelper.SetProperty("FullEnvironment", !PropertyHelper.GetProperty("FullEnvironment")), ref column3);

            Utils.AddToggle("Full Sun Light", PropertyHelper.GetProperty("FullRoomBrightness"),
                a => PropertyHelper.SetProperty("FullRoomBrightness", !PropertyHelper.GetProperty("FullRoomBrightness")), ref column3);

            Utils.AddToggle("Lock Temperature To 21", PropertyHelper.GetProperty("TemperatureLock"),
                a => PropertyHelper.SetProperty("TemperatureLock", !PropertyHelper.GetProperty("TemperatureLock")), ref column3);

            Utils.AddToggle("No Maintenance", PropertyHelper.GetProperty("NoMaintenance"),
                a => PropertyHelper.SetProperty("NoMaintenance", !PropertyHelper.GetProperty("NoMaintenance")), ref column3);

            Utils.AddToggle("Noise Reduction", PropertyHelper.GetProperty("NoiseReduction"),
                a => PropertyHelper.SetProperty("NoiseReduction", !PropertyHelper.GetProperty("NoiseReduction")), ref column3);

            Utils.AddToggle("Rooms Never Dirty", PropertyHelper.GetProperty("CleanRooms"),
                a => PropertyHelper.SetProperty("CleanRooms", !PropertyHelper.GetProperty("CleanRooms")), ref column3);

            //Utils.AddToggle("Disable Burglars", PropertyHelper.GetProperty("DisableBurglars"),
            //    a => PropertyHelper.SetProperty("DisableBurglars", !PropertyHelper.GetProperty("DisableBurglars")), ref column3);

            //Utils.AddToggle("Disable Fires", PropertyHelper.GetProperty("DisableFires"),
            //    a => PropertyHelper.SetProperty("DisableFires", !PropertyHelper.GetProperty("DisableFires")), ref column3);


            Utils.AddToggle("Auto Distribution Deals", PropertyHelper.GetProperty("AutoDistributionDeals"),
                a => PropertyHelper.SetProperty("AutoDistributionDeals", !PropertyHelper.GetProperty("AutoDistributionDeals")), ref column4);

            Utils.AddToggle("Free Print", PropertyHelper.GetProperty("FreePrint"),
                a => PropertyHelper.SetProperty("FreePrint", !PropertyHelper.GetProperty("FreePrint")), ref column4);

            Utils.AddToggle("Free Water & Electricity", PropertyHelper.GetProperty("NoWaterElectricity"),
                a => PropertyHelper.SetProperty("NoWaterElectricity", !PropertyHelper.GetProperty("NoWaterElectricity")), ref column4);

            Utils.AddToggle("Increase Bookshelf Skill", PropertyHelper.GetProperty("IncreaseBookshelfSkill"),
                a => PropertyHelper.SetProperty("IncreaseBookshelfSkill", !PropertyHelper.GetProperty("IncreaseBookshelfSkill")), ref column4);

            Utils.AddToggle("Increase Courier Capacity", PropertyHelper.GetProperty("IncreaseCourierCapacity"),
                a => PropertyHelper.SetProperty("IncreaseCourierCapacity", !PropertyHelper.GetProperty("IncreaseCourierCapacity")), ref column4);

            Utils.AddToggle("Increase Print Speed", PropertyHelper.GetProperty("IncreasePrintSpeed"),
                a => PropertyHelper.SetProperty("IncreasePrintSpeed", !PropertyHelper.GetProperty("IncreasePrintSpeed")), ref column4);

            Utils.AddToggle("More Hosting Deals", PropertyHelper.GetProperty("MoreHostingDeals"),
                a => PropertyHelper.SetProperty("MoreHostingDeals", !PropertyHelper.GetProperty("MoreHostingDeals")), ref column4);

            Utils.AddToggle("Reduce Internet Cost", PropertyHelper.GetProperty("ReduceISPCost"),
                a => PropertyHelper.SetProperty("ReduceISPCost", !PropertyHelper.GetProperty("ReduceISPCost")), ref column4);

            //Utils.AddToggle("Disable Skill Decay", PropertyHelper.GetProperty("DisableSkillDecay"),
            //    a => PropertyHelper.SetProperty("DisableSkillDecay", !PropertyHelper.GetProperty("DisableSkillDecay")), ref column4);

            Utils.AddToggle("No Server Cost", PropertyHelper.GetProperty("NoServerCost"),
                a => PropertyHelper.SetProperty("NoServerCost", !PropertyHelper.GetProperty("NoServerCost")), ref column4);

            Utils.AddToggle("Reduce Expansion Cost", PropertyHelper.GetProperty("ReduceExpansionCost"),
                a => PropertyHelper.SetProperty("ReduceExpansionCost", !PropertyHelper.GetProperty("ReduceExpansionCost")), ref column4);

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