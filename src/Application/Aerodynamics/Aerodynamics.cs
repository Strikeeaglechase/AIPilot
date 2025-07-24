using UnityGERunner;
using Coroutine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	
	public class Aerodynamics : ScriptableObject
	{
	    public AnimationCurve liftCurve;
	
	    public AnimationCurve dragCurve;
	
	    public AnimationCurve buffetCurve;
	
	    public float buffetMagnitude;
	
	    public float buffetTimeFactor;
	
	    public bool perpLiftVector;
	
	    public bool mirroredCurves = true;
	}
	
}