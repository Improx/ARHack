﻿

namespace HuaweiARInternal {
    using UnityEngine;
    using System.IO;
    public class HuaweiARProjectSettings
    {
        public static HuaweiARProjectSettings Instance { get; private set; }
        public bool IsHuaweiARRequired;
        public bool IsARCoreRequired;
        private const string c_ProjectSettingsPath = "ProjectSettings/HuaweiARProjectSettings.json";

        static HuaweiARProjectSettings()
        {
            if (Application.isEditor)
            {
                Instance = new HuaweiARProjectSettings();
                Instance.LoadSettings();
            }
            else
            {
                Instance = null;
                Debug.LogError("Cannot access HuaweiARProjectSettings outside of Unity Editor");
            }
        }

        public void LoadSettings()
        {
            IsHuaweiARRequired = true;
            IsARCoreRequired = false;

            if(File.Exists(c_ProjectSettingsPath))
            {
                HuaweiARProjectSettings settings = JsonUtility.FromJson<HuaweiARProjectSettings>(
                    File.ReadAllText(c_ProjectSettingsPath));
                IsHuaweiARRequired = settings.IsHuaweiARRequired;
                IsARCoreRequired = settings.IsARCoreRequired;
            }
        }

        public void SaveSettings()
        {
            File.WriteAllText(c_ProjectSettingsPath, JsonUtility.ToJson(this));
        }
    }
}
