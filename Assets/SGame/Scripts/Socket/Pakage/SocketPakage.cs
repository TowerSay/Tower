
using UnityEngine;
using System.Collections;
using System;
using System.Threading;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public class SocketPakage
{
	//结构体序列化
	[System.Serializable]

	//4字节对齐 iphone 和 android上可以1字节对齐
	#if UNITY_EDITOR_WIN
	[StructLayout(LayoutKind.Sequential, Pack = 4)]
	#elif 
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	#endif


	public struct WorldPackage
	{
		public byte mEquipID;
		public byte mAnimationID;
		public byte mHP;
		public short mPosx;
		public short mPosy;
		public short mPosz;
		public short mRosx;
		public short mRosy;
		public short mRosz;
		
		public WorldPackage(short posx,short posy,short posz, short rosx, short rosy, short rosz,byte equipID,byte animationID,byte hp)
		{

			mPosx = posx;
			mPosy = posy;
			mPosz = posz;
			mRosx = rosx;
			mRosy = rosy;
			mRosz = rosz;
			mEquipID = equipID;
			mAnimationID = animationID;
			mHP = hp;
		}
	};  



}

