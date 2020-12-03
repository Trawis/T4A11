﻿using System;
using System.Collections.Generic;
using System.Linq;
using OrbCreationExtensions;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Trainer_v4
{
	public class TrainerBehaviour : ModBehaviour
	{
		private bool _specializationsLoaded;

		private static GameSettings Settings => GameSettings.Instance;
		private static Dictionary<string, bool> TrainerSettings => Helpers.Settings;
		private static Dictionary<string, object> Stores => Helpers.Stores;

		private void Start()
		{
			Helpers.Random = new Random();

			if (!isActiveAndEnabled)
			{
				return;
			}

			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}

		private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
		{
			if (isActiveAndEnabled)
			{
				switch (scene.name)
				{
					case "MainMenu":
						if (Main.TrainerButton != null)
						{
							Destroy(Main.TrainerButton.gameObject);
							Destroy(Main.SkillChangeButton.gameObject);
						}
						break;
					case "MainScene":
						Main.CreateUIButtons();
						break;
					case "Customization":
						ActorCustomization.StartYears = new[]
						{
							1970, 1975, 1980, 1985, 1990, 1995, 2000, 2005, 2010, 2015, 2020, 2025, 2030
						};
						ActorCustomization.StartLoans = new[]
						{
							0, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 200000, 500000, 1000000
						};
						break;
					default:
						goto case "MainMenu";
				}
			}
		}

		private void Update()
		{
			if (!isActiveAndEnabled || !Helpers.IsGameLoaded)
			{
				return;
			}

			if (Input.GetKey(KeyCode.F1))
			{
				Main.OpenSettingsWindow();
			}

			if (Input.GetKey(KeyCode.F2))
			{
				Main.CloseSettingsWindow();
			}

			if (!_specializationsLoaded && HUD.Instance.mainReputataionBars.company != null)
			{
				LoadSpecializations();
				_specializationsLoaded = true;
				ShowDiscordInvite(displayAsPopup: true);
			}

			foreach (Furniture furniture in Settings.sRoomManager.AllFurniture)
			{
				if (Helpers.GetProperty(TrainerSettings, "NoiseReduction"))
				{
					furniture.ActorNoise = 0f;
					furniture.EnvironmentNoise = 0f;
					furniture.FinalNoise = 0f;
					furniture.Noisiness = 0;
				}

				if (Helpers.GetProperty(TrainerSettings, "NoWaterElectricity"))
				{
					furniture.Water = 0;
					furniture.Wattage = 0;
				}

				if (Helpers.GetProperty(TrainerSettings, "DisableFires"))
				{
					if (furniture.HasUpg && furniture.upg.FireStarter > 0.0f)
					{
						furniture.upg.FireStarter = 0.0f;
					}
					if (furniture.Parent.IsOnFire)
					{
						if (furniture.Parent.Temperature > 40f)
						{
							furniture.Parent.Temperature = 21f;
						}
						furniture.Parent.StopFire();
					}
				}

				if (Helpers.GetProperty(TrainerSettings, "IncreaseBookshelfSkill") && furniture.Type == "Bookshelf")
				{
					furniture.AuraValues[1] = 0.75f;
				}

				//TODO: else 0.25
				if (Helpers.GetProperty(TrainerSettings, "NoMaintenance"))
				{
					switch (furniture.Type)
					{
						case "Chair":
							if (furniture.Comfort < 1.2f)
							{
								furniture.Comfort = 1.5f;
							}
							goto case "Ventilation";
						case "CCTV":
						case "Computer":
						case "Lamp":
						case "Server":
						case "Product Printer":
						case "Radiator":
						case "Sink":
						case "Toilet":
						case "Ventilation":
							break;
						default:
							break;
					}

					if (furniture.HasUpg && (furniture.upg.Quality < 0.8f || furniture.upg.Broken))
					{
						furniture.upg.RepairMe();
					}
				}
			}

			for (int i = 0; i < Settings.sRoomManager.Rooms.Count; i++)
			{
				Room room = Settings.sRoomManager.Rooms[i];

				if (Helpers.GetProperty(TrainerSettings, "CleanRooms"))
				{
					room.ClearDirt();
				}

				if (Helpers.GetProperty(TrainerSettings, "TemperatureLock"))
				{
					room.Temperature = 21.4f;
				}

				if (Helpers.GetProperty(TrainerSettings, "FullEnvironment"))
				{
					room.FurnEnvironment = 4;
				}

				if (Helpers.GetProperty(TrainerSettings, "FullRoomBrightness"))
				{
					room.IndirectLighting = 8;
				}
			}

			for (int i = 0; i < Settings.sActorManager.Actors.Count; i++)
			{
				Actor actor = Settings.sActorManager.Actors[i];
				Employee employee = Settings.sActorManager.Actors[i].employee;

				if (Helpers.GetProperty(TrainerSettings, "NoSickness") && actor.SpecialState == Actor.HomeState.Sick)
				{
					var sickActors = TimeOfDay.Instance.Sick;

					if (sickActors != null)
					{
						if (sickActors.Contains(actor))
						{
							TimeOfDay.Instance.Sick.Remove(actor);
						}
					}
				}

				if (Helpers.GetProperty(TrainerSettings, "LockAge"))
				{
					employee.AgeMonth = (int)employee.Age * 12;
					actor.UpdateAgeLook();
				}

				if (Helpers.GetProperty(TrainerSettings, "NoStress"))
				{
					employee.Stress = 1;
				}

				actor.Effectiveness = (employee.RoleString.Contains("Lead") ? Helpers.GetProperty(Stores, "LeadEfficiencyStore") : Helpers.GetProperty(Stores, "EfficiencyStore")).MakeFloat();

				if (Helpers.GetProperty(TrainerSettings, "FullSatisfaction"))
				{
					employee.JobSatisfaction = 2f;
				}

				if (Helpers.GetProperty(TrainerSettings, "NoNeeds"))
				{
					employee.Bladder = 1;
					employee.Hunger = 1;
					employee.Energy = 1;
					employee.Social = 1;
				}

				if (Helpers.GetProperty(TrainerSettings, "FreeEmployees"))
				{
					employee.ChangeSalary(0f, 0f, actor, false);
				}

				if (Helpers.GetProperty(TrainerSettings, "NoiseReduction"))
				{
					actor.Noisiness = 0;
				}

				if (Helpers.GetProperty(TrainerSettings, "NoVacation"))
				{
					actor.VacationMonth = SDateTime.NextMonth(24);
				}

				actor.WalkSpeed = Helpers.GetProperty(TrainerSettings, "IncreaseWalkSpeed") ? 4f : 2f;
			}

			if (Helpers.GetProperty(TrainerSettings, "AutoDistributionDeals"))
			{
				Settings.Distribution.TimeToCancel = -999;
				foreach (var company in Settings.simulation.Companies)
				{
					company.Value.HasDistributionDeal = true;
				}
			}

			if (Helpers.GetProperty(TrainerSettings, "MoreHostingDeals"))
			{
				int inGameHour = TimeOfDay.Instance.Hour;

				if ((inGameHour == 9 || inGameHour == 15) && !Helpers.DealIsPushed)
				{
					PushDeal();
				}
				else if (inGameHour != 9 && inGameHour != 15 && Helpers.DealIsPushed)
				{
					Helpers.DealIsPushed = false;
				}

				if (!Helpers.RewardIsGained && inGameHour == 12)
				{
					PushReward();
				}
				else if (inGameHour != 12 && Helpers.RewardIsGained)
				{
					Helpers.RewardIsGained = false;
				}
			}

			if (Helpers.GetProperty(TrainerSettings, "DisableBurglars"))
			{
				foreach (var burglar in Settings.sActorManager.Others["Burglars"])
				{
					burglar.Dismiss();
					burglar.Despawned = true;
					Settings.sActorManager.RemoveFromAwaiting(burglar);
				}
			}

			if (Helpers.GetProperty(TrainerSettings, "AutoEndDesign"))
			{
				var designDocuments = Settings.MyCompany.WorkItems
														.OfType<DesignDocument>()
														.Where(d => d.HasFinished)
														.ToList();

				designDocuments.ForEach(designDocument =>
				{
					designDocument.PromoteAction();
				});
			}

			if (Helpers.GetProperty(TrainerSettings, "AutoEndResearch"))
			{
				var researchWorks = Settings.MyCompany.WorkItems
														.OfType<ResearchWork>()
														.Where(rw => rw.Finished)
														.ToList();

				researchWorks.ForEach(researchWork =>
				{
					GameSettings.Instance.MyCompany.AddResearch(researchWork.Spec, researchWork.Year);
					TechLevel tech = GameSettings.Instance.simulation.AddTechLevel(researchWork.Spec, researchWork.Year);
					if (tech != null)
					{
						LegalWork legalWork = new LegalWork(tech);
						GameSettings.Instance.MyCompany.WorkItems.Add(legalWork);
						GameSettings.Instance.ApplyDefaultTeams(legalWork, ((int)legalWork.Type).ToString() + "Team");
					}
					researchWork.Kill(false);
				});
			}

			if (Helpers.GetProperty(TrainerSettings, "AutoEndPatent"))
			{
				var legalWorks = Settings.MyCompany.WorkItems
													 .OfType<LegalWork>()
													 .Where(lw => lw.CurrentStage() == "Finished" &&
																lw.Type == LegalWork.WorkType.Patent)
													 .ToList();

				legalWorks.ForEach(legalWork =>
				{
					legalWork.PatentNow();
				});
			}

			//TODO: add printspeed and printprice when it's disabled (else)
			if (Helpers.GetProperty(TrainerSettings, "FreePrint"))
			{
				Settings.ProductPrinters.ForEach(p => p.PrintPrice = 0f);
			}

			if (Helpers.GetProperty(TrainerSettings, "IncreasePrintSpeed"))
			{
				Settings.ProductPrinters.ForEach(p => p.PrintSpeed = 2f);
			}

			if (Helpers.GetProperty(TrainerSettings, "NoEducationCost"))
			{
				EducationWindow.EdCost = new[]
				{
					0f, 0f, 0f
				};
			}

			if (Helpers.GetProperty(TrainerSettings, "FreeStaff"))
			{
				Settings.StaffSalaryDue = 0f;
			}

			if (Helpers.GetProperty(TrainerSettings, "NoServerCost"))
			{
				Settings.ServerCost = 0f;
			}

			GameSettings.MaxFloor = 100; //10 default
			AI.MaxBoxes = Helpers.GetProperty(TrainerSettings, "IncreaseCourierCapacity") ? 108 : 54;
			AI.BoxPrice = Helpers.GetProperty(TrainerSettings, "ReduceBoxPrice") ? 62.5f : 125;
			Settings.Environment.ISPCostFactor = 0f;
			Settings.ExpansionCost = Helpers.GetProperty(TrainerSettings, "ReduceExpansionCost") ? 175f : 350f;
		}

		private static void LoadSpecializations()
		{
			if (Helpers.SpecializationsList != null && Helpers.SpecializationsList.Count() > 0)
			{
				return;
			}

			var specializations = new Dictionary<string, bool>();

			foreach (var role in Helpers.RolesList)
			{
				foreach (var specialization in Settings.GetAllSpecializations(role.Key.ToEmployeeRole()))
				{
					if (!specializations.ContainsKey(specialization))
					{
						specializations.Add(specialization, false);
					}
				}
			}

			Helpers.SpecializationsList = specializations;
		}

		public static void ShowDiscordInvite(bool displayAsPopup = false)
		{
			string message = "Join us on our discord server\nhttps://discord.io/trainer";
			if (displayAsPopup)
			{
				HUD.Instance.AddPopupMessage(message, "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
			}
			else
			{
				WindowManager.SpawnDialog(message, false, DialogWindow.DialogType.Information);
			}
		}

		public static void SetSkillPerEmployee()
		{
			var selectedActors = SelectorController.Instance.Selected.OfType<Actor>().ToList();
			var selectedRoles = Helpers.RolesList.Where(r => r.Value).ToList();
			var selectedSpecializations = Helpers.SpecializationsList.Where(s => s.Value).ToList();

			if (selectedActors.Count == 0)
			{
				WindowManager.SpawnDialog("Select one or more employees.", false, DialogWindow.DialogType.Error);
				return;
			}
			else if (selectedRoles.Count() == 0)
			{
				WindowManager.SpawnDialog("Select one or more roles.", false, DialogWindow.DialogType.Error);
				return;
			}
			else if (selectedSpecializations.Count() == 0)
			{
				WindowManager.SpawnDialog("Select one or more specializations.", false, DialogWindow.DialogType.Error);
				return;
			}

			selectedActors.ForEach(actor =>
			{
				foreach (var role in selectedRoles)
				{
					actor.employee.ChangeSkillDirect(role.Key.ToEmployeeRole(), 1f);

					foreach (var specialization in selectedSpecializations)
					{
						actor.employee.AddSpecialization(role.Key.ToEmployeeRole(), specialization.Key, false, true, 3);
					}
				}
			});
		}

		public static void ClearLoans()
		{
			Settings.Loans.Clear();
			HUD.Instance.AddPopupMessage("Trainer: All loans are cleared!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void PushReward()
		{
			var Deals = HUD.Instance.dealWindow.GetActiveDeals().Where(deal => deal.ToString() == "ServerDeal").ToArray();

			if (!Deals.Any())
			{
				return;
			}

			for (int i = 0; i < Deals.Length; i++)
			{
				Settings.MyCompany.MakeTransaction(Helpers.Random.Next(500, 50000), Company.TransactionCategory.Deals);
			}

			Helpers.RewardIsGained = true;
		}

		public static void PushDeal()
		{
			Helpers.DealIsPushed = true;

			SoftwareProduct[] Products = Settings.simulation.GetAllProducts().Where(pr =>
						(pr.Type.ToString() == "CMS"
					|| pr.Type.ToString() == "Office Software"
					|| pr.Type.ToString() == "Operating System"
					|| pr.Type.ToString() == "Game")
					&& pr.Userbase > 0
					&& pr.DevCompany.Name != Settings.MyCompany.Name
					&& pr.ServerReq > 0
					&& !pr.ExternalHostingActive)
								.ToArray();

			if (Products.Length == 0)
			{
				return;
			}

			int index = Helpers.Random.Next(0, Products.Length);
			ServerDeal deal = new ServerDeal(Products[index]) { Request = true };
			deal.StillValid(true);
			HUD.Instance.dealWindow.InsertDeal(deal);
		}

		public static void Test()
		{
			var designDocuments = Settings.MyCompany.WorkItems.OfType<DesignDocument>().Where(d => !d.HasFinished).ToList();

			foreach (var designDocument in designDocuments)
			{
				for (int i = 0; i < DesignDocument.MaxIteration; i++)
				{
					if (designDocument.Parent == null && designDocument.Iteration < 3)
					{
						for (int index = 0; index < designDocument.Features.Length; index++)
						{
							designDocument.Features[index].ArtDone = designDocument.Features[index].CodeDone = false;
							designDocument.Features[index].Progress = 1f;
							designDocument.Features[index].DevTime = 1f;
							designDocument.Features[index].Qual = 1f;
							designDocument.Features[index].LastIterationProg = 1f;
						}
						DevConsole.Console.Log(designDocument.GetProgress());
						designDocument.Iteration++;
					}
				}
			}
		}

		public static void AIBankrupt()
		{
			SimulatedCompany[] Companies = Settings.simulation.Companies.Values.ToArray();

			for (int i = 0; i < Companies.Length; i++)
			{
				Companies[i].Bankrupt = true;
			}
		}

		public static void HREmployees()
		{
			if (!Helpers.IsGameLoaded || SelectorController.Instance == null)
			{
				return;
			}

			Actor[] Actors = Settings.sActorManager.Actors
															 .Where(actor => actor.employee.RoleString.Contains("Lead"))
															 .ToArray();

			if (Actors.Length == 0)
			{
				return;
			}

			for (var i = 0; i < Actors.Length; i++)
			{
				Actors[i].employee.SetSpecialization(Actors[i].employee.GetRoleOrNatural(), "HR", 5);
			}

			HUD.Instance.AddPopupMessage("Trainer: All leaders are now HRed!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
		}

		public static void SellProductStock()
		{
			WindowManager.SpawnDialog("Stock of products with no active users were sold at half the price.",
					false, DialogWindow.DialogType.Information);

			SoftwareProduct[] Products = Settings.MyCompany.Products
																					 .Where(product => product.Userbase == 0)
																					 .ToArray();

			if (Products.Length == 0)
			{
				return;
			}

			for (int i = 0; i < Products.Length; i++)
			{
				SoftwareProduct product = Products[i];
				int st = (int)product.PhysicalCopies * (int)(product.Price / 2);

				product.PhysicalCopies = 0;
				Settings.MyCompany.MakeTransaction(st, Company.TransactionCategory.Sales);
			}
		}

		public static void RemoveSoft()
		{
			SDateTime time = new SDateTime(1, 70);
			CompanyType type = new CompanyType();
			var dict = new Dictionary<string, string[]>();
			var sim = new MarketSimulation();
			SimulatedCompany simComp = new SimulatedCompany("Trainer Company", time, type, dict, 0f, sim);
			simComp.CanMakeTransaction(1000000000f);

			SoftwareProduct[] Products = Settings.simulation.GetAllProducts().Where(product =>
					product.DevCompany == Settings.MyCompany &&
					product.Inventor != Settings.MyCompany.Name).ToArray();

			if (Products.Length == 0)
			{
				return;
			}

			for (int i = 0; i < Products.Length; i++)
			{
				SoftwareProduct Product = Products[i];

				Product.Userbase = 0;
				Product.PhysicalCopies = 0;
				Product.Marketing = 0;
				Product.Trade(simComp);
			}

			WindowManager.SpawnDialog("Products that you didn't invent are removed.", false, DialogWindow.DialogType.Information);
		}

		public static void ResetAgeOfEmployees()
		{
			for (int i = 0; i < Settings.sActorManager.Actors.Count; i++)
			{
				var actor = Settings.sActorManager.Actors[i];

				actor.employee.AgeMonth = Employee.Youngest * 12;
				actor.UpdateAgeLook();
			}

			HUD.Instance.AddPopupMessage("Trainer: Employees age has been reset!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void EmployeesToMax()
		{
			if (!Helpers.IsGameLoaded || SelectorController.Instance == null)
			{
				return;
			}

			for (int index1 = 0; index1 < Settings.sActorManager.Actors.Count; index1++)
			{
				Actor actor = Settings.sActorManager.Actors[index1];
				for (int index = 0; index < Enum.GetNames(typeof(Employee.EmployeeRole)).Length; index++)
				{
					actor.employee.ChangeSkillDirect((Employee.EmployeeRole)index, 1f);

					foreach (var specialization in Settings.GetAllSpecializations((Employee.EmployeeRole)index))
					{
						actor.employee.AddSpecialization((Employee.EmployeeRole)index, specialization, false, true, 3);
					}
				}
			}

			HUD.Instance.AddPopupMessage("Trainer: All employees are now max skilled!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
		}

		public static void UnlockAllSpace()
		{
			if (!Helpers.IsGameLoaded)
			{
				return;
			}

			Example.TakeAllLand();
			HUD.Instance.AddPopupMessage("Trainer: All plots has been unlocked!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void UnlockFurniture()
		{
			if (!Helpers.IsGameLoaded)
			{
				return;
			}

			Example.UnlockFurniture();
			Cheats.UnlockFurn = true;
			HUD.Instance.UpdateFurnitureButtons();
			HUD.Instance.AddPopupMessage("Trainer: All furniture has been unlocked!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		#region MonthDays

		public static void MonthDaysAction(string input)
		{
			int i;
			if (!int.TryParse(input, out i))
			{
				return;
			}

			GameSettings.DaysPerMonth = i;
			WindowManager.SpawnDialog("You have changed days per month. Please restart the game.", false, DialogWindow.DialogType.Warning);
		}

		public static void MonthDays()
		{
			WindowManager.SpawnInputDialog("How many days per month do you want?", "Days per month", "2", MonthDaysAction);
		}

		#endregion

		#region Add Quality

		public static void AddQualityAction(string input)
		{
			WorkItem WorkItem = Settings.MyCompany.WorkItems
					.Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
							(item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).InBeta);

			if (WorkItem == null)
			{
				return;
			}

			var softwareAlpha = ((SoftwareAlpha)WorkItem);
			softwareAlpha.AddQuality(10f, 10f, false);
		}

		public static void AddQuality()
		{
			WindowManager.SpawnInputDialog("Type product name:", "Add Quality", "", AddQualityAction);
		}

		#endregion

		#region Fix Bugs

		public static void FixBugsAction(string input)
		{
			WorkItem WorkItem = Settings.MyCompany.WorkItems
					.Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
							(item as SoftwareAlpha).Name == input && (item as SoftwareAlpha).InBeta);

			if (WorkItem == null)
			{
				return;
			}

			((SoftwareAlpha)WorkItem).FixedBugs = ((SoftwareAlpha)WorkItem).MaxBugs;
		}

		public static void FixBugs()
		{
			WindowManager.SpawnInputDialog("Type product name:", "Fix Bugs", "", FixBugsAction);
		}

		#endregion

		#region Max Followers

		public static void MaxFollowersAction(string input)
		{
			WorkItem WorkItem = Settings.MyCompany.WorkItems
					.Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
							(item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).Paused);

			if (WorkItem == null)
			{
				return;
			}

			SoftwareAlpha alpha = (SoftwareAlpha)WorkItem;

			alpha.MaxFollowers += 1000000000;
			alpha.ReEvaluateMaxFollowers();

			alpha.FollowerChange += 1000000000f;
			alpha.Followers += 1000000000f;
		}

		public static void MaxFollowers()
		{
			WindowManager.SpawnInputDialog("Type product name:", "Max Followers", "", MaxFollowersAction);
		}

		#endregion

		#region Set Product Price

		public static void SetProductPriceAction(string input)
		{
			SoftwareProduct Product =
					Settings.MyCompany.Products.FirstOrDefault(product => product.Name == Helpers.ProductPriceName);

			if (Product == null)
			{
				return;
			}

			Product.Price = input.ConvertToFloatDef(50f);
			HUD.Instance.AddPopupMessage("Trainer: Price for " + Product.Name + " has been setted up!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void SetProductPrice()
		{
			WindowManager.SpawnInputDialog("Type product price:", "Product price", "50", SetProductPriceAction);
		}

		#endregion

		#region Set Product Stock

		public static void SetProductStockAction(string input)
		{
			SoftwareProduct Product =
					Settings.MyCompany.Products.FirstOrDefault(product => product.Name == Helpers.ProductPriceName);

			if (Product == null)
			{
				return;
			}

			Product.PhysicalCopies = (uint)input.ConvertToIntDef(100000);
			HUD.Instance.AddPopupMessage("Trainer: Stock for " + Product.Name + " has been setted up!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void SetProductStock()
		{
			WindowManager.SpawnInputDialog("Type product stock:", "Product stock", "100000", SetProductStockAction);
		}

		#endregion

		#region Add Active Users

		public static void AddActiveUsersAction(string input)
		{
			SoftwareProduct Product =
					Settings.MyCompany.Products.FirstOrDefault(product => product.Name == Helpers.ProductPriceName);

			if (Product == null)
			{
				return;
			}

			Product.Userbase = input.ConvertToIntDef(100000);
			HUD.Instance.AddPopupMessage("Trainer: Active users for " + Product.Name + " has been setted up!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void AddActiveUsers()
		{
			WindowManager.SpawnInputDialog("Type product active users:", "Product active users", "100000", AddActiveUsersAction);
		}

		#endregion

		#region Takeover Company

		public static void TakeoverCompanyAction(string input)
		{
			var simulatedCompany = Settings.simulation.Companies
					.FirstOrDefault(simCompany => simCompany.Value.Name == input).Value;

			if (simulatedCompany == null)
			{
				WindowManager.SpawnDialog("Trainer: Company " + input + " not found!", false, DialogWindow.DialogType.Information);
				return;
			}

			if (!simulatedCompany.CanBuyOut(Settings.MyCompany))
			{
				WindowManager.SpawnDialog("Trainer: Company can't be bought!", false, DialogWindow.DialogType.Information);
				return;
			}

			var simulatedCompanyWorth = simulatedCompany.Stocks[0].CurrentWorth;

			simulatedCompany.BuyOut(new Company[]
			{
				Settings.MyCompany
			}, false);

			Settings.MyCompany.MakeTransaction(simulatedCompanyWorth, Company.TransactionCategory.Stocks, (string)null, false);
			WindowManager.SpawnDialog("Trainer: Company " + input + " has been takovered by you!", false, DialogWindow.DialogType.Information);
		}

		public static void TakeoverCompany()
		{
			WindowManager.SpawnInputDialog("Type company name:", "Takeover Company", "", TakeoverCompanyAction);
		}

		#endregion

		#region Subsidiary Company

		public static void SubDCompanyAction(string input)
		{
			SimulatedCompany Company =
					Settings.simulation.Companies.FirstOrDefault(company => company.Value.Name == input).Value;

			if (Company == null)
			{
				return;
			}

			Company.MakeSubsidiary(Settings.MyCompany);
			HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " is now your subsidiary!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}

		public static void SubDCompany()
		{
			WindowManager.SpawnInputDialog("Type company name:", "Subsidiary Company", "", SubDCompanyAction);
		}

		#endregion

		#region Force Bankrupt

		public static void ForceBankruptAction(string input)
		{
			SimulatedCompany Company =
					Settings.simulation.Companies.FirstOrDefault(company => company.Value.Name == input).Value;

			DevConsole.Console.Log("input => " + input + " Company: " + Company);

			if (Company == null)
			{
				return;
			}

			Company.Bankrupt = !Company.Bankrupt;
		}

		public static void ForceBankrupt()
		{
			WindowManager.SpawnInputDialog("Type company name:", "Force Bankrupt", "", ForceBankruptAction);
		}

		#endregion

		#region Increase Money

		public static void IncreaseMoneyAction(string input)
		{
			Settings.MyCompany.MakeTransaction(input.ConvertToIntDef(100000), Company.TransactionCategory.Deals);
			HUD.Instance.AddPopupMessage("Trainer: Money has been added in category Deals!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
		}
		public static void IncreaseMoney()
		{
			WindowManager.SpawnInputDialog("How much money do you want to add?", "Add Money", "100000", IncreaseMoneyAction);
		}

		#endregion

		#region Add Rep

		public static void MaxReputation()
		{
			Settings.MyCompany.ChangeBusinessRep(1f, "Publisher", 1f);
			Settings.MyCompany.ChangeBusinessRep(1f, "Deal", 1f);
			Settings.MyCompany.ChangeBusinessRep(1f, "Printing", 1f);
			Settings.MyCompany.ChangeBusinessRep(1f, "Lawsuit", 1f);
			Settings.MyCompany.ChangeBusinessRep(1f, "Contract", 1f);
			WindowManager.SpawnDialog("Trainer: Max reputation is applied to all categories", false, DialogWindow.DialogType.Information);
		}

		public static void MaxMarketRecognition()
		{
			var softwareTypes = MarketSimulation.Active.SoftwareTypes.Values.Where(value => !value.OneClient).ToList();
			foreach (var softwareType in softwareTypes)
			{
				foreach (var category in softwareType.Categories.ToList())
				{
					Example.AddReputation(softwareType.Name, category.Key, int.MaxValue);
				}
			}

			WindowManager.SpawnDialog("Trainer: Max market recognition is applied to all software types and categories.", false, DialogWindow.DialogType.Information);
		}

		#endregion

		#region Overrides

		public override void OnActivate() { /* Mandatory but not needed */ }

		public override void OnDeactivate() { /* Mandatory but not needed */ }

		#endregion
	}
}
