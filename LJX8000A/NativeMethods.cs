//Copyright (c) 2020 KEYENCE CORPORATION. All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace LJXA_ImageAcquisitionSample
{
    #region enum

    /// <summary>
    /// Return value definition
    /// </summary>
    public enum Rc
    {
        /// <summary>Normal termination</summary>
        Ok = 0x0000,
        /// <summary>Failed to open the device</summary>
        ErrOpenDevice = 0x1000,
        /// <summary>Device not open</summary>
        ErrNoDevice,
        /// <summary>Command send error</summary>
        ErrSend,
        /// <summary>Response reception error</summary>
        ErrReceive,
        /// <summary>Timeout</summary>
        ErrTimeout,
        /// <summary>No free space</summary>
        ErrNomemory,
        /// <summary>Parameter error</summary>
        ErrParameter,
        /// <summary>Received header format error</summary>
        ErrRecvFmt,
        /// <summary>Not open error (for high-speed communication)</summary>
        ErrHispeedNoDevice = 0x1009,
        /// <summary>Already open error (for high-speed communication)</summary>
        ErrHispeedOpenYet,
        /// <summary>Already performing high-speed communication error (for high-speed communication)</summary>
        ErrHispeedRecvYet,
        /// <summary>Insufficient buffer size</summary>
        ErrBufferShort,
    }

    /// Definition that indicates the "setting type" in LJX8IF_TARGET_SETTING structure.
    public enum SettingType : byte
    {
        /// <summary>Environment setting</summary>
        Environment = 0x01,
        /// <summary>Common measurement setting</summary>
        Common = 0x02,
        /// <summary>Measurement Program setting</summary>
        Program00 = 0x10,
        Program01,
        Program02,
        Program03,
        Program04,
        Program05,
        Program06,
        Program07,
        Program08,
        Program09,
        Program10,
        Program11,
        Program12,
        Program13,
        Program14,
        Program15,
    };

    #endregion

	#region Structure
	/// <summary>
	/// Version Information
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_VERSION_INFO
	{
		public int nMajorNumber;
		public int nMinorNumber;
		public int nRevisionNumber;
		public int nBuildNumber;
	};

	/// <summary>
	/// Ethernet settings structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_ETHERNET_CONFIG
	{
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
		public byte[] abyIpAddress;
		public ushort wPortNo;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve;

	};																																																																							   

	/// <summary>
	/// Setting item designation structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_TARGET_SETTING
	{
		public byte byType;
		public byte byCategory;
		public byte byItem;
		public byte reserve;
		public byte byTarget1;
		public byte byTarget2;
		public byte byTarget3;
		public byte byTarget4;
	};

	/// <summary>
	/// Profile information structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_PROFILE_INFO
	{
		public byte byProfileCount;
		public byte reserve1;
		public byte byLuminanceOutput;
		public byte reserve2;
		public short nProfileDataCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve3;
		public int lXStart;
		public int lXPitch;
	};

	/// <summary>
	/// Profile header information structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_PROFILE_HEADER
	{
		public uint reserve;
		public uint dwTriggerCount;
		public int lEncoderCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public uint[] reserve2;
	};

	/// <summary>
	/// Profile footer information structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_PROFILE_FOOTER
	{
		public uint reserve;
	};

	/// <summary>
	/// Get profile request structure (batch measurement: off)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_GET_PROFILE_REQUEST
	{
		public byte byTargetBank;
		public byte byPositionMode;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve;
		public uint dwGetProfileNo;
		public byte byGetProfileCount;
		public byte byErase;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve2;
	};

	/// <summary>
	/// Get profile request structure (batch measurement: on)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_GET_BATCH_PROFILE_REQUEST
	{
		public byte byTargetBank;
		public byte byPositionMode;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve;
		public uint dwGetBatchNo;
		public uint dwGetProfileNo;
		public byte byGetProfileCount;
		public byte byErase;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve2;
	};

	/// <summary>
	/// Get profile response structure (batch measurement: off)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_GET_PROFILE_RESPONSE
	{
		public uint dwCurrentProfileNo;
		public uint dwOldestProfileNo;
		public uint dwGetTopProfileNo;
		public byte byGetProfileCount;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] reserve;
	};

	/// <summary>
	/// Get profile response structure (batch measurement: on)
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_GET_BATCH_PROFILE_RESPONSE
	{
		public uint dwCurrentBatchNo;
		public uint dwCurrentBatchProfileCount;
		public uint dwOldestBatchNo;
		public uint dwOldestBatchProfileCount;
		public uint dwGetBatchNo;
		public uint dwGetBatchProfileCount;
		public uint dwGetBatchTopProfileNo;
		public byte byGetProfileCount;
		public byte byCurrentBatchCommited;
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
		public byte[] reserve;
	};

	/// <summary>
	/// High-speed communication start preparation request structure
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct LJX8IF_HIGH_SPEED_PRE_START_REQUEST
	{
		public byte bySendPosition;		// Send start position
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
		public byte[] reserve;		// Reservation 
	};

	#endregion

	#region Method
	/// <summary>
	/// Callback function for high-speed communication
	/// </summary>
	/// <param name="pBuffer">Received profile data pointer</param>
	/// <param name="dwSize">Size in units of bytes of one profile</param>
	/// <param name="dwCount">Number of profiles</param>
	/// <param name="dwNotify">Finalization condition</param>
	/// <param name="dwUser">Thread ID</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
	public delegate void HighSpeedDataCallBack(IntPtr pBuffer, uint dwSize, uint dwCount, uint dwNotify, uint dwUser);

	/// <summary>
	/// Callback function for high-speed communication simple array
	/// </summary>
	/// <param name="pProfileHeaderArray">Received header data array pointer</param>
	/// <param name="pHeightProfileArray">Received profile data array pointer</param>
	/// <param name="pLuminanceProfileArray">Received luminance profile data array pointer</param>
	/// <param name="dwLuminanceEnable">The value indicating whether luminance data output is enable or not</param>
	/// <param name="dwProfileDataCount">The data count of one profile</param>
	/// <param name="dwCount">Number of profiles</param>
	/// <param name="dwNotify">Finalization condition</param>
	/// <param name="dwUser">Thread ID</param>
	[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HighSpeedDataCallBackForSimpleArray(IntPtr pProfileHeaderArray, IntPtr pHeightProfileArray, IntPtr pLuminanceProfileArray, uint dwLuminanceEnable, uint dwProfileDataCount, uint dwCount, uint dwNotify, uint dwUser);

    /// <summary>
    /// Function definitions
    /// </summary>
    internal class NativeMethods
    {
        /// <summary>
        /// Number of connectable devices
        /// </summary>
        internal static int DeviceCount
        {
            get { return 6; }
        }

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_Initialize();

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_Finalize();

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_EthernetOpen(int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_CommunicationClose(int lDeviceId);
		
		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_Trigger(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StartMeasure(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_StopMeasure(int lDeviceId);



        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_FinalizeHighSpeedDataCommunication(int lDeviceId);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetZUnitSimpleArray(int lDeviceId, ref ushort pwZUnit);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_ClearMemory(int lDeviceId);
		
		[DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_SetXpitch(int lDeviceId, uint dwXpitch);

        [DllImport("LJX8_IF.dll")]
        internal static extern int LJX8IF_GetXpitch(int lDeviceId, ref uint pdwXpitch);

        [DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_GetProfile(int lDeviceId, ref LJX8IF_GET_PROFILE_REQUEST pReq,
		ref LJX8IF_GET_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo, IntPtr pdwProfileData, uint dwDataSize);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_GetBatchProfile(int lDeviceId, ref LJX8IF_GET_BATCH_PROFILE_REQUEST pReq,
		ref LJX8IF_GET_BATCH_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo,
		IntPtr pdwBatchData, uint dwDataSize);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_GetBatchSimpleArray(int lDeviceId, ref LJX8IF_GET_BATCH_PROFILE_REQUEST pReq,
		ref LJX8IF_GET_BATCH_PROFILE_RESPONSE pRsp, ref LJX8IF_PROFILE_INFO pProfileInfo,
		IntPtr pProfileHeaderArray, IntPtr pHeightProfileArray, IntPtr pLuminanceProfileArray);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_InitializeHighSpeedDataCommunication(
		int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig, ushort wHighSpeedPortNo,
		HighSpeedDataCallBack pCallBack, uint dwProfileCount, uint dwThreadId);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_InitializeHighSpeedDataCommunicationSimpleArray(
		int lDeviceId, ref LJX8IF_ETHERNET_CONFIG pEthernetConfig, ushort wHighSpeedPortNo,
		HighSpeedDataCallBackForSimpleArray pCallBackSimpleArray, uint dwProfileCount, uint dwThreadId);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_PreStartHighSpeedDataCommunication(
		int lDeviceId, ref LJX8IF_HIGH_SPEED_PRE_START_REQUEST pReq,
		ref LJX8IF_PROFILE_INFO pProfileInfo);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_StartHighSpeedDataCommunication(int lDeviceId);

		[DllImport("LJX8_IF.dll")]
		internal static extern int LJX8IF_StopHighSpeedDataCommunication(int lDeviceId);
    }
    #endregion
}
