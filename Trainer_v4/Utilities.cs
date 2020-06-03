using System.Collections.Generic;
using OrbCreationExtensions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Trainer_v4
{
	public static class Utilities
	{
		public static void AddButton(string text, UnityAction action, List<GameObject> buttons)
		{
			Button button = WindowManager.SpawnButton();
			button.GetComponentInChildren<Text>().text = text;
			button.onClick.AddListener(action);
			buttons.Add(button.gameObject);
		}

		public static void AddButton(string text, Rect rectButton, UnityAction action, GUIWindow window)
		{
			Button button = WindowManager.SpawnButton();
			button.GetComponentInChildren<Text>().text = text;
			button.onClick.AddListener(action);
			WindowManager.AddElementToWindow(button.gameObject, window, rectButton, new Rect(0, 0, 0, 0));
		}

		public static void AddInputBox(string text, Rect rectInputBox, UnityAction<string> action, GUIWindow window)
		{
			InputField inputBox = WindowManager.SpawnInputbox();
			inputBox.text = text;
			inputBox.onValueChanged.AddListener(action);
			WindowManager.AddElementToWindow(inputBox.gameObject, window, rectInputBox, new Rect(0, 0, 0, 0));
		}

		public static void AddLabel(string text, Rect labelRect, GUIWindow window)
		{
			Text label = WindowManager.SpawnLabel();
			label.text = text;
			WindowManager.AddElementToWindow(label.gameObject, window, labelRect, new Rect(0, 0, 0, 0));
		}

		public static void AddToggle(string text, bool isOn, UnityAction<bool> action, List<GameObject> toggles)
		{
			Toggle toggle = WindowManager.SpawnCheckbox();
			toggle.GetComponentInChildren<Text>().text = text;
			toggle.isOn = isOn;
			toggle.onValueChanged.AddListener(action);
			toggles.Add(toggle.gameObject);
		}

		public static GUICombobox AddComboBox(string text, List<KeyValuePair<string, object>> selectableItems, int selection, List<GameObject> comboBoxes)
		{
			Text label = WindowManager.SpawnLabel();
			label.text = text;

			GUICombobox comboBox = WindowManager.SpawnComboBox();
			comboBox.UpdateContent(selectableItems.Select((KeyValuePair<string, object> x) => x.Key));
			comboBox.UpdateSelection(selection);
			comboBoxes.Add(label.gameObject);
			comboBoxes.Add(comboBox.gameObject);

			return comboBox;
		}

		public static void CreateGameObjects(int column, int skipRows, GameObject[] gameObjects, GUIWindow window, bool isComboBox = false)
		{
			for (int i = 0; i < gameObjects.Length; i++)
			{
				GameObject item = gameObjects[i];

				WindowManager.AddElementToWindow(item, window,
						new Rect(column, (i + skipRows - (isComboBox ? 1 : 0)) * Constants.ELEMENT_HEIGHT + (isComboBox && i % 2 == 0 ? 16 : 0), Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
						new Rect(0, 0, 0, 0));
			}
		}

		public static void SetWindowSize(int[] colums, int xWindowSize, int yWindowOffset, GUIWindow window)
		{
			window.MinSize.x = xWindowSize;
			window.MinSize.y = Mathf.Max(colums) * Constants.ELEMENT_HEIGHT + yWindowOffset;
		}
	}
}