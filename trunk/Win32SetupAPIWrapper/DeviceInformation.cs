//-----------------------------------------------------------------------
// <copyright file="DeviceInformation.cs" company="IMS Messsysteme GmbH">
//     Copyright (c) IMS Messsysteme GmbH.
// </copyright>
// <author>Janis Fliegenschmidt</author>
//-----------------------------------------------------------------------

namespace Win32SetupAPIWrapper
{
   using System;

   /// <summary>
   /// Class that holds the device information that 
   /// can be retrieved via the win32 setup apis.
   /// Caution! Almost all properties are stored in
   /// Strings, but they might actually be numbers
   /// or binary data!
   /// </summary>
   public class DeviceInformation
   {
      #region Fields
      /// <summary>
      /// The bus number
      /// </summary>
      private Int32 busNo;

      /// <summary>
      /// The device description
      /// </summary>
      private String description;

      /// <summary>
      /// The bus type GUID
      /// </summary>
      private String busTypeGuid;

      /// <summary>
      /// The device class GUID
      /// </summary>
      private String deviceClassGuid;

      /// <summary>
      /// The hardware id
      /// </summary>
      private String hardwareId;

      /// <summary>
      /// The location info
      /// </summary>
      private String locationInfo;

      /// <summary>
      /// The driver information
      /// </summary>
      private String driver;

      /// <summary>
      /// The compatible IDs
      /// </summary>
      private String compatibleIDs;

      /// <summary>
      /// The service info
      /// </summary>
      private String serviceInfo;

      /// <summary>
      /// The class name
      /// </summary>
      private String className;

      /// <summary>
      /// The config flags
      /// </summary>
      private String configFlags;

      /// <summary>
      /// The manufacturer name
      /// </summary>
      private String manufacturerName;

      /// <summary>
      /// The friendly name
      /// </summary>
      private String friendlyName;

      /// <summary>
      /// The physical device object name
      /// </summary>
      private String physicalDeviceObjectName;

      /// <summary>
      /// The capabilities
      /// </summary>
      private String capabilities;

      /// <summary>
      /// The UI number
      /// </summary>
      private String uiNumber;

      /// <summary>
      /// The upper filters
      /// </summary>
      private String upperFilters;

      /// <summary>
      /// The lower filters
      /// </summary>
      private String lowerFilters;

      /// <summary>
      /// The legacy bus type
      /// </summary>
      private String legacyBusType;

      /// <summary>
      /// The enumerator name
      /// </summary>
      private String enumeratorName;

      /// <summary>
      /// The security info in binary format
      /// </summary>
      private String security;

      /// <summary>
      /// The security info in SDS format
      /// </summary>
      private String securitySds;

      /// <summary>
      /// The device type
      /// </summary>
      private String deviceType;

      /// <summary>
      /// The exclusive access
      /// </summary>
      private String exclusiveAccess;

      /// <summary>
      /// The characteristics
      /// </summary>
      private String characteristics;

      /// <summary>
      /// The address
      /// </summary>
      private String address;

      /// <summary>
      /// The UI number desc format
      /// </summary>
      private String uiNumberDescFormat;

      /// <summary>
      /// The device power data
      /// </summary>
      private String devicePowerData;

      /// <summary>
      /// The removal policy
      /// </summary>
      private String removalPolicy;

      /// <summary>
      /// The hardware default removal policy
      /// </summary>
      private String removalPolicyHwDefault;

      /// <summary>
      /// The removal policy override
      /// </summary>
      private String removalPolicyOverride;

      /// <summary>
      /// The install state
      /// </summary>
      private String installState;

      /// <summary>
      /// The location paths
      /// </summary>
      private String locationPaths;

      /// <summary>
      /// The container id
      /// </summary>
      private String containerId;
      #endregion

      /// <summary>
      /// Initializes a new instance of the <see cref="DeviceInformation"/> class.
      /// </summary>
      public DeviceInformation()
      { // Initialization: All fields empty.
         this.busNo = -1;
         this.description = String.Empty;
         this.busTypeGuid = String.Empty;
         this.deviceClassGuid = String.Empty;
         this.hardwareId = String.Empty;
         this.locationInfo = String.Empty;
         this.address = String.Empty;
         this.capabilities = String.Empty;
         this.characteristics = String.Empty;
         this.className = String.Empty;
         this.compatibleIDs = String.Empty;
         this.configFlags = String.Empty;
         this.containerId = String.Empty;
         this.devicePowerData = String.Empty;
         this.deviceType = String.Empty;
         this.driver = String.Empty;
         this.enumeratorName = String.Empty;
         this.exclusiveAccess = String.Empty;
         this.friendlyName = String.Empty;
         this.installState = String.Empty;
         this.legacyBusType = String.Empty;
         this.locationPaths = String.Empty;
         this.lowerFilters = String.Empty;
         this.manufacturerName = String.Empty;
         this.physicalDeviceObjectName = String.Empty;
         this.removalPolicy = String.Empty;
         this.removalPolicyHwDefault = String.Empty;
         this.removalPolicyOverride = String.Empty;
         this.security = String.Empty;
         this.securitySds = String.Empty;
         this.serviceInfo = String.Empty;
         this.uiNumber = String.Empty;
         this.uiNumberDescFormat = String.Empty;
         this.upperFilters = String.Empty;
      }

      #region Properties
      /// <summary>
      /// Gets the driver identification.
      /// </summary>
      /// <value>
      /// The driver identification.
      /// </value>
      public String DriverIdent
      {
         get
         {
            return this.driver;
         }

         internal set
         {
            this.driver = value;
         }
      }

      /// <summary>
      /// Gets the location paths.
      /// </summary>
      /// <value>
      /// The location paths.
      /// </value>
      public String LocationPaths
      {
         get
         {
            return this.locationPaths;
         }

         internal set
         {
            this.locationPaths = value;
         }
      }

      /// <summary>
      /// Gets the state of the installation.
      /// </summary>
      /// <value>
      /// The state of the installation.
      /// </value>
      public String InstallState
      {
         get
         {
            return this.installState;
         }

         internal set
         {
            this.installState = value;
         }
      }

      /// <summary>
      /// Gets the container ID.
      /// </summary>
      /// <value>
      /// The container ID.
      /// </value>
      public String ContainerID
      {
         get
         {
            return this.containerId;
         }

         internal set
         {
            this.containerId = value;
         }
      }

      /// <summary>
      /// Gets the removal policy override.
      /// </summary>
      /// <value>
      /// The removal policy override.
      /// </value>
      public String RemovalPolicyOverride
      {
         get
         {
            return this.removalPolicyOverride;
         }

         internal set
         {
            this.removalPolicyOverride = value;
         }
      }

      /// <summary>
      /// Gets the removal policy HW default.
      /// </summary>
      /// <value>
      /// The removal policy HW default.
      /// </value>
      public String RemovalPolicyHwDefault
      {
         get
         {
            return this.removalPolicyHwDefault;
         }

         internal set
         {
            this.removalPolicyHwDefault = value;
         }
      }

      /// <summary>
      /// Gets the removal policy.
      /// </summary>
      /// <value>
      /// The removal policy.
      /// </value>
      public String RemovalPolicy
      {
         get
         {
            return this.removalPolicy;
         }

         internal set
         {
            this.removalPolicy = value;
         }
      }

      /// <summary>
      /// Gets the device power data.
      /// </summary>
      /// <value>
      /// The device power data.
      /// </value>
      public String DevicePowerData
      {
         get
         {
            return this.devicePowerData;
         }

         internal set
         {
            this.devicePowerData = value;
         }
      }

      /// <summary>
      /// Gets the UI number in desc format.
      /// </summary>
      /// <value>
      /// The UI number in desc format.
      /// </value>
      public String UiNumberDescFormat
      {
         get
         {
            return this.uiNumberDescFormat;
         }

         internal set
         {
            this.uiNumberDescFormat = value;
         }
      }

      /// <summary>
      /// Gets the address.
      /// </summary>
      /// <value>
      /// The address.
      /// </value>
      public String Address
      {
         get
         {
            return this.address;
         }

         internal set
         {
            this.address = value;
         }
      }

      /// <summary>
      /// Gets the characteristics.
      /// </summary>
      /// <value>
      /// The characteristics.
      /// </value>
      public String Characteristics
      {
         get
         {
            return this.characteristics;
         }

         internal set
         {
            this.characteristics = value;
         }
      }

      /// <summary>
      /// Gets a flag that indicates whether the device is exclusive access.
      /// </summary>
      /// <value>
      /// The is exclusive access.
      /// </value>
      public String IsExclusiveAccess
      {
         get
         {
            return this.exclusiveAccess;
         }

         internal set
         {
            this.exclusiveAccess = value;
         }
      }

      /// <summary>
      /// Gets the type of the device.
      /// </summary>
      /// <value>
      /// The type of the device.
      /// </value>
      public String DeviceType
      {
         get
         {
            return this.deviceType;
         }

         internal set
         {
            this.deviceType = value;
         }
      }

      /// <summary>
      /// Gets the security in SDS format.
      /// </summary>
      /// <value>
      /// The security in SDS format.
      /// </value>
      public String SecuritySDS
      {
         get
         {
            return this.securitySds;
         }

         internal set
         {
            this.securitySds = value;
         }
      }

      /// <summary>
      /// Gets the security.
      /// </summary>
      /// <value>
      /// The security.
      /// </value>
      public String Security
      {
         get
         {
            return this.security;
         }

         internal set
         {
            this.security = value;
         }
      }

      /// <summary>
      /// Gets the name of the enumerator.
      /// </summary>
      /// <value>
      /// The name of the enumerator.
      /// </value>
      public String EnumeratorName
      {
         get
         {
            return this.enumeratorName;
         }

         internal set
         {
            this.enumeratorName = value;
         }
      }

      /// <summary>
      /// Gets the type of the legacy bus.
      /// </summary>
      /// <value>
      /// The type of the legacy bus.
      /// </value>
      public String LegacyBusType
      {
         get
         {
            return this.legacyBusType;
         }

         internal set
         {
            this.legacyBusType = value;
         }
      }

      /// <summary>
      /// Gets the lower filters.
      /// </summary>
      /// <value>
      /// The lower filters.
      /// </value>
      public String LowerFilters
      {
         get
         {
            return this.lowerFilters;
         }

         internal set
         {
            this.lowerFilters = value;
         }
      }

      /// <summary>
      /// Gets the upper filters.
      /// </summary>
      /// <value>
      /// The upper filters.
      /// </value>
      public String UpperFilters
      {
         get
         {
            return this.upperFilters;
         }

         internal set
         {
            this.upperFilters = value;
         }
      }

      /// <summary>
      /// Gets the UI number.
      /// </summary>
      /// <value>
      /// The UI number.
      /// </value>
      public String UINumber
      {
         get
         {
            return this.uiNumber;
         }

         internal set
         {
            this.uiNumber = value;
         }
      }

      /// <summary>
      /// Gets the capabilities.
      /// </summary>
      /// <value>
      /// The capabilities.
      /// </value>
      public String Capabilities
      {
         get
         {
            return this.capabilities;
         }

         internal set
         {
            this.capabilities = value;
         }
      }

      /// <summary>
      /// Gets the name of the physical device object.
      /// </summary>
      /// <value>
      /// The name of the physical device object.
      /// </value>
      public String PhysicalDeviceObjectName
      {
         get
         {
            return this.physicalDeviceObjectName;
         }

         internal set
         {
            this.physicalDeviceObjectName = value;
         }
      }

      /// <summary>
      /// Gets the friendly name.
      /// </summary>
      /// <value>
      /// The friendly name.
      /// </value>
      public String FriendlyName
      {
         get
         {
            return this.friendlyName;
         }

         internal set
         {
            this.friendlyName = value;
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
            return this.manufacturerName;
         }

         internal set
         {
            this.manufacturerName = value;
         }
      }

      /// <summary>
      /// Gets the config flags.
      /// </summary>
      /// <value>
      /// The config flags.
      /// </value>
      public String ConfigFlags
      {
         get
         {
            return this.configFlags;
         }

         internal set
         {
            this.configFlags = value;
         }
      }

      /// <summary>
      /// Gets the name of the device class.
      /// </summary>
      /// <value>
      /// The name of the device class.
      /// </value>
      public String DeviceClassName
      {
         get
         {
            return this.className;
         }

         internal set
         {
            this.className = value;
         }
      }

      /// <summary>
      /// Gets the compatible IDs.
      /// </summary>
      /// <value>
      /// The compatible IDs.
      /// </value>
      public String CompatibleIDs
      {
         get
         {
            return this.compatibleIDs;
         }

         internal set
         {
            this.compatibleIDs = value;
         }
      }

      /// <summary>
      /// Gets the service info.
      /// </summary>
      /// <value>
      /// The service info.
      /// </value>
      public String ServiceInfo
      {
         get
         {
            return this.serviceInfo;
         }

         internal set
         {
            this.serviceInfo = value;
         }
      }

      /// <summary>
      /// Gets the location info.
      /// </summary>
      /// <value>
      /// The location info.
      /// </value>
      public String LocationInfo
      {
         get
         {
            return this.locationInfo;
         }

         internal set
         {
            this.locationInfo = value;
         }
      }

      /// <summary>
      /// Gets the hardware ID.
      /// </summary>
      /// <value>
      /// The hardware ID.
      /// </value>
      public String HardwareID
      {
         get
         {
            return this.hardwareId;
         }

         internal set
         {
            this.hardwareId = value;
         }
      }

      /// <summary>
      /// Gets the device class GUID.
      /// </summary>
      /// <value>
      /// The device class GUID.
      /// </value>
      public String DeviceClassGUID
      {
         get
         {
            return this.deviceClassGuid;
         }

         internal set
         {
            this.deviceClassGuid = value;
         }
      }

      /// <summary>
      /// Gets the bus number.
      /// </summary>
      /// <value>
      /// The bus number.
      /// </value>
      public Int32 BusNumber
      {
         get
         {
            return this.busNo;
         }

         internal set
         {
            this.busNo = value;
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
      /// Gets the bus type GUID.
      /// </summary>
      /// <value>
      /// The bus type GUID.
      /// </value>
      public String BusTypeGUID
      {
         get
         {
            return this.busTypeGuid;
         }

         internal set
         {
            this.busTypeGuid = value;
         }
      }
      #endregion

      #region Public Methods
      /// <summary>
      /// Checks whether two DeviceInformation objects describe
      /// the same device.
      /// </summary>
      /// <param name="comparisonSubject">The comparison subject.</param>
      /// <returns>True if the two objects refer to the same device.</returns>
      public Boolean AreSameDevices(DeviceInformation comparisonSubject)
      {
         Boolean result = true;
         result &= this.PhysicalDeviceObjectName.Equals(comparisonSubject.PhysicalDeviceObjectName);
         return result;
      }
      #endregion
   }
}
