using UnityGERunner;
using Coroutine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnityGERunner.UnityApplication
{
	
	public class ActorBehaviour : MonoBehaviour
	{
	    private Actor _actor;
	    private bool attemptedToGetActor = false;
	
	    protected Actor actor
	    {
	        get
	        {
	            if (!attemptedToGetActor)
	            {
	                _actor = GetComponentInParent<Actor>();
	                attemptedToGetActor = true;
	            }
	            return _actor;
	        }
	        private set { }
	    }
	
	    // protected override void Awake()
	    // {
	    // actor = GetComponent<Actor>();
	    // }
	}
}