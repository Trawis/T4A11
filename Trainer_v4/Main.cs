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
		public static string Version { get { return "(v4.7.9)"; } }

		public override void Initialize(ModController.DLLMod parentMod)
		{
			_trainerBehaviour = parentMod.Behaviors.OfType<TrainerBehaviour>().First();
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

		public static void OpenSettingsWindow()
		{
			SettingsWindow.Show();
		}

		public static void CloseSettingsWindow()
		{
			SettingsWindow.Close();
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

			foreach (var Pair in PropertyHelper.Settings)
			{
				data[Pair.Key] = PropertyHelper.GetProperty(PropertyHelper.Settings, Pair.Key);
			}

			return data;
		}

		public override void Deserialize(WriteDictionary data, GameReader.LoadMode mode)
		{
			var keys = PropertyHelper.Settings.Keys.ToList();
			foreach (var key in keys)
			{
				PropertyHelper.SetProperty(PropertyHelper.Settings, key, data.Get(key, PropertyHelper.GetProperty(PropertyHelper.Settings, key)));
			}
		}

		public override string Name
		{
			get
			{
				return "Trainer v4";
			}
		}
	}
}