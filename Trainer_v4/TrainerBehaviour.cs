using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

namespace Trainer_v4
{
    public class TrainerBehaviour : ModBehaviour
    {
        private static GameSettings Settings
        {
            get { return GameSettings.Instance; }
        }

        private static Dictionary<string, bool> TrainerSettings
        {
            get { return PropertyHelper.Settings; }
        }

        private void Start()
        {
            PropertyHelper.Random = new Random();

            if (!isActiveAndEnabled)
            {
                return;
            }

            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (scene.name.Equals("MainMenu") && Main.TrainerButton != null)
            {
                Destroy(Main.TrainerButton.gameObject);
                Destroy(Main.SkillChangeButton.gameObject);
            }
            else if (scene.name.Equals("MainScene") && isActiveAndEnabled)
            {
                Main.CreateTrainerButton();
                Main.AttachSkillChangeButtonToEmployeeWindow();
            }
            else if (scene.name.Equals("Customization") && isActiveAndEnabled)
            {
                ActorCustomization.StartYears = new int[]
                {
                    1970, 1975, 1980, 1985, 1990, 1995, 2000, 2005, 2010, 2015, 2020, 2025, 2030
                };

                ActorCustomization.StartLoans = new int[]
                {
                    0, 1000, 2000, 5000, 10000, 20000, 50000, 100000, 200000, 500000, 1000000
                };
            }

            PropertyHelper.LoadedScene = scene.name;
            DevConsole.Console.Log("Loaded scene: " + PropertyHelper.LoadedScene);
        }

        private void Update()
        {
            if (!isActiveAndEnabled || !PropertyHelper.IsGameLoaded)
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

            if (PropertyHelper.GetProperty(TrainerSettings, "FreeStaff"))
            {
                Settings.StaffSalaryDue = 0f;
            }

            foreach (Furniture item in Settings.sRoomManager.AllFurniture)
            {
                if (PropertyHelper.GetProperty(TrainerSettings, "NoiseReduction"))
                {
                    item.ActorNoise = 0f;
                    item.EnvironmentNoise = 0f;
                    item.FinalNoise = 0f;
                    item.Noisiness = 0;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "NoWaterElectricity"))
                {
                    item.Water = 0;
                    item.Wattage = 0;
                }
            }

            for (int i = 0; i < Settings.sRoomManager.Rooms.Count; i++)
            {
                Room room = Settings.sRoomManager.Rooms[i];

                if (PropertyHelper.GetProperty(TrainerSettings, "CleanRooms"))
                {
                    room.ClearDirt();
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "TemperatureLock"))
                {
                    room.Temperature = 21.4f;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "FullEnvironment"))
                {
                    room.FurnEnvironment = 4;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "FullRoomBrightness"))
                {
                    room.IndirectLighting = 8;
                }
            }

            for (int i = 0; i < Settings.sActorManager.Actors.Count; i++)
            {
                Actor actor = Settings.sActorManager.Actors[i];
                Employee employee = Settings.sActorManager.Actors[i].employee;

                //if (PropertyHelper.GetProperty(TrainerSettings, "DisableSkillDecay"))
                //{
                //    for (int index = 0; index < 5; index++)
                //    {
                //        if (employee.IsRole((Employee.EmployeeRole)index))
                //        {
                //            employee.ChangeSkillDirect((Employee.EmployeeRole)index, employee.GetSkill((Employee.EmployeeRole)index));
                //            foreach (var specialization in Settings.Specializations)
                //            {
                //                employee.SetSpecialization(Employee.EmployeeRole.Designer, specialization, employee.GetSpecialization(Employee.EmployeeRole.Designer, specialization));
                //                employee.SetSpecialization(Employee.EmployeeRole.Artist, specialization, employee.GetSpecialization(Employee.EmployeeRole.Artist, specialization));
                //                employee.SetSpecialization(Employee.EmployeeRole.Programmer, specialization, employee.GetSpecialization(Employee.EmployeeRole.Programmer, specialization));
                //                employee.SetSpecialization(Employee.EmployeeRole.Lead, specialization, employee.GetSpecialization(Employee.EmployeeRole.Lead, specialization));
                //                employee.SetSpecialization(Employee.EmployeeRole.Service, specialization, employee.GetSpecialization(Employee.EmployeeRole.Service, specialization));
                //            }
                //        }
                //    }
                //}

                if (PropertyHelper.GetProperty(TrainerSettings, "LockAge"))
                {
                    employee.AgeMonth = (int)employee.Age * 12; //20*12
                    actor.UpdateAgeLook();
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "NoStress"))
                {
                    employee.Stress = 1;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "FullEfficiency"))
                {
                    if (employee.RoleString.Contains("Lead"))
                    {
                        actor.Effectiveness = PropertyHelper.GetProperty(TrainerSettings, "UltraEfficiency") ? 20 : 4;
                    }
                    else
                    {
                        actor.Effectiveness = PropertyHelper.GetProperty(TrainerSettings, "UltraEfficiency") ? 10 : 2;
                    }
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "FullSatisfaction"))
                {
                    employee.JobSatisfaction = 2f;
                    employee.JobSatisfaction = 2f;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "NoNeeds"))
                {
                    employee.Bladder = 1;
                    employee.Hunger = 1;
                    employee.Energy = 1;
                    employee.Social = 1;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "FreeEmployees"))
                {
                    employee.ChangeSalary(0f, 0f, actor, false);
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "NoiseReduction"))
                {
                    actor.Noisiness = 0;
                }

                if (PropertyHelper.GetProperty(TrainerSettings, "NoVacation"))
                {
                    actor.VacationMonth = SDateTime.NextMonth(24);
                }
            }

            LoanWindow.factor = 250000;
            GameSettings.MaxFloor = 75; //10 default
            AI.MaxBoxes = PropertyHelper.GetProperty(TrainerSettings, "IncreaseCourierCapacity") ? 108 : 54;
            Server.ISPCost = PropertyHelper.GetProperty(TrainerSettings, "ReduceISPCost") ? 25f : 50f;
            Settings.ExpansionCost = PropertyHelper.GetProperty(TrainerSettings, "ReduceExpansionCost") ? 175f : 350f;

            if (PropertyHelper.GetProperty(TrainerSettings, "NoServerCost"))
            {
                Settings.ServerCost = 0f;
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "NoEducationCost"))
            {
                EducationWindow.EdCost = new float[3]
                {
                    0f, 0f, 0f
                };
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "AutoDistributionDeals"))
            {
                foreach (var company in Settings.simulation.Companies)
                {
                    float money = company.Value.GetMoneyWithInsurance(true);

                    if (money < 10000000f)
                        company.Value.DistributionDeal = 0.05f;
                    else if (money > 10000000f && money < 100000000f)
                        company.Value.DistributionDeal = 0.10f;
                    else if (money > 100000000f && money < 250000000f)
                        company.Value.DistributionDeal = 0.15f;
                    else if (money > 250000000f && money < 500000000f)
                        company.Value.DistributionDeal = 0.20f;
                    else if (money > 500000000f && money < 1000000000f)
                        company.Value.DistributionDeal = 0.25f;
                    else if (money > 1000000000f)
                        company.Value.DistributionDeal = 0.30f;
                }
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "MoreHostingDeals"))
            {
                int inGameHour = TimeOfDay.Instance.Hour;

                if ((inGameHour == 9 || inGameHour == 15) && !PropertyHelper.DealIsPushed)
                {
                    PushDeal();
                }
                else if (inGameHour != 9 && inGameHour != 15 && PropertyHelper.DealIsPushed)
                {
                    PropertyHelper.DealIsPushed = false;
                }

                if (!PropertyHelper.RewardIsGained && inGameHour == 12)
                {
                    PushReward();
                }
                else if (inGameHour != 12 && PropertyHelper.RewardIsGained)
                {
                    PropertyHelper.RewardIsGained = false;
                }
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "IncreasePrintSpeed"))
            {
                for (int i = 0; i < Settings.ProductPrinters.Count; i++)
                {
                    Settings.ProductPrinters[i].PrintSpeed = 2f;
                }
            }

            //add printspeed and printprice when it's disabled (else) TODO
            if (PropertyHelper.GetProperty(TrainerSettings, "FreePrint"))
            {
                Settings.ProductPrinters.ForEach(p => p.PrintPrice = 0f);
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "IncreaseBookshelfSkill"))
            {
                foreach (var bookshelf in Settings.sRoomManager.AllFurniture)
                {
                    if (bookshelf.Type != "Bookshelf")
                        continue;

                    foreach (var x in bookshelf.AuraValues)
                    {
                        bookshelf.AuraValues[1] = 0.75f;
                    }
                }
            }

            //else 0.25 TODO
            if (PropertyHelper.GetProperty(TrainerSettings, "NoMaintenance"))
            {
                foreach (Furniture furniture in Settings.sRoomManager.AllFurniture)
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
                            if (furniture.upg != null && furniture.upg.Quality < 0.8f)
                            {
                                furniture.upg.Quality = 1f;
                                furniture.upg.Broken = false;
                            }
                            break;
                    }
                }
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "NoSickness"))
            {
                var sickEmployees = Settings.sActorManager.Actors.Where(employee => employee.SpecialState == Actor.HomeState.Sick);
                var sickActors = TimeOfDay.Instance.Sick;

                if (sickEmployees != null && sickActors != null)
                {
                    foreach (var sickEmployee in sickEmployees)
                    {
                        if (sickActors.Contains(sickEmployee))
                        {
                            TimeOfDay.Instance.Sick.Remove(sickEmployee);
                        }
                    }
                }
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "DisableBurglars"))
            {
                foreach (var burglar in Settings.sActorManager.Others["Burglars"])
                {
                    burglar.Dismiss();
                    burglar.Despawned = true;
                    Settings.sActorManager.RemoveFromAwaiting(burglar);
                }
            }

            if (PropertyHelper.GetProperty(TrainerSettings, "DisableFires"))
            {
                foreach (var furniture in Settings.sRoomManager.AllFurniture)
                {
                    furniture.Parent.Temperature = 21f;
                    if (furniture.Parent.IsOnFire)
                    {
                        furniture.Parent.StopFire();
                    }
                }
            }

            Main.AttachSkillChangeButtonToEmployeeWindow();
        }

        //TODO: Automate software phases
        public static void CheckDesign(DesignDocument document)
        {
            var actors = GameSettings.Instance.sActorManager.Actors;
            Actor bestActor = actors.FirstOrDefault();

            foreach (var actor in actors)
            {
                if (actor.employee.GetSkill(Employee.EmployeeRole.Designer) > bestActor.employee.GetSkill(Employee.EmployeeRole.Designer))
                {
                    bestActor = actor;
                }
            }

            if (!document.HasFinished)
            {
                document.DoWork(bestActor, 1f, 1f, false);
            }

            if (document.Done)
            {
                document.PromoteAction();
            }
        }

        public static void CheckSoftwareProgress(SoftwareAlpha software)
        {
            string softwareName = software.SoftwareName;

            if (!software.InBeta)
            {
                MaxCodeAction(softwareName);
                MaxArtAction(softwareName);

                software.PromoteAction();
            }
            else
            {
                FixBugsAction(softwareName);
            }
        }

        public static void CheckResearch()
        {

        }

        public static void SetSkillPerEmployee()
        {
            var selectables = SelectorController.Instance.Selected.ToArray();
            var actors = Settings.sActorManager.Actors.ToArray();
            List<Actor> selectedActors = new List<Actor>();

            foreach (var actor in actors)
            {
                if (selectables.Contains(actor))
                {
                    selectedActors.Add(actor);
                }
            }

            var selectedRoles = PropertyHelper.RolesList.Where(r => r.Value);
            var selectedSpecializations = PropertyHelper.SpecializationsList.Where(s => s.Value);

            if (selectedActors.Count == 0)
            {
                WindowManager.SpawnDialog("Select one or more employees.", false, DialogWindow.DialogType.Warning);
                return;
            }
            else if (selectedRoles.Count() == 0)
            {
                WindowManager.SpawnDialog("Select one or more roles.", false, DialogWindow.DialogType.Warning);
                return;
            }
            else if (selectedSpecializations.Count() == 0)
            {
                WindowManager.SpawnDialog("Select one or more specializations.", false, DialogWindow.DialogType.Warning);
                return;
            }

            foreach (var actor in selectedActors)
            {
                foreach (var role in selectedRoles)
                {
                    actor.employee.ChangeSkillDirect(PropertyHelper.RoleStringToEnum[role.Key], 1f);

                    foreach (var specialization in selectedSpecializations)
                    {
                        actor.employee.AddSpecialization(PropertyHelper.RoleStringToEnum[role.Key], specialization.Key, false, true, 3);
                    }
                }
            }
        }

        public static void ClearLoans()
        {
            Settings.Loans.Clear();
            HUD.Instance.AddPopupMessage("Trainer: All loans are cleared!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
        }

        public void PushReward()
        {
            var Deals = HUD.Instance.dealWindow.GetActiveDeals().Where(deal => deal.ToString() == "ServerDeal")
                .ToArray();

            if (!Deals.Any())
            {
                return;
            }

            for (int i = 0; i < Deals.Length; i++)
            {
                Settings.MyCompany.MakeTransaction(PropertyHelper.Random.Next(500, 50000),
                       Company.TransactionCategory.Deals);
            }

            PropertyHelper.RewardIsGained = true;
        }

        public void PushDeal()
        {
            PropertyHelper.DealIsPushed = true;

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

            int index = PropertyHelper.Random.Next(0, Products.Length);

            SoftwareProduct prod = Settings.simulation.GetProduct(Products.ElementAt(index).SoftwareID, false);

            ServerDeal deal = new ServerDeal(Products[index]) { Request = true };
            deal.StillValid(true);
            HUD.Instance.dealWindow.InsertDeal(deal);
        }

        public static void ChangeEducationDays()
        {

        }

        public static void Test()
        {
            //GUIListView listView;
            //listView = WindowManager.SpawnList();
            //listView.Initialize();

            string[] specializations = Settings.Specializations;
            foreach (string specialization in specializations)
            {
                DevConsole.Console.Log(specialization);
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
            if (!PropertyHelper.IsGameLoaded || SelectorController.Instance == null)
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
            WindowManager.SpawnDialog("Products stock with no active users have been sold in half a price.",
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
            SimulatedCompany simComp = new SimulatedCompany("Trainer Company", time, type, dict, 0f);
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
                var item = Settings.sActorManager.Actors[i];

                item.employee.AgeMonth = 240;
                item.UpdateAgeLook();
            }

            HUD.Instance.AddPopupMessage("Trainer: Age of employees has been reset!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
        }

        public static void EmployeesToMax()
        {
            if (!PropertyHelper.IsGameLoaded || SelectorController.Instance == null)
            {
                return;
            }

            for (int index1 = 0; index1 < Settings.sActorManager.Actors.Count; index1++)
            {
                Actor actor = Settings.sActorManager.Actors[index1];
                for (int index = 0; index < Enum.GetNames(typeof(Employee.EmployeeRole)).Length; index++)
                {
                    actor.employee.ChangeSkillDirect((Employee.EmployeeRole)index, 1f);

                    switch ((Employee.EmployeeRole)index)
                    {
                        case Employee.EmployeeRole.Lead:
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Lead, "HR", false, true, 3);
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Lead, "Automation", false, true, 3);
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Lead, "Socialization", false, true, 3);
                            break;
                        case Employee.EmployeeRole.Designer:
                        case Employee.EmployeeRole.Programmer:
                            actor.employee.AddSpecialization((Employee.EmployeeRole)index, "System", false, true, 3);
                            actor.employee.AddSpecialization((Employee.EmployeeRole)index, "Network", false, true, 3);
                            goto case Employee.EmployeeRole.Artist;
                        case Employee.EmployeeRole.Artist:
                            actor.employee.AddSpecialization((Employee.EmployeeRole)index, "2D", false, true, 3);
                            actor.employee.AddSpecialization((Employee.EmployeeRole)index, "3D", false, true, 3);
                            actor.employee.AddSpecialization((Employee.EmployeeRole)index, "Audio", false, true, 3);
                            break;
                        case Employee.EmployeeRole.Service:
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Service, "Support", false, true, 3);
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Service, "Marketing", false, true, 3);
                            actor.employee.AddSpecialization(Employee.EmployeeRole.Service, "Law", false, true, 3);
                            break;
                    }
                }
            }

            HUD.Instance.AddPopupMessage("Trainer: All employees are now max skilled!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
        }

        public static void UnlockAllSpace()
        {
            if (!PropertyHelper.IsGameLoaded)
            {
                return;
            }

            Example.TakeAllLand();
            HUD.Instance.AddPopupMessage("Trainer: All plots are now unlocked!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
        }

        public static void UnlockFurniture()
        {
            if (!PropertyHelper.IsGameLoaded)
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

        #region Max Code

        public static void MaxCodeAction(string input)
        {
            WorkItem WorkItem = Settings.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).InBeta);

            if (WorkItem == null)
            {
                return;
            }

            var code = ((SoftwareAlpha)WorkItem);

            //float optimalProgress = 0f;

            for (int i = 1; i < 5; i++)
            {
                //float p = SoftwareAlpha.FinalQualityCalc(
                //    (Mathf.Pow(10, i) - 1f) / Mathf.Pow(10, i),
                //    SoftwareAlpha.GetMaxCodeQuality(code.CodeProgress)
                //);

                //if (p > optimalProgress)
                //{
                //    optimalProgress = p;
                //}
            }

            //code.CodeProgress = optimalProgress;
        }

        public static void MaxCode()
        {
            WindowManager.SpawnInputDialog("Type product name:", "Max Code", "", MaxCodeAction);
        }

        #endregion

        #region Max Art

        public static void MaxArtAction(string input)
        {
            WorkItem WorkItem = Settings.MyCompany.WorkItems
                .Where(item => item.GetType() == typeof(SoftwareAlpha)).FirstOrDefault(item =>
                    (item as SoftwareAlpha).Name == input && !(item as SoftwareAlpha).InBeta);

            if (WorkItem == null)
            {
                return;
            }

            var art = ((SoftwareAlpha)WorkItem);

            //float optimalProgress = 0;

            for (int i = 1; i < 5; i++)
            {
                //float p = SoftwareAlpha.FinalQualityCalc(
                //    (Mathf.Pow(10, i) - 1f) / Mathf.Pow(10, i),
                //    SoftwareAlpha.GetMaxCodeQuality(art.ArtProgress)
                //);

                //if (p > optimalProgress)
                //{
                //    optimalProgress = p;
                //}
            }

            //art.ArtProgress = optimalProgress;
        }

        public static void MaxArt()
        {
            WindowManager.SpawnInputDialog("Type product name:", "Max Art", "", MaxArtAction);
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
                Settings.MyCompany.Products.FirstOrDefault(product => product.Name == PropertyHelper.ProductPriceName);

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
                Settings.MyCompany.Products.FirstOrDefault(product => product.Name == PropertyHelper.ProductPriceName);

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
                Settings.MyCompany.Products.FirstOrDefault(product => product.Name == PropertyHelper.ProductPriceName);

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
            SimulatedCompany Company = Settings.simulation.Companies
                .FirstOrDefault(company => company.Value.Name == input).Value;

            if (Company == null)
            {
                return;
            }

            Company.BuyOut(Settings.MyCompany, true);
            HUD.Instance.AddPopupMessage("Trainer: Company " + Company.Name + " has been takovered by you!", "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0);
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
            Company.IsSubsidiary();
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

        public static void AddRepAction(string input)
        {
            var softwareType = MarketSimulation.Active.SoftwareTypes.Values.Where(value => !value.OneClient).GetRandom();
            var softwareCategory = softwareType.Categories.Keys.GetRandom();
            Settings.MyCompany.ChangeBusinessRep(5f, "", 1f);
            Example.AddReputation(softwareType.Name, softwareCategory, input.ConvertToIntDef(10000));
            HUD.Instance.AddPopupMessage("Trainer: Reputation has been added for SoftwareType: " + softwareType.Name + ", Category: " + softwareCategory, "Cogs", PopupManager.PopUpAction.None, 0, 0, 0, 0, 1);
        }

        public static void AddRep()
        {
            WindowManager.SpawnInputDialog("How much reputation do you want to add?", "Add Reputation", "10000", AddRepAction);
        }

        #endregion

        #region Overrides

        public override void OnActivate()
        {
            PropertyHelper.SetProperty(TrainerSettings, "ModActive", true);
        }

        public override void OnDeactivate()
        {
            PropertyHelper.SetProperty(TrainerSettings, "ModActive", false);
        }

        #endregion
    }
}
