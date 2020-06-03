using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Trainer_v4
{
	public class Main : ModMeta
	{
		private TrainerBehaviour _trainerBehaviour;
		public static Button TrainerButton { get; set; }
		public static Button SkillChangeButton { get; set; }
		public static string Version { get { return "(v4.8.4)"; } }

		public override void Initialize(ModController.DLLMod parentMod)
		{
			_trainerBehaviour = parentMod.Behaviors.OfType<TrainerBehaviour>().First();
		}

		public static void OpenSettingsWindow()
		{
			SettingsWindow.Show();
		}

		public static void CloseSettingsWindow()
		{
			SettingsWindow.Close();
		}

		public static void CreateTrainerButton()
		{
			TrainerButton = WindowManager.SpawnButton();
			TrainerButton.GetComponentInChildren<Text>().text = "Trainer " + Version;
			TrainerButton.onClick.AddListener(() => SettingsWindow.Show());
			TrainerButton.name = "TrainerButton";

			WindowManager.AddElementToElement(TrainerButton.gameObject,
					WindowManager.FindElementPath("MainPanel/Holder/FanPanel").gameObject, new Rect(164, 0, 100, 32),
					new Rect(0, 0, 0, 0));
		}

		public static void AttachSkillChangeButtonToEmployeeWindow()
		{
			SkillChangeButton = WindowManager.SpawnButton();
			SkillChangeButton.GetComponentInChildren<Text>().text = "Skill Change";
			SkillChangeButton.onClick.AddListener(() => EmployeeSkillChangeWindow.Show());
			SkillChangeButton.name = "EmployeeSkillButton";

			WindowManager.AddElementToElement(SkillChangeButton.gameObject,
					WindowManager.FindElementPath("ActorWindow/ContentPanel/Panel").gameObject, new Rect(0, 0, 100, 32),
					new Rect(0, 0, 0, 0));
		}

		public override void ConstructOptionsScreen(RectTransform parent, bool inGame)
		{
			Text label = WindowManager.SpawnLabel();
			label.text = "Please load a game and press 'Trainer' button.";

			WindowManager.AddElementToElement(label.gameObject, parent.gameObject, new Rect(0, 0, 400, 128),
					new Rect(0, 0, 0, 0));
		}

		public override WriteDictionary Serialize(GameReader.LoadMode mode)
		{
			var data = new WriteDictionary();

			foreach (var setting in PropertyHelper.Settings)
			{
				data[setting.Key] = PropertyHelper.GetProperty(PropertyHelper.Settings, setting.Key);
			}

			foreach (var store in PropertyHelper.Stores)
			{
				data[store.Key] = PropertyHelper.GetProperty(PropertyHelper.Stores, store.Key);
			}

			return data;
		}

		public override void Deserialize(WriteDictionary data, GameReader.LoadMode mode)
		{
			var settings = PropertyHelper.Settings.Keys.ToList();
			foreach (var setting in settings)
			{
				PropertyHelper.SetProperty(PropertyHelper.Settings, setting, data.Get(setting, PropertyHelper.GetProperty(PropertyHelper.Settings, setting)));
			}

			var stores = PropertyHelper.Stores.Keys.ToList();
			foreach (var store in stores)
			{
				PropertyHelper.SetProperty(PropertyHelper.Stores, store, data.Get(store, PropertyHelper.GetProperty(PropertyHelper.Stores, store)));
			}
		}

		public override string Name
		{
			get
			{
				return "Trainer " + Version;
			}
		}
	}
}