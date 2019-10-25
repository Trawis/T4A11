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
        private static string _title = "Employee Skill Change, by Trawis";

        public static GUIWindow Window;
        public static bool Shown = false;

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

            List<GameObject> roleToggles = new List<GameObject>();
            List<GameObject> specializationToggles = new List<GameObject>();

            Utils.AddLabel("Roles", new Rect(10, 0, 150, 32), Window);

            foreach (var role in PropertyHelper.RolesList)
            {
                Utils.AddToggle(role.Key, PropertyHelper.GetProperty(PropertyHelper.RolesList, role.Key), 
                                     a => PropertyHelper.SetProperty(PropertyHelper.RolesList, role.Key, 
                                         !PropertyHelper.GetProperty(PropertyHelper.RolesList, role.Key)), 
                                     ref roleToggles);
            }

            Utils.AddButton("Set Skills", TrainerBehaviour.AIBankrupt, ref roleToggles);

            foreach (var specialization in PropertyHelper.SpecializationsList)
            {
                Utils.AddToggle(specialization.Key,
                                PropertyHelper.GetProperty(PropertyHelper.SpecializationsList, specialization.Key),
                                a => PropertyHelper.SetProperty(PropertyHelper.SpecializationsList, specialization.Key, 
                                    !PropertyHelper.GetProperty(PropertyHelper.SpecializationsList, specialization.Key)),
                                ref specializationToggles);
            }

            Utils.CreateGameObjects(Constants.FIRST_COLUMN, 1, roleToggles.ToArray(), Window);
            Utils.CreateGameObjects(Constants.SECOND_COLUMN, 1, specializationToggles.ToArray(), Window);

            int[] columnsCount = new int[]
            {
                roleToggles.Count(),
                specializationToggles.Count()
            };

            Utils.SetWindowSize(columnsCount, 300, 64, Window);
        }
    }
}
