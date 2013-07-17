//-----------------------------------------------------------------------
// <copyright file="DriverInstance.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupApiWrapper
{
   using System;

   /// <summary>
   /// Class that holds the driver information that 
   /// can be retrieved via the win32 setup apis.
   /// </summary>
   public class DriverInstance
   {
      /// <summary>
      /// The description
      /// </summary>
      private String description;

      /// <summary>
      /// The version
      /// </summary>
      private Int64 version;

      /// <summary>
      /// The manufacturer name
      /// </summary>
      private String mfgName;

      /// <summary>
      /// The provider name
      /// </summary>
      private String providerName;

      /// <summary>
      /// The driver date
      /// </summary>
      private DateTime driverDate;

      /// <summary>
      /// Initializes a new instance of the <see cref="DriverInstance"/> class.
      /// </summary>
      public DriverInstance()
      {
         this.description = String.Empty;
         this.version = -1;
      }

      /// <summary>
      /// Gets the driver date.
      /// </summary>
      /// <value>
      /// The driver date.
      /// </value>
      public DateTime DriverDate
      {
         get
         {
            return this.driverDate;
         }

         internal set
         {
            this.driverDate = value;
         }
      }

      /// <summary>
      /// Gets the name of the provider.
      /// </summary>
      /// <value>
      /// The name of the provider.
      /// </value>
      public String ProviderName
      {
         get
         {
            return this.providerName;
         }

         internal set
         {
            this.providerName = value;
         }
      }

      /// <summary>
      /// Gets the name of the manufacturer.
      /// </summary>
      /// <value>
      /// The name of the manufacturer.
      /// </value>
      public String ManufacturerName
      {
         get
         {
            return this.mfgName;
         }

         internal set
         {
            this.mfgName = value;
         }
      }

      /// <summary>
      /// Gets the description.
      /// </summary>
      /// <value>
      /// The description.
      /// </value>
      public String Description
      {
         get
         {
            return this.description;
         }

         internal set
         {
            this.description = value;
         }
      }

      /// <summary>
      /// Gets the version.
      /// </summary>
      /// <value>
      /// The version.
      /// </value>
      public Int64 Version
      {
         get
         {
            return this.version;
         }

         internal set
         {
            this.version = value;
         }
      }
   }
}
