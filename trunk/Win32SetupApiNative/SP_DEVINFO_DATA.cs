//-----------------------------------------------------------------------
// <copyright file="SP_DEVINFO_DATA.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupApiNative
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>
   /// The struct that contains the device info retrieved from the win32 APIs.
   /// </summary>
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
   public struct SP_DEVINFO_DATA
   {
      /// <summary>
      /// The struct size
      /// </summary>
      public int Size;

      /// <summary>
      /// The device class GUID
      /// </summary>
      public Guid DeviceClassGuid;

      /// <summary>
      /// The device instance
      /// </summary>
      public IntPtr DeviceInstance;

      /// <summary>
      /// Reserved
      /// </summary>
      public int Reserved;
   }
}
