using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Utils = Trainer_v4.Utilities;

namespace Trainer_v4
{
    public class EmployeeSkillChangeWindow : MonoBehaviour
    {
        private static List<GameObject> _roleToggles = new List<GameObject>();
        private static List<GameObject> _specializationToggles = new List<GameObject>();
        private static string _title = "Employee Skill Change, by Trawis";

        public static GUIWindow Window;
        public static bool Shown = false;

        private static Dictionary<string, bool> _rolesList = new Dictionary<string, bool>
        {
            {"Lead", false},
            {"Service", false},
            {"Programmer", false},
            {"Artist", false},
            {"Designer", false}
        };

        private static Dictionary<string, bool> _specializationsList = new Dictionary<string, bool>
        {
            {"HR", false},
            {"Automation", false},
            {"Socialization", false},
            {"System", false},
            {"Network", false},
            {"2D", false},
            {"3D", false},
            {"Audio", false},
            {"Support", false},
            {"Marketing", false},
            {"Law", false}
        };

        public static void Show()
        {
            if (Shown)
            {
                Window.Close();
                Shown = false;
                return;
            }

            CreateWindow();
            Shown = true;
        }

        private static void CreateWindow()
        {
            Window = WindowManager.SpawnWindow();
            Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = _title;
            Window.name = "EmployeeSkillChange";
            Window.MainPanel.name = "EmployeeSkillChangePanel";

            if (Window.name == "EmployeeSkillChange")
            {
                Window.GetComponentsInChildren<Button>()
                  .SingleOrDefault(x => x.name == "CloseButton")
                  .onClick.AddListener(() => Shown = false);
            }

            //_roles.UpdateContent(rolesList);
            //_roles.Selected = 1;

            Utils.AddLabel("Roles", new Rect(10, 0, 150, 32), Window);

            foreach (var role in _rolesList)
            {
                Utils.AddToggle(role.Key, GetToggle(_rolesList, role.Key), a => SetToggle(_rolesList, role.Key, !GetToggle(_rolesList ,role.Key)), ref _roleToggles);
            }

            foreach (var specialization in _specializationsList)
            {
                Utils.AddToggle(specialization.Key, 
                                GetToggle(_specializationsList, specialization.Key), 
                                a => SetToggle(_specializationsList, specialization.Key, !GetToggle(_specializationsList, specialization.Key)), 
                                ref _specializationToggles);
            }

            Utils.CreateGameObjects(Constants.FIRST_COLUMN, 1, _roleToggles.ToArray(), Window);
            Utils.CreateGameObjects(Constants.SECOND_COLUMN, 1, _specializationToggles.ToArray(), Window);

            int[] columnsCount = new int[]
            {
                _rolesList.Count(),
                _specializationsList.Count()
            };

            Utils.SetWindowSize(columnsCount, 300, 32, Window);
        }

        private static bool GetToggle(Dictionary<string, bool> properties, string key)
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

        private static void SetToggle(Dictionary<string, bool> properties, string key, bool value)
        {
            DevConsole.Console.Log("set " + key + " " + value);
            properties[key] = value;
        }
    }
}
