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
		public static bool Shown;

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

			Utils.AddInputBox("ProductName".LocDef("Product Name Here"), new Rect(Constants.FIRST_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
					boxText => PropertyHelper.ProductPriceName = boxText, Window);

			Utils.AddButton("AddMoney".LocDef("Add Money"), new Rect(Constants.FIRST_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.IncreaseMoney, Window);

			Utils.AddButton("AddReputation".LocDef("Add Reputation"), new Rect(Constants.SECOND_COLUMN, Constants.FIRST_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddRep, Window);

			Utils.AddButton("SetProductPrice".LocDef("Set Product Price"), new Rect(Constants.SECOND_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductPrice, Window);

			Utils.AddButton("SetProductStock".LocDef("Set Product Stock"), new Rect(Constants.THIRD_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SetProductStock, Window);

			Utils.AddButton("SetActiveUsers".LocDef("Set Active Users"), new Rect(Constants.FOURTH_COLUMN, Constants.FOURTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.AddActiveUsers, Window);

			Utils.AddButton("MaxFollowers".LocDef("Max Followers"), new Rect(Constants.FIRST_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.MaxFollowers, Window);

			Utils.AddButton("FixBugs".LocDef("Fix Bugs"), new Rect(Constants.SECOND_COLUMN, Constants.SECOND_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.FixBugs, Window);

			//Utils.AddButton("Max Code", new Rect(Constants.THIRD_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxCode, Window);

			//Utils.AddButton("Max Art", new Rect(Constants.FOURTH_COLUMN, Constants.SECOND_ROW, Constants.Y_BUTTON_WIDTH, Constants.Y_BUTTON_HEIGHT), TrainerBehaviour.MaxArt, Window);

			Utils.AddButton("TakeoverCompany".LocDef("Takeover Company"), new Rect(Constants.FIRST_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.TakeoverCompany, Window);

			Utils.AddButton("SubsidiaryCompany".LocDef("Subsidiary Company"), new Rect(Constants.SECOND_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.SubDCompany, Window);

			Utils.AddButton("Bankrupt".LocDef("Bankrupt"), new Rect(Constants.THIRD_COLUMN, Constants.SIXTH_ROW, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT), TrainerBehaviour.ForceBankrupt, Window);

			#region column1

			Utils.AddButton("BankruptAll".LocDef("AI Bankrupt All"), TrainerBehaviour.AIBankrupt, column1);

			Utils.AddButton("DaysPerMonth".LocDef("Days per month"), TrainerBehaviour.MonthDays, column1);

			Utils.AddButton("ClearAllLoans".LocDef("Clear all loans"), TrainerBehaviour.ClearLoans, column1);

			Utils.AddButton("MaxSkill".LocDef("Max Skill of employees"), TrainerBehaviour.EmployeesToMax, column1);

			Utils.AddButton("RemoveProducts".LocDef("Remove Products"), TrainerBehaviour.RemoveSoft, column1);

			Utils.AddButton("ResetAge".LocDef("Reset age of employees"), TrainerBehaviour.ResetAgeOfEmployees, column1);

			Utils.AddButton("SellProductsStock".LocDef("Sell products stock"), TrainerBehaviour.SellProductStock, column1);

			Utils.AddButton("UnlockAllFurniture".LocDef("Unlock all furniture"), TrainerBehaviour.UnlockFurniture, column1);

			Utils.AddButton("UnlockAllSpace".LocDef("Unlock all space"), TrainerBehaviour.UnlockAllSpace, column1);

			//Utils.AddButton("Test".LocDef("Test"), TrainerBehaviour.Test, ref column1);

			#endregion

			#region column2

			Utils.AddToggle("DisableNeeds".LocDef("Disable Needs"), PropertyHelper.GetProperty(settings, "NoNeeds"),
					a => PropertyHelper.SetProperty(settings, "NoNeeds", !PropertyHelper.GetProperty(settings, "NoNeeds")), column2);

			Utils.AddToggle("DisableStress".LocDef("Disable Stress"), PropertyHelper.GetProperty(settings, "NoStress"),
					a => PropertyHelper.SetProperty(settings, "NoStress", !PropertyHelper.GetProperty(settings, "NoStress")), column2);

			Utils.AddToggle("FreeEmployees".LocDef("Free Employees"), PropertyHelper.GetProperty(settings, "FreeEmployees"),
					a => PropertyHelper.SetProperty(settings, "FreeEmployees", !PropertyHelper.GetProperty(settings, "FreeEmployees")), column2);

			Utils.AddToggle("FreeStaff".LocDef("Free Staff"), PropertyHelper.GetProperty(settings, "FreeStaff"),
					a => PropertyHelper.SetProperty(settings, "FreeStaff", !PropertyHelper.GetProperty(settings, "FreeStaff")), column2);

			Utils.AddToggle("FullEfficiency".LocDef("Full Efficiency"), PropertyHelper.GetProperty(settings, "FullEfficiency"),
					a => PropertyHelper.SetProperty(settings, "FullEfficiency", !PropertyHelper.GetProperty(settings, "FullEfficiency")), column2);

			Utils.AddToggle("FullSatisfaction".LocDef("Full Satisfaction"), PropertyHelper.GetProperty(settings, "FullSatisfaction"),
					a => PropertyHelper.SetProperty(settings, "FullSatisfaction", !PropertyHelper.GetProperty(settings, "FullSatisfaction")), column2);

			Utils.AddToggle("LockAge".LocDef("Lock Age of Employees"), PropertyHelper.GetProperty(settings, "LockAge"),
					a => PropertyHelper.SetProperty(settings, "LockAge", !PropertyHelper.GetProperty(settings, "LockAge")), column2);

			Utils.AddToggle("NoVacation".LocDef("No Vacation"), PropertyHelper.GetProperty(settings, "NoVacation"),
					a => PropertyHelper.SetProperty(settings, "NoVacation", !PropertyHelper.GetProperty(settings, "NoVacation")), column2);

			Utils.AddToggle("NoSickness".LocDef("No Sickness"), PropertyHelper.GetProperty(settings, "NoSickness"),
					a => PropertyHelper.SetProperty(settings, "NoSickness", !PropertyHelper.GetProperty(settings, "NoSickness")), column2);

			Utils.AddToggle("UltraEfficiency".LocDef("Ultra Efficiency (Tick Full Eff first)"), PropertyHelper.GetProperty(settings, "UltraEfficiency"),
					a => PropertyHelper.SetProperty(settings, "UltraEfficiency", !PropertyHelper.GetProperty(settings, "UltraEfficiency")), column2);

			#endregion

			#region column3

			Utils.AddToggle("FullEnvironment".LocDef("Full Environment"), PropertyHelper.GetProperty(settings, "FullEnvironment"),
					a => PropertyHelper.SetProperty(settings, "FullEnvironment", !PropertyHelper.GetProperty(settings, "FullEnvironment")), column3);

			Utils.AddToggle("FullSunLight".LocDef("Full Sun Light"), PropertyHelper.GetProperty(settings, "FullRoomBrightness"),
					a => PropertyHelper.SetProperty(settings, "FullRoomBrightness", !PropertyHelper.GetProperty(settings, "FullRoomBrightness")), column3);

			Utils.AddToggle("LockTemperature".LocDef("Lock Temperature To 21"), PropertyHelper.GetProperty(settings, "TemperatureLock"),
					a => PropertyHelper.SetProperty(settings, "TemperatureLock", !PropertyHelper.GetProperty(settings, "TemperatureLock")), column3);

			Utils.AddToggle("NoMaintenance".LocDef("No Maintenance"), PropertyHelper.GetProperty(settings, "NoMaintenance"),
					a => PropertyHelper.SetProperty(settings, "NoMaintenance", !PropertyHelper.GetProperty(settings, "NoMaintenance")), column3);

			Utils.AddToggle("NoiseReduction".LocDef("Noise Reduction"), PropertyHelper.GetProperty(settings, "NoiseReduction"),
					a => PropertyHelper.SetProperty(settings, "NoiseReduction", !PropertyHelper.GetProperty(settings, "NoiseReduction")), column3);

			Utils.AddToggle("RoomsNeverDirty".LocDef("Rooms Never Dirty"), PropertyHelper.GetProperty(settings, "CleanRooms"),
					a => PropertyHelper.SetProperty(settings, "CleanRooms", !PropertyHelper.GetProperty(settings, "CleanRooms")), column3);

			Utils.AddToggle("NoEducationCost".LocDef("No Education Cost"), PropertyHelper.GetProperty(settings, "NoEducationCost"),
					a => PropertyHelper.SetProperty(settings, "NoEducationCost", !PropertyHelper.GetProperty(settings, "NoEducationCost")), column3);

			//Utils.AddToggle("Disable Burglars", PropertyHelper.GetProperty(settings, "DisableBurglars"),
			//    a => PropertyHelper.SetProperty(settings, "DisableBurglars", !PropertyHelper.GetProperty(settings, "DisableBurglars")), ref column3);

			//Utils.AddToggle("Disable Fires", PropertyHelper.GetProperty(settings, "DisableFires"),
			//    a => PropertyHelper.SetProperty(settings, "DisableFires", !PropertyHelper.GetProperty(settings, "DisableFires")), ref column3);

			Utils.AddToggle("AutoDesignEnd".LocDef("Auto Design End"), PropertyHelper.GetProperty(settings, "AutoEndDesign"),
					a => PropertyHelper.SetProperty(settings, "AutoEndDesign", !PropertyHelper.GetProperty(settings, "AutoEndDesign")), column3);

			Utils.AddToggle("AutoResearchEnd".LocDef("Auto Research End"), PropertyHelper.GetProperty(settings, "AutoEndResearch"),
					a => PropertyHelper.SetProperty(settings, "AutoEndResearch", !PropertyHelper.GetProperty(settings, "AutoEndResearch")), column3);

			Utils.AddToggle("AutoPatentEnd".LocDef("Auto Patent End"), PropertyHelper.GetProperty(settings, "AutoEndPatent"),
					a => PropertyHelper.SetProperty(settings, "AutoEndPatent", !PropertyHelper.GetProperty(settings, "AutoEndPatent")), column3);

			Utils.AddToggle("IncreaseWalkSpeed".LocDef("Increase Walk Speed"), PropertyHelper.GetProperty(settings, "IncreaseWalkSpeed"),
					a => PropertyHelper.SetProperty(settings, "IncreaseWalkSpeed", !PropertyHelper.GetProperty(settings, "IncreaseWalkSpeed")), column3);

			#endregion

			#region column4

			//Utils.AddToggle("Auto Distribution Deals", PropertyHelper.GetProperty(settings, "AutoDistributionDeals"),
			//    a => PropertyHelper.SetProperty(settings, "AutoDistributionDeals", !PropertyHelper.GetProperty(settings, "AutoDistributionDeals")), ref column4);

			Utils.AddToggle("FreePrint".LocDef("Free Print"), PropertyHelper.GetProperty(settings, "FreePrint"),
					a => PropertyHelper.SetProperty(settings, "FreePrint", !PropertyHelper.GetProperty(settings, "FreePrint")), column4);

			Utils.AddToggle("FreeWaterElectricity".LocDef("Free Water & Electricity"), PropertyHelper.GetProperty(settings, "NoWaterElectricity"),
					a => PropertyHelper.SetProperty(settings, "NoWaterElectricity", !PropertyHelper.GetProperty(settings, "NoWaterElectricity")), column4);

			Utils.AddToggle("IncreaseBookshelfSkill".LocDef("Increase Bookshelf Skill"), PropertyHelper.GetProperty(settings, "IncreaseBookshelfSkill"),
					a => PropertyHelper.SetProperty(settings, "IncreaseBookshelfSkill", !PropertyHelper.GetProperty(settings, "IncreaseBookshelfSkill")), column4);

			Utils.AddToggle("IncreaseCourierCapacity".LocDef("Increase Courier Capacity"), PropertyHelper.GetProperty(settings, "IncreaseCourierCapacity"),
					a => PropertyHelper.SetProperty(settings, "IncreaseCourierCapacity", !PropertyHelper.GetProperty(settings, "IncreaseCourierCapacity")), column4);

			Utils.AddToggle("IncreasePrintSpeed".LocDef("Increase Print Speed"), PropertyHelper.GetProperty(settings, "IncreasePrintSpeed"),
					a => PropertyHelper.SetProperty(settings, "IncreasePrintSpeed", !PropertyHelper.GetProperty(settings, "IncreasePrintSpeed")), column4);

			Utils.AddToggle("MoreHostingDeals".LocDef("More Hosting Deals"), PropertyHelper.GetProperty(settings, "MoreHostingDeals"),
					a => PropertyHelper.SetProperty(settings, "MoreHostingDeals", !PropertyHelper.GetProperty(settings, "MoreHostingDeals")), column4);

			Utils.AddToggle("ReduceInternetCost".LocDef("Reduce Internet Cost"), PropertyHelper.GetProperty(settings, "ReduceISPCost"),
					a => PropertyHelper.SetProperty(settings, "ReduceISPCost", !PropertyHelper.GetProperty(settings, "ReduceISPCost")), column4);

			//Utils.AddToggle("Disable Skill Decay", PropertyHelper.GetProperty(settings, "DisableSkillDecay"),
			//    a => PropertyHelper.SetProperty(settings, "DisableSkillDecay", !PropertyHelper.GetProperty(settings, "DisableSkillDecay")), ref column4);

			Utils.AddToggle("NoServerCost".LocDef("No Server Cost"), PropertyHelper.GetProperty(settings, "NoServerCost"),
					a => PropertyHelper.SetProperty(settings, "NoServerCost", !PropertyHelper.GetProperty(settings, "NoServerCost")), column4);

			Utils.AddToggle("ReduceExpansionCost".LocDef("Reduce Expansion Cost"), PropertyHelper.GetProperty(settings, "ReduceExpansionCost"),
					a => PropertyHelper.SetProperty(settings, "ReduceExpansionCost", !PropertyHelper.GetProperty(settings, "ReduceExpansionCost")), column4);

			Utils.AddToggle("ReduceBoxPrice".LocDef("Reduce Box Price"), PropertyHelper.GetProperty(settings, "ReduceBoxPrice"),
								a => PropertyHelper.SetProperty(settings, "ReduceBoxPrice", !PropertyHelper.GetProperty(settings, "ReduceBoxPrice")), column4);
			#endregion

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