using LedLib2;
using LedLib2.IT8297;
using LedLib2.LED_V4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace Aorus.X570.Pro.RgbController
{
    public class Collection_8297
    {
        public string mCurrentPath = "";
        public List<MCU_8297> lstM97 = new List<MCU_8297>();
        private string mapXml = "divsmap.xml";
        private List<Collection_8297.DivsMapping> lstMapp = new List<Collection_8297.DivsMapping>();
        internal const uint ERROR_SUCCESS = 0;
        internal const uint ERROR_INVALID_OPERATION = 4317;
        private int cal_mcu_idx;

        public int McuCount { get; private set; }

        public uint cLedId { get; private set; }

        public MBIdentify cMBId { get; private set; }

        public Collection_8297()
        {
            this.McuCount = 0;
            this.cLedId = 0U;
            this.cMBId = MBIdentify.UnknownMB;
            this.mapXml = this.mapXml.Insert(0, this.mCurrentPath);
        }

        public void Add(MCU_8297 m97)
        {
            if (this.cLedId == 0U)
                this.cLedId = m97.m_LedId;
            if (this.cMBId == MBIdentify.UnknownMB)
                this.cMBId = m97.MbId2;
            this.lstM97.Add(m97);
            this.McuCount = this.lstM97.Count;
        }

        public void Reorganize()
        {
            List<MCU_8297> source = new List<MCU_8297>();
            int num = 0;
            do
            {
                foreach (MCU_8297 mcu8297 in this.lstM97)
                {
                    if (mcu8297.McuIndex == num)
                    {
                        source.Add(mcu8297);
                        ++num;
                        break;
                    }
                }
            }
            while (source.Count < this.lstM97.Count);
            this.lstM97 = source.ToList<MCU_8297>();
        }

        public bool ParseXml()
        {
            bool flag = true;

            this.mapXml = this.mapXml.Insert(0, this.mCurrentPath);

            if (!File.Exists(this.mapXml))
                return false;

            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                xmlDocument.Load(this.mapXml);
                foreach (XmlElement childNode1 in xmlDocument.SelectSingleNode("html").ChildNodes)
                {
                    if ((MBIdentify)int.Parse(childNode1.GetAttribute("id")) == this.cMBId)
                    {
                        foreach (XmlElement childNode2 in childNode1.ChildNodes)
                        {
                            if (int.Parse(childNode2.GetAttribute("id"), NumberStyles.HexNumber) == (int)this.cLedId)
                            {
                                foreach (XmlElement childNode3 in childNode2.ChildNodes)
                                    this.lstMapp.Add(new Collection_8297.DivsMapping(int.Parse(childNode3.GetAttribute("idx")), int.Parse(childNode3.GetAttribute("mcu"))));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                flag = false;
            }
            return flag;
        }

        public void ClearParameter(int ifrom, int McuIndex = -1)
        {
            if (!this.mcu_connected())
                return;
            if (McuIndex == -1)
            {
                foreach (MCU_8297 mcu8297 in this.lstM97)
                    mcu8297.ClearIT8297Parameter(ifrom);
            }
            else
                this.lstM97[McuIndex].ClearIT8297Parameter(ifrom);
        }

        public uint SetLedEffect(
          LMode_8297 mode,
          int iColor,
          int speedLv,
          int BrightnessLv,
          int McuIndex = -1,
          bool bApplyNow = true)
        {
            uint num1 = 0;
            if (!this.mcu_connected())
                return 4317;
            if (McuIndex == -1)
            {
                if (mode == LMode_8297.Wave9 || mode == LMode_8297.Wave10 || (mode == LMode_8297.Wave11 || mode == LMode_8297.Wave12))
                {
                    int num2 = (int)this.lstM97[1].SetLedEffect(mode, iColor, speedLv, BrightnessLv, -1, true);
                    if (mode == LMode_8297.Wave9 || mode == LMode_8297.Wave11)
                    {
                        int ccycle = (int)this.lstM97[0].SetExternalStripToCCycle();
                    }
                }
                else
                {
                    foreach (MCU_8297 mcu8297 in this.lstM97)
                        num1 = mcu8297.SetLedEffect(mode, iColor, speedLv, BrightnessLv, -1, true);
                }
            }
            else if (McuIndex >= this.McuCount)
                return 4317;
            return num1;
        }

        public uint TurnOffLed(int iDivs = -1)
        {
            uint num = 0;
            if (this.McuCount < 1)
                return 4317;
            if (iDivs == -1)
            {
                foreach (MCU_8297 mcu8297 in this.lstM97)
                    num = mcu8297.TurnOffLed(-1);
            }
            else
                num = this.lstM97[this.GetMcuIndex(iDivs)].TurnOffLed(iDivs);
            return num;
        }

        public void StopDigitalLedEffect()
        {
            if (!this.mcu_connected())
                return;
            foreach (MCU_8297 mcu8297 in this.lstM97)
                mcu8297.StopEffectThread();
        }

        public void disable_DLED_direct_control(int iStrip)
        {
            if (!this.mcu_connected())
                return;
            foreach (MCU_8297 mcu8297 in this.lstM97)
                mcu8297.dstrip_direct_control_end(iStrip);
        }

        public void Start_SWaveMode1(int Direction, int Speed_Lv, int Brightness_Lv, int McuIndex = -1)
        {
            if (!this.mcu_connected() || McuIndex != -1)
                return;
            foreach (MCU_8297 mcu8297 in this.lstM97)
                mcu8297.Start_SWaveMode1(Direction, Speed_Lv, Brightness_Lv);
        }

        public void Run_Random(int iDivis, Color c)
        {
            int num = (int)this.lstM97[this.GetMcuIndex(iDivis)].Run_Random(iDivis, c);
        }

        public void SaveSetting()
        {
            foreach (MCU_8297 mcu8297 in this.lstM97)
            {
                int num = (int)mcu8297.SaveSetting();
            }
        }

        public uint AdvancedSetMode(
          int iDivision,
          LMode_8297 mode,
          List<Color_Sceenes_class> lstCSData)
        {
            return this.lstM97[this.GetMcuIndex(iDivision)].AdvancedSetMode(iDivision, mode, lstCSData);
        }

        public uint Apply(int _ApplyBit)
        {
            uint num = 0;
            foreach (MCU_8297 mcu8297 in this.lstM97)
                num = mcu8297.Apply(_ApplyBit);
            return num;
        }

        public void SetCalibrationDivis(int divis)
        {
            this.cal_mcu_idx = this.GetMcuIndex(divis);
            this.lstM97[this.cal_mcu_idx].SetCalibrationDivis(divis);
        }

        public void SetStripPinDefine(int _func)
        {
            int num = (int)this.lstM97[this.cal_mcu_idx].SetStripPinDefine(_func);
        }

        public void CalibrateShowColor(Color c)
        {
            int num = (int)this.lstM97[this.cal_mcu_idx].CalibrateShowColor(c);
        }

        public void GetCalibrationValue(int step, int see_color)
        {
            int calibrationValue = (int)this.lstM97[this.cal_mcu_idx].GetCalibrationValue(step, see_color);
        }

        public void Calibrate_Done()
        {
            int num = (int)this.lstM97[this.cal_mcu_idx].SaveSetting();
        }

        private bool mcu_connected()
        {
            return this.McuCount >= 1;
        }

        private int GetMcuIndex(int Divis)
        {
            int num = 0;
            if (this.lstMapp.Count == 0)
                return 0;
            foreach (DivsMapping divsMapping in this.lstMapp)
            {
                if (divsMapping.divs == Divis)
                    return divsMapping.mcuIdx;
            }
            return num;
        }

        internal struct DivsMapping
        {
            public int divs;
            public int mcuIdx;

            public DivsMapping(int _divs, int _mcuidx)
            {
                this.divs = _divs;
                this.mcuIdx = _mcuidx;
            }
        }
    }
}
