using Gigabyte.ULightingEffects.Devices;
using Gigabyte.ULightingEffects.Devices.Kingston.MemoryModule;
using LedLib2;
using LedLib2.IT8297;
using LedLib2.LED_V4;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Aorus.X570.Pro.RgbController
{
    public class Motherboard
    {
        private Motherboard()
        {
            if (InvkSMBCtrl.LibInitial() != 0)
                throw new DeviceInitializationException();

            ledId = InvkSMBCtrl.GetLEDId();
            identity = (MBIdentify)InvkSMBCtrl.GetMBId();
            ConnectMcu();
            LoadMemoryModules();
        }

        private static Lazy<Motherboard> instance = new Lazy<Motherboard>(() => new Motherboard());

        public static Motherboard Instance
        {
            get
            {
                return instance.Value;
            }
        }

        private Collection_8297 coll97 = new Collection_8297();
        private HID_ReportByteLengt[] MyReportLength = new HID_ReportByteLengt[256];
        private Connected_Devices[] cdevlist;
        private uint ledId;
        private MBIdentify identity;
        private KingstonMemoryDevice memoryDevice;

        private void ConnectMcu()
        {
            for (int index = 0; index < 3; ++index)
            {
                var deviceCount = ConnectToMcuNative(1165, 33431, 65417, 204);

                if (deviceCount >= 1)
                {
                    coll97.mCurrentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\";
                    foreach (Connected_Devices connectedDevices in cdevlist.Where(device => device.device == McuOnDevice.Motherboard))
                    {
                        coll97.Add(new MCU_8297(connectedDevices.devNum,
                                                MyReportLength[connectedDevices.devNum].FeatureLength,
                                                ledId,
                                                identity,
                                                coll97.mCurrentPath));
                    }
                    if (coll97.McuCount > 0)
                    {
                        coll97.ParseXml();
                        break;
                    }
                }
            }
        }

        private void LoadMemoryModules()
        {
            string spddumpXmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "SPD_Dump.XML");
            List<UleRamModuleInformation> destination = new List<UleRamModuleInformation>();

            MemoryHelper.GetMemorySpdInfo(destination, spddumpXmlPath);
            memoryDevice = new KingstonMemoryDevice(destination, UleMcuTypes.Mcu_Unknown, 0, false);
        }

        private int ConnectToMcuNative(ushort vid, ushort pid, ushort devUp, ushort report_id)
        {
            int length = InvkGHidApi.ConnectDevice(vid, pid, devUp, report_id, MyReportLength);
            if (length > 0)
            {
                cdevlist = new Connected_Devices[length];
                InvkGHidApi.GetConnectedList(vid, pid, cdevlist);
            }
            return length;
        }

        public void SetLedColor(Color color)
        {
            coll97.SetLedEffect(LMode_8297.Static, color.ToArgb(), 100, 100);
        }

        public void SetMemoryLedColor(Color color)
        {
            memoryDevice.SetLedEffect(UleLightingEffectModes.Still, color.ToArgb(), 100, -1);
        }
    }
}
