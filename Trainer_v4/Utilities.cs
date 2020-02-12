using System.Collections.Generic;
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

        public static void SetWindowSize(int[] colums, int xWindowSize, int yWindowOffset, GUIWindow window)
        {
            window.MinSize.x = xWindowSize;
            window.MinSize.y = Mathf.Max(colums) * Constants.ELEMENT_HEIGHT + yWindowOffset;
        }
    }
}