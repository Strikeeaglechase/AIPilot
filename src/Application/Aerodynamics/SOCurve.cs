using UnityGERunner;
using Coroutine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	
	public class SOCurve : ScriptableObject
	{
	    public AnimationCurve curve;
	
	    public float Evaluate(float t)
	    {
	        return curve.Evaluate(t);
	    }
	}
}