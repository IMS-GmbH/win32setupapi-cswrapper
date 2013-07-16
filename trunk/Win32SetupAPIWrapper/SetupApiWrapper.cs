//-----------------------------------------------------------------------
// <copyright file="SetupApiWrapper.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupAPIWrapper
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Runtime.InteropServices;
   using System.Text;

   /// <summary>
   /// A layer on top of the win32 setupapi that allows for
   /// easier usage in a less unmanaged kind of way.
   /// </summary>
   public static class SetupApiWrapper
   {
      #region Public Methods
      /// <summary>
      /// The internal reboot required representation.
      /// </summary>
      private static Boolean rebootRequired = false;

      /// <summary>
      /// Gets a Boolean flag that specifies whether the 
      /// taken actions require a system reboot.
      /// </summary>
      /// <value>
      /// The reboot-required-flag.
      /// </value>
      public static Boolean RebootRequired
      {
         get
         {
            return rebootRequired;
         }

         internal set
         {
            rebootRequired = value;
         }
      }

      /// <summary>
      /// Gets a list of devices that match the filter criteria.
      /// </summary>
      /// <param name="deviceFilter">The device filter.</param>
      /// <returns>An array of DeviceInformation objects.</returns>
      public static DeviceInformation[] GetDevices(DeviceFilter deviceFilter)
      {
         List<DeviceInformation> result = new List<DeviceInformation>();

         Guid emptyGuid = Guid.Empty;

         int filter;
         if (deviceFilter == DeviceFilter.AllDevices || deviceFilter == DeviceFilter.AllPciDevices)
         {
            filter = (int)DIGCF.DIGCF_ALLCLASSES;
         }
         else
         {
            filter = (int)DIGCF.DIGCF_ALLCLASSES | (int)DIGCF.DIGCF_PRESENT;
         }

         IntPtr deviceInfoSet =
            Win32SetupApi.SetupDiGetClassDevs(
               ref emptyGuid,
               IntPtr.Zero,
               IntPtr.Zero,
               filter);

         SP_DEVINFO_DATA deviceInfoData =
            new SP_DEVINFO_DATA();
         deviceInfoData.Size = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

         for (int i = 0; Win32SetupApi.SetupDiEnumDeviceInfo(deviceInfoSet, i, ref deviceInfoData); i++)
         {
            DeviceInformation deviceInformation =
               GetDeviceInformation(deviceInfoSet, deviceInfoData);

            switch (deviceFilter)
            {
               case DeviceFilter.PresentPciDevices:
               case DeviceFilter.AllPciDevices:
                  if (!deviceInformation.LocationInfo
                        .ToLowerInvariant().Contains("pci"))
                  {
                     continue; // relates to the for-loop
                  }
                  else
                  {
                     break; // relates to the switch
                  }

               case DeviceFilter.AllDevices:
               default:
                  break;
            }

            result.Add(deviceInformation);
         }

         Win32SetupApi.SetupDiDestroyDeviceInfoList(deviceInfoSet);

         return result.ToArray();
      }

      /// <summary>
      /// Gets the compatible drivers for a specified device.
      /// </summary>
      /// <param name="deviceInfo">The device info.</param>
      /// <returns></returns>
      public static DriverInstance[] GetCompatibleDrivers(DeviceInformation deviceInfo)
      {
         IntPtr hDevInfo;
         SP_DEVINFO_DATA devInfoData;

         GetDeviceHandleFromMatch(deviceInfo, out hDevInfo, out devInfoData);

         return GetMatchingDrivers(hDevInfo, devInfoData);
      }

      /// <summary>
      /// Applies the specified driver to the specified device.
      /// </summary>
      /// <param name="deviceInfo">The device info.</param>
      /// <param name="driverInstance">The driver instance.</param>
      public static void ApplyDriverToDevice(
         DeviceInformation deviceInfo,
         DriverInstance driverInstance)
      {
         IntPtr deviceInfoSet;
         SP_DEVINFO_DATA devInfoData;
         GetDeviceHandleFromMatch(deviceInfo, out deviceInfoSet, out devInfoData);

         SP_DRVINFO_DATA drvInfoData;
         GetDriverHandleFromMatch(driverInstance, deviceInfoSet, devInfoData, out drvInfoData);

         try
         {
            bool needReboot = false;
            Win32SetupApi.DiInstallDevice(
               IntPtr.Zero,
               deviceInfoSet,
               ref devInfoData,
               ref drvInfoData,
               0,
               ref needReboot);

            RebootRequired = needReboot;
         }
         catch (Exception)
         {
            throw;
         }
      }

      /// <summary>
      /// Uninstalls the device.
      /// </summary>
      /// <param name="deviceInfo">The device info.</param>
      public static void UninstallDevice(DeviceInformation deviceInfo)
      {
         IntPtr hdevInfo;
         SP_DEVINFO_DATA devInfoData;

         GetDeviceHandleFromMatch(deviceInfo, out hdevInfo, out devInfoData);

         Boolean needReboot;
         Win32SetupApi.DiUninstallDevice(
            IntPtr.Zero,
            hdevInfo,
            devInfoData,
            0,
            out needReboot);

         RebootRequired = needReboot;
      }

      /// <summary>
      /// Restarts the system.
      /// </summary>
      public static void RestartSystem()
      {
         RestartSystem(0);
      }

      /// <summary>
      /// Restarts the system after a given delay.
      /// </summary>
      /// <param name="delay">The delay in seconds.</param>
      public static void RestartSystem(int delay)
      {
         Process.Start("shutdown.exe", "-r -t " + delay);
      }
      #endregion

      #region Private Methods
      /// <summary>
      /// Gets the device information from the system
      /// using the specified pointers.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <returns>The device information.</returns>
      private static DeviceInformation GetDeviceInformation(
         IntPtr deviceInfoSet,
         SP_DEVINFO_DATA deviceInfoData)
      {
         DeviceInformation deviceInformation = new DeviceInformation();

         deviceInformation.Address =
            GetDeviceAddress(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.BusNumber =
            GetBusNumber(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.BusTypeGUID =
            GetBusType(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.Capabilities =
            GetCapabilities(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.Characteristics =
            GetCharacteristics(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.CompatibleIDs =
            GetCompatibleIDs(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.ConfigFlags =
            GetConfigFlags(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.ContainerID =
            GetBaseContainerID(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.Description =
            GetDescription(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.DeviceClassGUID =
            GetDeviceClassGuid(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.DeviceClassName =
            GetClassName(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.DevicePowerData =
            GetDevicePowerData(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.DeviceType =
            GetDeviceType(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.DriverIdent =
            GetDriverInfo(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.EnumeratorName =
            GetEnumeratorName(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.FriendlyName =
            GetFriendlyName(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.HardwareID =
            GetHardwareID(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.InstallState =
            GetInstallState(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.IsExclusiveAccess =
            GetIsExclusiveAccess(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.LegacyBusType =
            GetLegacyBusType(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.LocationInfo =
            GetLocationInfo(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.LocationPaths =
            GetDeviceLocationPaths(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.LowerFilters =
            GetLowerFilters(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.ManufacturerName =
            GetDeviceManufacturerName(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.PhysicalDeviceObjectName =
            GetPhysicalDeviceObjectName(
               deviceInfoSet,
               deviceInfoData);

         deviceInformation.RemovalPolicy =
            GetRemovalPolicy(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.RemovalPolicyHwDefault =
            GetRemovalPolicyHWDefault(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.RemovalPolicyOverride =
            GetRemovalPolicyOverride(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.Security =
            GetSecurityBinary(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.SecuritySDS =
            GetSecuritySds(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.ServiceInfo =
            GetServiceInfo(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.UINumber =
            GetUiNumber(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.UiNumberDescFormat =
            GetUINoDescFormat(
            deviceInfoSet,
            deviceInfoData);

         deviceInformation.UpperFilters =
            GetUpperFilters(
            deviceInfoSet,
            deviceInfoData);

         return deviceInformation;
      }

      /// <summary>
      /// Gets the matching drivers for a device.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>An array of matching device drivers.</returns>
      private static DriverInstance[] GetMatchingDrivers(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         List<DriverInstance> result = null;

         SP_DRVINFO_DATA drvInfoData = new SP_DRVINFO_DATA();
         drvInfoData.Size = Marshal.SizeOf(typeof(SP_DRVINFO_DATA));
         if (Win32SetupApi.SetupDiBuildDriverInfoList(devInfoSet, ref devInfoData, Constants.SPDIT_COMPATDRIVER))
         {
            result = new List<DriverInstance>();

            for (int i = 0; Win32SetupApi.SetupDiEnumDriverInfo(devInfoSet, ref devInfoData, Constants.SPDIT_COMPATDRIVER, i, ref drvInfoData); i++)
            {
               result.Add(new DriverInstance
               {
                  Description = drvInfoData.Description,
                  Version = drvInfoData.DriverVersion,
                  ManufacturerName = drvInfoData.MfgName,
                  ProviderName = drvInfoData.ProviderName,
                  DriverDate = drvInfoData.DriverDate.ToDateTime()
               });
            }
         }

         return result != null ? result.ToArray() : new DriverInstance[] { };
      }

      /// <summary>
      /// Gets a device handle from matching.
      /// </summary>
      /// <param name="deviceInformation">The device information.</param>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      private static void GetDeviceHandleFromMatch(
         DeviceInformation deviceInformation,
         out IntPtr deviceInfoSet,
         out SP_DEVINFO_DATA deviceInfoData)
      {
         Guid emptyGuid = Guid.Empty;
         deviceInfoSet = Win32SetupApi.SetupDiGetClassDevs(
            ref emptyGuid,
            IntPtr.Zero,
            IntPtr.Zero,
            (int)DIGCF.DIGCF_PRESENT | (int)DIGCF.DIGCF_ALLCLASSES);

         deviceInfoData = new SP_DEVINFO_DATA();
         deviceInfoData.Size = Marshal.SizeOf(typeof(SP_DEVINFO_DATA));

         for (int i = 0; Win32SetupApi.SetupDiEnumDeviceInfo(deviceInfoSet, i, ref deviceInfoData); i++)
         {
            String matchSubjectDesc = GetDescription(deviceInfoSet, deviceInfoData);
            String matchSubjectPhysDevName = GetPhysicalDeviceObjectName(deviceInfoSet, deviceInfoData);
            int matchSubjectBus = GetBusNumber(deviceInfoSet, deviceInfoData);

            if (
                  deviceInformation.Description.Equals(matchSubjectDesc) 
               && deviceInformation.BusNumber.Equals(matchSubjectBus)
               && deviceInformation.PhysicalDeviceObjectName.Equals(matchSubjectPhysDevName))
            {
               return;
            }
         }

         deviceInfoData = new SP_DEVINFO_DATA();
         deviceInfoSet = IntPtr.Zero;
      }

      /// <summary>
      /// Gets a driver handle from matching.
      /// </summary>
      /// <param name="driverInstance">The driver instance.</param>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="drvInfoData">The DRV info data.</param>
      private static void GetDriverHandleFromMatch(
         DriverInstance driverInstance,
         IntPtr deviceInfoSet,
         SP_DEVINFO_DATA deviceInfoData,
         out SP_DRVINFO_DATA drvInfoData)
      {
         drvInfoData = new SP_DRVINFO_DATA();
         drvInfoData.Size = Marshal.SizeOf(typeof(SP_DRVINFO_DATA));
         if (Win32SetupApi.SetupDiBuildDriverInfoList(deviceInfoSet, ref deviceInfoData, Constants.SPDIT_COMPATDRIVER))
         {
            for (int i = 0; Win32SetupApi.SetupDiEnumDriverInfo(deviceInfoSet, ref deviceInfoData, Constants.SPDIT_COMPATDRIVER, i, ref drvInfoData); i++)
            {
               if (driverInstance.Description.Equals(drvInfoData.Description))
               {
                  return;
               }
            }
         }

         drvInfoData = new SP_DRVINFO_DATA();
         return;
      }
      #endregion

      #region Hardware Information Getter
      /// <summary>
      /// Gets the description.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The Description.</returns>
      private static String GetDescription(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
                  GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_DEVICEDESC);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the bus number.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The bus number.</returns>
      private static Int32 GetBusNumber(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_BUSNUMBER);

            if (propertyBuffer.Length > 0)
            {
               return (Int32)(propertyBuffer.ToString().ToCharArray()[0]);
            }
            else
            {
               return -1;
            }
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the hardware ID.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The hardware ID.</returns>
      private static String GetHardwareID(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_HARDWAREID);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the compatible IDs.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The compatible IDs.</returns>
      private static String GetCompatibleIDs(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_COMPATIBLEIDS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the service info.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The service info.</returns>
      private static String GetServiceInfo(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_SERVICE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the name of the device's class.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The classname.</returns>
      private static String GetClassName(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_CLASS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the config flags.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The config flags.</returns>
      private static String GetConfigFlags(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_CONFIGFLAGS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the friendly name.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The friendly name.</returns>
      private static String GetFriendlyName(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_FRIENDLYNAME);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the name of the physical device object.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The name of the physical device object.</returns>
      private static String GetPhysicalDeviceObjectName(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_PHYSICAL_DEVICE_OBJECT_NAME);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the capabilities.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The capabilities.</returns>
      private static String GetCapabilities(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_CAPABILITIES);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the UI number.
      /// </summary>
      /// <param name="devInfoSet">The dev info set.</param>
      /// <param name="devInfoData">The dev info data.</param>
      /// <returns>The UI number.</returns>
      private static String GetUiNumber(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_UI_NUMBER);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the upper filters.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The upper filters.</returns>
      private static String GetUpperFilters(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_UPPERFILTERS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the lower filters.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The lower filters.</returns>
      private static String GetLowerFilters(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_LOWERFILTERS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the legacy bustype.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The legacy bus type,</returns>
      private static String GetLegacyBusType(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_LEGACYBUSTYPE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the name of the enumerator.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The enumerator name.</returns>
      private static String GetEnumeratorName(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_ENUMERATOR_NAME);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the security in binary form.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The security in binary form.</returns>
      private static String GetSecurityBinary(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_SECURITY);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the security in SDS form.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The security in sds form.</returns>
      private static String GetSecuritySds(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_SECURITY_SDS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the type of the device.
      /// </summary>
      /// <param name="devInfoSet">The dev info set.</param>
      /// <param name="devInfoData">The dev info data.</param>
      /// <returns>The device type.</returns>
      private static String GetDeviceType(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_DEVTYPE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the is exclusive access.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>
      /// A value that indicates whether the 
      /// device access is exclusive.
      /// </returns>
      private static String GetIsExclusiveAccess(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_EXCLUSIVE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the characteristics.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The device's characteristics.</returns>
      private static String GetCharacteristics(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_CHARACTERISTICS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the device address.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The device address.</returns>
      private static String GetDeviceAddress(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_ADDRESS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the UI number in desc format.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The UI no. in desc format.</returns>
      private static String GetUINoDescFormat(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_UI_NUMBER_DESC_FORMAT);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the device power data.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The device power data.</returns>
      private static String GetDevicePowerData(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_DEVICE_POWER_DATA);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the removal policy.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The removal policy.</returns>
      private static String GetRemovalPolicy(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_REMOVAL_POLICY);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the removal policy HW default.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The removal policy HW default.</returns>
      private static String GetRemovalPolicyHWDefault(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_REMOVAL_POLICY_HW_DEFAULT);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the removal policy override.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The removal policy override.</returns>
      private static String GetRemovalPolicyOverride(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_REMOVAL_POLICY_OVERRIDE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the state of the device installation.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The device installation state.</returns>
      private static String GetInstallState(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_INSTALL_STATE);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the base container ID.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The base container ID.</returns>
      private static String GetBaseContainerID(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_BASE_CONTAINERID);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the device class GUID.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The device class GUID.</returns>
      private static String GetDeviceClassGuid(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_CLASSGUID);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the type of the bus.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The bus type information.</returns>
      private static String GetBusType(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_BUSTYPEGUID);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the driver info.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The driver info.</returns>
      private static String GetDriverInfo(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_DRIVER);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the device location paths.
      /// </summary>
      /// <param name="devInfoSet">The dev info set.</param>
      /// <param name="devInfoData">The dev info data.</param>
      /// <returns>The device location paths.</returns>
      private static String GetDeviceLocationPaths(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
               GetProperty(
                  devInfoSet,
                  devInfoData,
                  SPDRP.SPDRP_LOCATION_PATHS);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the location info.
      /// </summary>
      /// <param name="devInfoSet">The device info set.</param>
      /// <param name="devInfoData">The device info data.</param>
      /// <returns>The location info.</returns>
      private static String GetLocationInfo(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
                  GetProperty(
                     devInfoSet,
                     devInfoData,
                     SPDRP.SPDRP_LOCATION_INFORMATION);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets the name of the device manufacturer.
      /// </summary>
      /// <param name="devInfoSet">The dev info set.</param>
      /// <param name="devInfoData">The dev info data.</param>
      /// <returns>The name of the device manufacturer.</returns>
      private static String GetDeviceManufacturerName(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData)
      {
         try
         {
            StringBuilder propertyBuffer =
                  GetProperty(
                     devInfoSet,
                     devInfoData,
                     SPDRP.SPDRP_MFG);

            return propertyBuffer.ToString();
         }
         catch
         {
            throw;
         }
      }

      /// <summary>
      /// Gets a device's registry property. Helper class that
      /// encapsulates the core functionality of the other get
      /// registry property functions.
      /// </summary>
      /// <param name="devInfoSet">The dev info set.</param>
      /// <param name="devInfoData">The dev info data.</param>
      /// <param name="propertyIndex">Index of the property.</param>
      /// <returns>The property buffer.</returns>
      private static StringBuilder GetProperty(
         IntPtr devInfoSet,
         SP_DEVINFO_DATA devInfoData,
         SPDRP propertyIndex)
      {
         Int32 requiredSize = 0;
         Int32 propertyRegDataType = 0;
         StringBuilder propertyBuffer;

         Win32SetupApi.SetupDiGetDeviceRegistryProperty(
            devInfoSet,
            ref devInfoData,
            (int)propertyIndex,
            ref propertyRegDataType,
            null,
            0,
            ref requiredSize);

         propertyBuffer = new StringBuilder(requiredSize);

         propertyRegDataType = 0;
         Win32SetupApi.SetupDiGetDeviceRegistryProperty(
            devInfoSet,
            ref devInfoData,
            (int)propertyIndex,
            ref propertyRegDataType,
            propertyBuffer,
            propertyBuffer.Capacity,
            ref requiredSize);

         return propertyBuffer;
      }
      #endregion
   }
}
