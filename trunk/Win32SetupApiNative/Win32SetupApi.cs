//-----------------------------------------------------------------------
// <copyright file="Win32SetupApi.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupApiNative
{
   using System;
   using System.Runtime.InteropServices;
   using System.Text;

   /// <summary>
   /// P/Invokes for a subset of functions from the setupapi.h
   /// and newdev.h Windows APIs that allow reading hardware properties 
   /// and switching drivers on devices.
   /// </summary>
   public static class Win32SetupApi
   {
      /// <summary>
      /// Destroys a device info list created with SetupDiEnumDeviceInfo.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern Boolean SetupDiDestroyDeviceInfoList(
         IntPtr deviceInfoSet);

      /// <summary>
      /// Gets the class devs.
      /// </summary>
      /// <param name="classGuid">The class GUID.</param>
      /// <param name="enumerator">The enumerator.</param>
      /// <param name="hwndParent">The parent window.</param>
      /// <param name="flags">The flags.</param>
      /// <returns>Pointer to the class devs.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern IntPtr SetupDiGetClassDevs(
         ref Guid classGuid,
         IntPtr enumerator,
         IntPtr hwndParent,
         int flags);

      /// <summary>
      /// Creates a device info list.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="memberIndex">Index of the member.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetupDiEnumDeviceInfo(
         IntPtr deviceInfoSet,
         int memberIndex,
         ref SP_DEVINFO_DATA deviceInfoData);

      /// <summary>
      /// Gets the specified device property from the registry.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="property">The index of the desired property.</param>
      /// <param name="propertyRegDataType">Type of the property reg data.</param>
      /// <param name="propertyBuffer">The property buffer.</param>
      /// <param name="propertyBufferSize">Size of the property buffer.</param>
      /// <param name="requiredSize">Size the property buffer is required to have.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetupDiGetDeviceRegistryProperty(
         IntPtr deviceInfoSet,
         ref SP_DEVINFO_DATA deviceInfoData,
         int property, 
         ref int propertyRegDataType,
         StringBuilder propertyBuffer,
         int propertyBufferSize,
         ref int requiredSize);

      /// <summary>
      /// Destroys a driver info list.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="driverType">Type of the driver.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetupDiDestroyDriverInfoList(
         IntPtr deviceInfoSet,
         ref SP_DEVINFO_DATA deviceInfoData,
         int driverType);

      /// <summary>
      /// Builds a driver info list.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="driverType">Type of the driver.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetupDiBuildDriverInfoList(
         IntPtr deviceInfoSet,
         ref SP_DEVINFO_DATA deviceInfoData,
         int driverType);

      /// <summary>
      /// Enumerates driver info.
      /// </summary>
      /// <param name="deviceInfoSet">The device info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="driverType">Type of the driver.</param>
      /// <param name="memberIndex">Index of the member.</param>
      /// <param name="driverInfoData">The driver info data.</param>
      /// <returns>True on success.</returns>
      [DllImport("setupapi.dll", SetLastError = true, CharSet = CharSet.Unicode)]
      public static extern bool SetupDiEnumDriverInfo(
         IntPtr deviceInfoSet,
         ref SP_DEVINFO_DATA deviceInfoData,
         int driverType,
         int memberIndex,
         ref SP_DRVINFO_DATA driverInfoData);

      /// <summary>
      /// Uninstalls the specified device.
      /// </summary>
      /// <param name="hParent">The parent window.</param>
      /// <param name="lpInfoSet">The lp info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="flags">The flags.</param>
      /// <param name="needReboot">If set to <c>true</c> [need reboot].</param>
      /// <returns></returns>
      [DllImport("newdev.dll", SetLastError = true)]
      public static extern bool DiUninstallDevice(
         IntPtr hParent, 
         IntPtr lpInfoSet, 
         SP_DEVINFO_DATA deviceInfoData, 
         Int32 flags,
         out Boolean needReboot);

      /// <summary>
      /// Install the specified driver for the specified device.
      /// </summary>
      /// <param name="hParent">The parent window.</param>
      /// <param name="lpInfoSet">The lp info set.</param>
      /// <param name="deviceInfoData">The device info data.</param>
      /// <param name="driverInfoData">The driver info data.</param>
      /// <param name="flags">The flags.</param>
      /// <param name="needReboot">If set to <c>true</c> [need reboot].</param>
      /// <returns>True on success.</returns>
      [DllImport("newdev.dll", SetLastError = true)]
      public static extern bool DiInstallDevice(
         IntPtr hParent, 
         IntPtr lpInfoSet, 
         ref SP_DEVINFO_DATA deviceInfoData,
         ref SP_DRVINFO_DATA driverInfoData,
         UInt32 flags, 
         ref bool needReboot);
   }
}
