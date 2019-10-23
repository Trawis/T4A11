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
        private GUICombobox _roles;
        //private GUIListView _specializations;
        private string title = "Employee Skill Change, by Trawis";

        public static GUIWindow Window;
        public static bool shown = false;

        public static EmployeeSkillChangeWindow Instance { get; set; } 

        private string[] rolesList =
        {
            "Lead",
            "Service",
            "Programmer",
            "Artist",
            "Designer"
        };

        public static void ShowWindow()
        {
            if (shown)
            {
                Window.Close();
                shown = false;
                return;
            }

            Instance.CreateWindow();
            shown = true;
        }

        private void CreateWindow()
        {
            Window = WindowManager.SpawnWindow();
            Window.InitialTitle = Window.TitleText.text = Window.NonLocTitle = title;
            Window.MinSize.x = 300;
            Window.MinSize.y = 200;
            Window.name = "EmployeeSkillChange";
            Window.MainPanel.name = "EmployeeSkillChangePanel";

            if (Window.name == "EmployeeSkillChange")
            {
                Window.GetComponentsInChildren<Button>()
                  .SingleOrDefault(x => x.name == "CloseEmployeeSkillChangeButton")
                  .onClick.AddListener(() => shown = false);
            }

            _roles.UpdateContent(rolesList);
            _roles.Selected = 1;

            Utils.AddLabel("Roles", new Rect(0, 0, 150, 32), Window);
            WindowManager.AddElementToWindow(_roles.gameObject, Window, new Rect(0, 32, 150, 32), new Rect(0, 0, 0, 0));
        }
    }
}
