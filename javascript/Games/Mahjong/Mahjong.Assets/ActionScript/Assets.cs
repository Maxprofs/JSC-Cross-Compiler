﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ScriptCoreLib;
using ScriptCoreLib.ActionScript.mx.core;
using ScriptCoreLib.ActionScript;
using ScriptCoreLib.ActionScript.Extensions;
using ScriptCoreLib.Shared;

namespace Mahjong.ActionScript
{
	[Script(Implements=typeof(Mahjong.Shared.Assets))]
	public class __Assets
	{

		public static readonly __Assets Default = new __Assets();

		public void PlaySound(string SoundName)
		{
			var AssetName = this.FileNames.FirstOrDefault(k => k.EndsWith(SoundName + ".mp3"));

			if (AssetName != null)
				this[AssetName].ToSoundAsset().play();
		}

		public string[] FileNames
		{
			[EmbedGetFileNames]
			get
			{
				throw new NotImplementedException();
			}
		}

		public Class this[string e]
		{
			[EmbedByFileName]
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
