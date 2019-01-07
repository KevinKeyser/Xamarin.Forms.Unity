using UnityEngine;
using System.Windows.Input;
using System.ComponentModel;
using System;

using Xamarin.Forms;


public class SampleBindingContext
{
	
	public ICommand InstantiateCommand
	{
		get;
	}

	public int Counter
	{
		get;
		set;
	}

	public double DoubleValue
	{
		get;
		set;
	}
	
	public SampleBindingContext()
	{
		System.Random random = new System.Random();
		InstantiateCommand = new Command(() =>
		{
			
			var go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			UnityEngine.Object.Destroy(go, 5.0f);

			go.transform.position = new Vector3((float)(random.NextDouble() * 2.0 - 1.0), 5.0f, (float)(random.NextDouble() * 2.0 - 1.0));

			var rb = go.AddComponent<Rigidbody>();
			rb.useGravity = true;

			Counter++;

			DoubleValue = random.NextDouble();
		});	
	}
}

