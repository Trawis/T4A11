using System;
using System.Collections.Generic;
using OrbCreationExtensions;

namespace Trainer_v4
{
	public static class PropertyHelper
	{
		public static System.Random Random { get; set; }
		public static bool RewardIsGained { get; set; }
		public static bool DealIsPushed { get; set; }
		public static string ProductPriceName { get; set; }

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

		public static List<KeyValuePair<string, object>> EfficiencyValues
		{
			get
			{
				return new List<KeyValuePair<string, object>>
				{
					new KeyValuePair<string, object>("100%", 1),
					new KeyValuePair<string, object>("200%", 2),
					new KeyValuePair<string, object>("500%", 5),
					new KeyValuePair<string, object>("1000%", 10),
					new KeyValuePair<string, object>("2000%", 20),
					new KeyValuePair<string, object>("4000%", 40)
				};
			}
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

		private static Dictionary<string, object> _stores = new Dictionary<string, object>
		{
			{"EfficiencyStore", 2},
			{"LeadEfficiencyStore", 4}
		};

		public static Dictionary<string, bool> Settings
		{
			get { return _settings; }
		}

		public static Dictionary<string, bool> RolesList
		{
			get { return _rolesList; }
		}

		public static Dictionary<string, object> Stores
		{
			get { return _stores; }
		}

		public static Dictionary<string, bool> SpecializationsList { get; set; }

		public static bool GetProperty(Dictionary<string, bool> properties, string key)
		{
			bool value;
			if (properties.TryGetValue(key, out value))
			{
				return value;
			}
			return false;
		}

		public static object GetProperty(Dictionary<string, object> properties, string key)
		{
			object value;
			if (properties.TryGetValue(key, out value))
			{
				return value;
			}
			return null;
		}

		public static void SetProperty(Dictionary<string, bool> properties, string key, bool value)
		{
			properties[key] = value;
		}

		public static void SetProperty(Dictionary<string, object> properties, string key, object value)
		{
			properties[key] = value;
		}

		public static int GetIndex(List<KeyValuePair<string, object>> values, Dictionary<string, object> properties, string store, int valueType)
		{
			switch (valueType)
			{
				case 1:
					return values.FindIndex(x => x.Value.MakeInt() == GetProperty(properties, store).MakeInt());
				case 2:
					return values.FindIndex(x => x.Value.MakeFloat() == GetProperty(properties, store).MakeFloat());
				case 3:
					return values.FindIndex(x => x.Value.MakeString() == GetProperty(properties, store).MakeString());
				case 4:
					return values.FindIndex(x => x.Value.MakeBool() == GetProperty(properties, store).MakeBool());
				default:
					throw new NotImplementedException("Method GetIndex received an unkown value type as parameter");
			}
		}

		#region extensions

		public static Employee.EmployeeRole ToEmployeeRole(this string str)
		{
			return (Employee.EmployeeRole)Enum.Parse(typeof(Employee.EmployeeRole), str);
		}

		#endregion
	}
}