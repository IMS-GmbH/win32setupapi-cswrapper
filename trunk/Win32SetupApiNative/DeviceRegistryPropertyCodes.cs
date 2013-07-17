//-----------------------------------------------------------------------
// <copyright file="DeviceRegistryPropertyCodes.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupApiNative
{
   /// <summary>
   /// Device registry property codes
   /// </summary>
   public enum SPDRP : int
   {
      /// <summary>
      /// Device Description (R/W)
      /// </summary>
      SPDRP_DEVICEDESC = 0x00000000,

      /// <summary>
      /// Hardware ID (R/W)
      /// </summary>
      SPDRP_HARDWAREID = 0x00000001,

      /// <summary>
      /// Compatible IDs (R/W)
      /// </summary>
      SPDRP_COMPATIBLEIDS = 0x00000002,

      /// <summary>
      /// [unused]
      /// </summary>
      SPDRP_UNUSED0 = 0x00000003,

      /// <summary>
      /// Service (R/W)
      /// </summary>
      SPDRP_SERVICE = 0x00000004,

      /// <summary>
      /// [unused]
      /// </summary>
      SPDRP_UNUSED1 = 0x00000005,

      /// <summary>
      /// [unused]
      /// </summary>
      SPDRP_UNUSED2 = 0x00000006,

      /// <summary>
      /// Class (R) (Tied to ClassGUID)
      /// </summary>
      SPDRP_CLASS = 0x00000007,

      /// <summary>
      /// ClassGUID (R/W)
      /// </summary>
      SPDRP_CLASSGUID = 0x00000008,

      /// <summary>
      /// Driver (R/W)
      /// </summary>
      SPDRP_DRIVER = 0x00000009,

      /// <summary>
      /// Configuration Flags (R/W)
      /// </summary>
      SPDRP_CONFIGFLAGS = 0x0000000A,

      /// <summary>
      /// Manufacturer Name (R/W)
      /// </summary>
      SPDRP_MFG = 0x0000000B,

      /// <summary>
      /// Friendly Name (R/W)
      /// </summary>
      SPDRP_FRIENDLYNAME = 0x0000000C,

      /// <summary>
      /// Location Information (R/W)
      /// </summary>
      SPDRP_LOCATION_INFORMATION = 0x0000000D,

      /// <summary>
      /// Physical Device Object Name (R)
      /// </summary>
      SPDRP_PHYSICAL_DEVICE_OBJECT_NAME = 0x0000000E,

      /// <summary>
      /// Capabilities (R)
      /// </summary>
      SPDRP_CAPABILITIES = 0x0000000F,

      /// <summary>
      /// UI Number (R)
      /// </summary>
      SPDRP_UI_NUMBER = 0x00000010,

      /// <summary>
      /// Upper Filters (R/W)
      /// </summary>
      SPDRP_UPPERFILTERS = 0x00000011,

      /// <summary>
      /// Lower Filters (R/W)
      /// </summary>
      SPDRP_LOWERFILTERS = 0x00000012,

      /// <summary>
      /// Bus Type GUID (R)
      /// </summary>
      SPDRP_BUSTYPEGUID = 0x00000013,

      /// <summary>
      /// Legacy Bus Type (R)
      /// </summary>
      SPDRP_LEGACYBUSTYPE = 0x00000014,

      /// <summary>
      /// Bus Number (R)
      /// </summary>
      SPDRP_BUSNUMBER = 0x00000015,

      /// <summary>
      /// Enumerator Name (R)
      /// </summary>
      SPDRP_ENUMERATOR_NAME = 0x00000016,

      /// <summary>
      /// Security (R/W, binary format)
      /// </summary>
      SPDRP_SECURITY = 0x00000017,

      /// <summary>
      /// Security (W, SDS format)
      /// </summary>
      SPDRP_SECURITY_SDS = 0x00000018,

      /// <summary>
      /// Device Type (R/W)
      /// </summary>
      SPDRP_DEVTYPE = 0x00000019,

      /// <summary>
      /// Device is exclusive-access (R/W)
      /// </summary>
      SPDRP_EXCLUSIVE = 0x0000001A,

      /// <summary>
      /// Device Characteristics (R/W)
      /// </summary>
      SPDRP_CHARACTERISTICS = 0x0000001B,

      /// <summary>
      /// Device Address (R)
      /// </summary>
      SPDRP_ADDRESS = 0x0000001C,

      /// <summary>
      /// UI Number Desc Format (R/W)
      /// </summary>
      SPDRP_UI_NUMBER_DESC_FORMAT = 0X0000001D,

      /// <summary>
      /// Device Power Data (R)
      /// </summary>
      SPDRP_DEVICE_POWER_DATA = 0x0000001E,

      /// <summary>
      /// Removal Policy (R)
      /// </summary>
      SPDRP_REMOVAL_POLICY = 0x0000001F,

      /// <summary>
      /// Hardware Removal Policy (R)
      /// </summary>
      SPDRP_REMOVAL_POLICY_HW_DEFAULT = 0x00000020,

      /// <summary>
      /// Removal Policy Override (RW)
      /// </summary>
      SPDRP_REMOVAL_POLICY_OVERRIDE = 0x00000021,

      /// <summary>
      /// Device Install State (R)
      /// </summary>
      SPDRP_INSTALL_STATE = 0x00000022,

      /// <summary>
      /// Device Location Paths (R)
      /// </summary>
      SPDRP_LOCATION_PATHS = 0x00000023,

      /// <summary>
      /// Base ContainerID (R)
      /// </summary>
      SPDRP_BASE_CONTAINERID = 0x00000024
   }
}
