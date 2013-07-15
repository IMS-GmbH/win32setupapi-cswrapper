//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupAPIWrapper
{
   using System;
   using System.Collections.Generic;

   /// <summary>
   /// Static class that holds constants that are not sorted
   /// into an enumeration.
   /// </summary>
   internal static class Constants
   {
      /// <summary>
      /// The CR code constant that specifies "success"
      /// </summary>
      public const int CR_SUCCESS = (0x00000000);

      /// <summary>
      /// The win32 system error code for "no error"
      /// </summary>
      public const int ERROR_NO_ERROR = 0;

      /// <summary>
      /// The win32 system error code for "no more items"
      /// </summary>
      public const int ERROR_NO_MORE_ITEMS = 259;

      /// <summary>
      /// The SPDI code constant that specifies "no driver"
      /// </summary>
      public const int SPDIT_NODRIVER = (0x00000000);

      /// <summary>
      /// The SPDI code constant that specifies "class related drivers"
      /// </summary>
      public const int SPDIT_CLASSDRIVER = (0x00000001);

      /// <summary>
      /// The SPDI code constant that specifies "compatible drivers"
      /// </summary>
      public const int SPDIT_COMPATDRIVER = (0x00000002);
   }
}
