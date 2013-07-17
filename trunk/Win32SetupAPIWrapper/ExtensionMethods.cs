//-----------------------------------------------------------------------
// <copyright file="ExtensionMethods.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupApiWrapper
{
   using System;
   using System.Runtime.InteropServices.ComTypes;

   /// <summary>
   /// Static class that hosts extension methods used within
   /// the Win32SetupApiWrapper Project.
   /// </summary>
   internal static class ExtensionMethods
   {
      /// <summary>
      /// Extension method for ComTypes.FILETIME.
      /// Converts the filetime object into a DateTime object.
      /// </summary>
      /// <param name="filetime">The filetime.</param>
      /// <returns>The DateTime.</returns>
      internal static DateTime ToDateTime(this FILETIME filetime)
      {
         long highBits = filetime.dwHighDateTime;
         highBits = highBits << 32;

         return DateTime.FromFileTimeUtc(
            highBits + (long)(uint)filetime.dwLowDateTime);
      }
   }
}
