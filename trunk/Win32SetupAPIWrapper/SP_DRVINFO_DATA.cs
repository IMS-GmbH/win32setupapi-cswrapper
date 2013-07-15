//-----------------------------------------------------------------------
// <copyright file="SP_DRVINFO_DATA.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------
namespace Win32SetupAPIWrapper
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>
   /// The struct that contains the driver info retrieved from the win32 APIs.
   /// </summary>
   [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 4)]
   internal struct SP_DRVINFO_DATA
   {
      /// <summary>
      /// The size of the struct
      /// </summary>
      public int Size;

      /// <summary>
      /// The driver type
      /// </summary>
      public int DriverType;

      /// <summary>
      /// Reserved
      /// </summary>
      public UInt32 Reserved;

      /// <summary>
      /// The description
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string Description;

      /// <summary>
      /// The Manufacturer name
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string MfgName;

      /// <summary>
      /// The Provider name
      /// </summary>
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
      public string ProviderName;

      /// <summary>
      /// The driver date
      /// </summary>
      public System.Runtime.InteropServices.ComTypes.FILETIME DriverDate;

      /// <summary>
      /// The driver version
      /// </summary>
      public long DriverVersion;
   }
}
