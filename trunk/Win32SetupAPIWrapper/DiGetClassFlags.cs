//-----------------------------------------------------------------------
// <copyright file="DiGetClassFlags.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupAPIWrapper
{
   using System;

   /// <summary>
   /// Filter values for output of the DiGetClassDevs function.
   /// </summary>
   [Flags]
   internal enum DIGCF : uint
   {
      /// <summary>
      /// The DiGetClassDevs default value
      /// </summary>
      DIGCF_DEFAULT = 0x00000001,  // only valid with DIGCF_DEVICEINTERFACE

      /// <summary>
      /// The DiGetClassDevs "present devices" value
      /// </summary>
      DIGCF_PRESENT = 0x00000002,

      /// <summary>
      /// The DiGetClassDevs "all classes" value
      /// </summary>
      DIGCF_ALLCLASSES = 0x00000004,

      /// <summary>
      /// The DiGetClassDevs "profile" value
      /// </summary>
      DIGCF_PROFILE = 0x00000008,

      /// <summary>
      /// The DiGetClassDevs "device interface" value
      /// </summary>
      DIGCF_DEVICEINTERFACE = 0x00000010,
   }
}
