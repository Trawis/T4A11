﻿using System;
using System.Collections.Generic;

namespace Trainer_v4
{
	public static class PropertyHelper
	{
		public static System.Random Random { get; set; }
		public static bool RewardIsGained { get; set; }
		public static bool DealIsPushed { get; set; }
		public static string ProductPriceName { get; set; }

		private static int _UltraEfficiencyStore = 20;
		public static int UltraEfficiencyMultipplier { get { return _UltraEfficiencyStore; } set { _UltraEfficiencyStore = value > 20 ? value : 20; } }

		public static bool IsGameLoaded
		{
			get { return GameSettings.Instance != null && HUD.Instance != null; }
		}

		public static bool IsNewGameStarted
		{
			get { return ActorCustomization.Instance != null; }
		}

		public static float[] MaxDistributionPercentage
		{
			get { return new[] { 1f, 0.75f, 0.5f }; }
		}

		private static Dictionary<string, bool> _settings = new Dictionary<string, bool>
		{
			{"NoStress", false},
			{"NoVacation", false},
			{"FullRoomBrightness", false},
			{"CleanRooms", false},
			{"FullEnvironment", false},
			{"NoiseReduction", false},
			{"FreeStaff", false},
			{"TemperatureLock", false},
			{"NoWaterElectricity", false},
			{"NoNeeds", false},
			{"FullEfficiency", false},
			{"FreeEmployees", false},
			{"LockAge", false},
			{"AutoDistributionDeals", false},
			{"MoreHostingDeals", false},
			{"IncreaseCourierCapacity", false},
			{"ReduceISPCost", false},
			{"IncreasePrintSpeed", false},
			{"FreePrint", false},
			{"IncreaseBookshelfSkill", false},
			{"NoMaintenance", false},
			{"NoSickness", false},
			{"UltraEfficiency", false},
			{"FullSatisfaction", false},
			{"DisableSkillDecay", false},
			{"DisableBurglars", false},
			{"DisableFires", false},
			{"NoServerCost", false},
			{"ReduceExpansionCost", false},
			{"NoEducationCost", false},
			{"IncreaseWalkSpeed", false},
			{"AutoEndDesign", false},
			{"AutoEndResearch", false},
			{"AutoEndPatent", false},
			{"ReduceBoxPrice", false}
		};

		private static Dictionary<string, bool> _rolesList = new Dictionary<string, bool>
		{
			{"Lead", false},
			{"Service", false},
			{"Programmer", false},
			{"Artist", false},
			{"Designer", false}
		};

		public static Dictionary<string, bool> Settings
		{
			get { return _settings; }
		}

		public static Dictionary<string, bool> RolesList
		{
			get { return _rolesList; }
		}

		public static Dictionary<string, bool> SpecializationsList { get; set; }

		public static bool GetProperty(Dictionary<string, bool> properties, string key)
		{
			bool value;
			if (properties.TryGetValue(key, out value))
			{
				return value;
			}
			else
			{
				return false;
			}
		}

		public static void SetProperty(Dictionary<string, bool> properties, string key, bool value)
		{
			properties[key] = value;
		}

		#region extensions

		public static Employee.EmployeeRole ToEmployeeRole(this string str)
		{
			return (Employee.EmployeeRole)Enum.Parse(typeof(Employee.EmployeeRole), str);
		}

		#endregion
	}
}