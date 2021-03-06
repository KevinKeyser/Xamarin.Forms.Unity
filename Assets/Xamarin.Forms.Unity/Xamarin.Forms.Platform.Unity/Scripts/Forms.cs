﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(Xamarin.Forms.Platform.Unity.UnitySystemResourcesProvider))]


namespace Xamarin.Forms.Platform.Unity
{
	/// <summary>
	/// Xamarin.Forms の Forms 実装。
	/// </summary>
	public class Forms
	{
		/*-----------------------------------------------------------------*/
		#region Private Field

		static bool _isInitialized = false;
		static Thread _mainThread;
		static UnityFormsApplicationActivity _activity;

		#endregion

		/*-----------------------------------------------------------------*/
		#region MonoBehavior

		static public void Init(UnityFormsApplicationActivity activity)
		{
			if (_isInitialized)
			{
				return;
			}

			_mainThread = Thread.CurrentThread;
			_activity = activity;

			Device.PlatformServices = new UnityPlatformServices();
			Device.SetIdiom(TargetIdiom.Desktop);
			Device.Info = new UnityDeviceInfo();

			Registrar.RegisterAll(new[]
				{ typeof(ExportRendererAttribute), typeof(ExportCellAttribute), typeof(ExportImageSourceHandlerAttribute) });
			ExpressionSearch.Default = new UnityExpressionSearch();

			_isInitialized = true;
		}

		static public void Uninit()
		{
			Device.PlatformServices = null;
			Device.SetIdiom(TargetIdiom.Unsupported);
			Device.Info = null;

			_mainThread = null;
			_activity = null;
			_isInitialized = false;
		}

		#endregion

		/*-----------------------------------------------------------------*/
		#region Property

		static public Thread MainThread => _mainThread;
		static public UnityFormsApplicationActivity Activity => _activity;

		#endregion
	}
}
