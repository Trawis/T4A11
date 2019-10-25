﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace Trainer_v4
{
    public static class PropertyHelper
    {
        public static System.Random rnd { get; set; }
        public static bool IsGameLoaded { get { return GetProperty(_settings, "ModActive") && GameSettings.Instance != null && HUD.Instance != null; } }
        public static bool RewardIsGained { get; set; }
        public static bool DealIsPushed { get; set; }
        public static string ProductPriceName { get; set; }

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
            {"FullEfficiency", false},
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
            {"UltraEfficiency", false},
            {"FullSatisfaction", false},
            {"DisableSkillDecay", false},
            {"DisableBurglars", false},
            {"DisableFires", false},
            {"NoServerCost", false},
            {"ReduceExpansionCost", false}
        };

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

        public static Dictionary<string, bool> Settings
        {
            get { return _settings; }
        }

        public static Dictionary<string, bool> RolesList
        {
            get { return _rolesList; }
        }

        public static Dictionary<string, bool> SpecializationsList
        {
            get { return _specializationsList; }
        }

        public static bool GetProperty(Dictionary<string, bool> properties, string key)
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

        public static void SetProperty(Dictionary<string, bool> properties, string key, bool value)
        {
            DevConsole.Console.Log("set " + key + " " + value);
            properties[key] = value;
        }
    }
}
