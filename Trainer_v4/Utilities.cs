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

        public static void DoLoops(GameObject[] Column1, GameObject[] Column2, GameObject[] Column3, GameObject[] Column4)
        {
            for (int i = 0; i < Column1.Length; i++)
            {
                GameObject item = Column1[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, 
                    new Rect(Constants.FIRST_COLUMN, (i + Constants.ROWS_TO_SKIP) * Constants.ELEMENT_HEIGHT, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Column2.Length; i++)
            {
                GameObject item = Column2[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, 
                    new Rect(Constants.SECOND_COLUMN, (i + Constants.ROWS_TO_SKIP) * Constants.ELEMENT_HEIGHT, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Column3.Length; i++)
            {
                GameObject item = Column3[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, 
                    new Rect(Constants.THIRD_COLUMN, (i + Constants.ROWS_TO_SKIP) * Constants.ELEMENT_HEIGHT, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                    new Rect(0, 0, 0, 0));
            }

            for (int i = 0; i < Column4.Length; i++)
            {
                GameObject item = Column4[i];

                WindowManager.AddElementToWindow(item, SettingsWindow.Window, 
                    new Rect(Constants.FOURTH_COLUMN, (i + Constants.ROWS_TO_SKIP) * Constants.ELEMENT_HEIGHT, Constants.ELEMENT_WIDTH, Constants.ELEMENT_HEIGHT),
                    new Rect(0, 0, 0, 0));
            }
        }
    }
}