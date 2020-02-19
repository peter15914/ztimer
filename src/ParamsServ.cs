using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace zTimer
{
    class ParamsServ
    {
        const string sRegPath = @"Software\zTimer";

        private static ParamsServ inst;

        public static ParamsServ Inst
        {
            get 
            {
                if (inst == null)
                {
                    inst = new ParamsServ();
                }
                return inst;
            }
        }

        public ParamsServ()
        {
        }

        private void SetVal(string sParamName, int iParamVal)
        {
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(sRegPath))
            {
                if (registryKey != null)
                    registryKey.SetValue(sParamName, iParamVal, RegistryValueKind.DWord);
            }
        }

        private int GetVal(string sParamName, int iDefault)
        {
            int ret = iDefault;
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(sRegPath))
            {
                if (registryKey != null)
                {
                    object obj = registryKey.GetValue(sParamName);
                    ret = (obj != null) ? (int)obj : iDefault;
                }
            }

            return ret;
        }

        private void SetValS(string sParamName, string sParamVal)
        {
            using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(sRegPath))
            {
                if (registryKey != null)
                    registryKey.SetValue(sParamName, sParamVal, RegistryValueKind.String);
            }
        }

        private string GetValS(string sParamName, string sDefault)
        {
            string ret = sDefault;
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(sRegPath))
            {
                if (registryKey != null)
                {
                    object obj = registryKey.GetValue(sParamName);
                    ret = (obj != null) ? (string)obj : sDefault;
                }
            }

            return ret;
        }

        public int GetWindosPosX(int iDefault)  {  return GetVal("WindosPosX", iDefault);  }
        public void SetWindosPosX(int iParamVal) { SetVal("WindosPosX", iParamVal); }

        public int GetWindosPosY(int iDefault) { return GetVal("WindosPosY", iDefault); }
        public void SetWindosPosY(int iParamVal) { SetVal("WindosPosY", iParamVal); }

        public int GetLanguage(int iDefault) { return GetVal("Language", iDefault); }
        public void SetLanguage(int iParamVal) { SetVal("Language", iParamVal); }

        public int GetSound(int iDefault) { return GetVal("Sound", iDefault); }
        public void SetSound(int iParamVal) { SetVal("Sound", iParamVal); }

        public int GetLastRmndrMin(int iDefault) { return GetVal("LastRmndrMin", iDefault); }
        public void SetLastRmndrMin(int iParamVal) { SetVal("LastRmndrMin", iParamVal); }

        public int GetLastRmndrSound(int iDefault) { return GetVal("LastRmndrSound", iDefault); }
        public void SetLastRmndrSound(int iParamVal) { SetVal("LastRmndrSound", iParamVal); }

        public string GetLastRmndrText(string sDefault) { return GetValS("LastRmndrText", sDefault); }
        public void SetLastRmndrText(string sParamVal) { SetValS("LastRmndrText", sParamVal); }

        public int GetRemindersCount() { return GetVal("RemindersCount", 0); }
        public void SetRemindersCount(int iParamVal) { SetVal("RemindersCount", iParamVal); }

        public int GetWndSizeType() { return GetVal("WndSizeType", 0); }
        public void SetWndSizeType(int iParamVal) { SetVal("WndSizeType", iParamVal); }

        public void SaveReminderParam(int num, REMINDER_STRUCT r)
        {
            string name = string.Format("r{0}_", num);

            SetVal(name + "m_minutes", r.m_minutes);
            SetVal(name + "m_sound", r.m_sound ? 1 : 0);
            SetValS(name + "m_text", r.m_text);
        }

        public void GetReminderParam(int num, REMINDER_STRUCT r)
        {
            string name = string.Format("r{0}_", num);

            r.m_minutes = GetVal(name + "m_minutes", 60);
            r.m_sound = (GetVal(name + "m_sound", 0) == 0) ? false : true;
            r.m_text = GetValS(name + "m_text", "");
        }
    }
}
