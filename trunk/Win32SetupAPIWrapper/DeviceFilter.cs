//-----------------------------------------------------------------------
// <copyright file="DeviceFilter.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupAPIWrapper
{
   /// <summary>
   /// Enum that specifies a filter that can be used to
   /// filter the output when retrieving devices.
   /// </summary>
   public enum DeviceFilter
   {
      /// <summary>
      /// All devices
      /// </summary>
      AllDevices,

      /// <summary>
      /// All present devices
      /// </summary>
      PresentDevices,

      /// <summary>
      /// All PCI devices
      /// </summary>
      AllPciDevices,

      /// <summary>
      /// The present pci devices
      /// </summary>
      PresentPciDevices
   }
}
