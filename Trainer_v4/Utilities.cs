using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Trainer_v4
{
    public class Utilities
    {
        public static void AddButton(string Text, UnityAction Action, ref List<GameObject> Buttons)
        {
            Button x = WindowManager.SpawnButton();
            x.GetComponentInChildren<Text>().text = Text;
            x.onClick.AddListener(Action);
            Buttons.Add(x.gameObject);
        }

        public static void AddButton(string Text, Rect Button, UnityAction Action)
        {
            Button x = WindowManager.SpawnButton();
            x.GetComponentInChildren<Text>().text = Text;
            x.onClick.AddListener(Action);
            WindowManager.AddElementToWindow(x.gameObject, SettingsWindow.Window, Button, new Rect(0, 0, 0, 0));
        }

        public static void AddInputBox(string Text, Rect InputBox, UnityAction<string> Action)
        {
            InputField x = WindowManager.SpawnInputbox();
            x.text = Text;
            x.onValueChanged.AddListener(Action);
            WindowManager.AddElementToWindow(x.gameObject, SettingsWindow.Window, InputBox, new Rect(0, 0, 0, 0));
        }

        public static void AddLabel(string Text, Rect Label, GUIWindow window)
        {
            Text label = WindowManager.SpawnLabel();
            label.text = Text;
            WindowManager.AddElementToWindow(label.gameObject, window, Label, new Rect(0, 0, 0, 0));
        }

        public static void AddToggle(string Text, bool isOn, UnityAction<bool> Action, ref List<GameObject> Toggles)
        {
            Toggle Toggle = WindowManager.SpawnCheckbox();
            Toggle.GetComponentInChildren<Text>().text = Text;
            Toggle.isOn = isOn;
            Toggle.onValueChanged.AddListener(Action);
            Toggles.Add(Toggle.gameObject);
        }

        public static void CreateGameObjects(int column, int skipRows, GameObject[] gameObjects, GUIWindow window)
        {
            for (int i = 0; i < gameObjects.Length; i++)
            {
                GameObject item = gameObjects[i];

                WindowManager.AddElementToWindow(item, window,
                    new Rect(column, (i + skipRows) * Constants.ELEMENT_HEIGHT, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                    new Rect(0, 0, 0, 0));
            }
        }

        public static void SetWindowSize(int[] colums, int coordinateX, int yWindowOffset, GUIWindow window)
        {
            window.MinSize.x = coordinateX;
            window.MinSize.y = Mathf.Max(colums) * Constants.ELEMENT_HEIGHT + yWindowOffset;
        }
    }
}