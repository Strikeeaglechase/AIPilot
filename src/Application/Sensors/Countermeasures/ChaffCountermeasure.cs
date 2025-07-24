using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityGERunner.UnityApplication
{
	
	public class LockReturn
	{
	    public enum ReturnTypes
	    {
	        Actual,
	        Chaff,
	        // Jammer
	    }
	
	    public ReturnTypes name;
	    public Vector3 pos;
	    public Vector3 vel;
	    public float weight;
	    public LockReturn(ReturnTypes n)
	    {
	        name = n;
	    }
	}
	
	public struct LockPulseParams
	{
	    public Vector3 radarPos;
	    public Vector3 radarLookDir;
	    public float dotLim;
	    public float xRange;
	    public float rangeGate;
	    public float xClosingSpeed;
	    public float speedGate;
	}
	
	
	public class ChaffCountermeasure : ActorBehaviour
	{
	    public class Chaff
	    {
	        public Vector3 position;
	        private float timeStart;
	        private float effectiveness;
	        private float effectDecay;
	        private float decayTime;
	        private Vector3 accel;
	        private float maxMovementT;
	        private Vector3 startVel;
	
	        public float elapsedTime => Time.time - timeStart;
	        public float effectValue => Mathf.Max(0f, effectiveness - (Time.time - timeStart) * effectDecay);
	        public bool decayed => Time.time - timeStart > decayTime;
	
	        public Vector3 GetCurrentPosition()
	        {
	            float a = Time.time - timeStart;
	            a = Mathf.Min(a, maxMovementT);
	            return position + 0.5f * a * a * accel + startVel * a;
	        }
	
	        public Chaff(ChaffCountermeasure cm)
	        {
	            position = cm.transform.position;
	            timeStart = Time.time;
	            effectiveness = cm.effectiveness;
	            effectDecay = cm.effectDecay;
	            startVel = cm.actor.velocity;
	            accel = -startVel * ECM_CHAFF_DRAG_RATE;
	            maxMovementT = 1f / ECM_CHAFF_DRAG_RATE;
	            decayTime = effectiveness / effectDecay;
	        }
	    }
	
	    public float effectiveness = 2;
	    public float effectDecay = 0.5f;
	
	    public List<Chaff> chaffs = new List<Chaff>();
	
	    public int count = 120;
	    public bool fireChaff = false;
	
	    private const float ECM_CHAFF_CLOSING_SPEED_MUL = 0.07f;
	    private const float ECM_CHAFF_EFFECTIVENESS_MULT = 15f;
	    private const float ECM_CHAFF_CLOSING_SPEED_POW = 2f;
	    private const float ECM_CHAFF_DRAG_RATE = 0.5f;
	
	    public bool FireChaff()
	    {
	        if (count <= 0) return false;
	        count--;
	
	        var chaff = new Chaff(this);
	        chaffs.Add(chaff);
	
	        GameRecorder.Event(new ChaffEvent(actor.entityId));
	
	        return true;
	    }
	
	    protected override void Update()
	    {
	        if (fireChaff)
	        {
	            FireChaff();
	            fireChaff = false;
	        }
	
	        chaffs.RemoveAll(c => c.decayed);
	    }
	
	    public bool GetECMAffectedPos(Radar lr, float actualReturnSignal, Vector3 radarLookDir, float radarLookFOV, float xRange, float rangeGate, float xClosingSpeed, float speedGate, out Vector3 rPos, out Vector3 rVel, out bool receivePing, bool initial, bool debug = false)
	    {
	        // Logger.Info("[HSGE] " + "ECMA running");
	        List<LockReturn> lockReturnPool = new List<LockReturn>();
	
	        float dotLim = Mathf.Cos(radarLookFOV * ((float)Math.PI / 180f));
	        radarLookDir.Normalize();
	        Vector3 position = lr.rotationTransform.position;
	        float totalReceivedStrength = 0f;
	        LockPulseParams lockPulseParams = default(LockPulseParams);
	        lockPulseParams.radarPos = position;
	        lockPulseParams.radarLookDir = radarLookDir;
	        lockPulseParams.dotLim = dotLim;
	        lockPulseParams.xRange = xRange;
	        lockPulseParams.rangeGate = rangeGate;
	        lockPulseParams.xClosingSpeed = xClosingSpeed;
	        lockPulseParams.speedGate = speedGate;
	        LockPulseParams lp = lockPulseParams;
	        receivePing = false;
	        lockReturnPool.Clear();
	        LockReturn lockReturn = new LockReturn(LockReturn.ReturnTypes.Actual)
	        {
	            pos = actor.transform.position,
	            vel = actor.velocity,
	            weight = actualReturnSignal
	        };
	
	        if (IsReturnWithinGate(lockReturn, lp, debug))
	        {
	            lockReturnPool.Add(lockReturn);
	            totalReceivedStrength += lockReturn.weight;
	            receivePing = true;
	        }
	        else if (debug)
	        {
	            Logger.Info("[HSGE] " + " - actual");
	        }
	
	        if (initial)
	        {
	            lp.speedGate *= 10f;
	            lp.rangeGate *= 5f;
	        }
	
	        float lookDirOffset = Mathf.Abs(Vector3.Dot(lockReturn.vel, radarLookDir));
	        float closingSpeedFactor = Mathf.Max(0.001f, Mathf.Pow(lookDirOffset * ECM_CHAFF_CLOSING_SPEED_MUL, ECM_CHAFF_CLOSING_SPEED_POW));
	        float chaffEffectiveness = Mathf.Clamp01(1f - closingSpeedFactor) * ECM_CHAFF_EFFECTIVENESS_MULT;
	        for (int i = 0; i < chaffs.Count; i++)
	        {
	            ChaffCountermeasure.Chaff chaff = chaffs[i];
	            if (!chaff.decayed)
	            {
	                float sqrDist = (chaff.position - lr.rotationTransform.position).sqrMagnitude;
	                float recievedStrength = lr.transmissionStrength / sqrDist;
	                // Logger.Info("[HSGE] " + $"Recieved {recievedStrength} off chaff");
	                LockReturn chaffLr = new LockReturn(LockReturn.ReturnTypes.Chaff)
	                {
	                    pos = chaff.GetCurrentPosition(),
	                    vel = Vector3.Lerp(actor.velocity, Vector3.zero, chaff.elapsedTime * ECM_CHAFF_DRAG_RATE),
	                    weight = chaffEffectiveness * chaff.effectValue * recievedStrength
	                };
	
	                if (IsReturnWithinGate(chaffLr, lp, debug))
	                {
	                    lockReturnPool.Add(chaffLr);
	                    totalReceivedStrength += chaffLr.weight;
	                }
	                else if (debug)
	                {
	                    Logger.Info("[HSGE] " + " - chaff");
	                }
	            }
	        }
	
	        if (lockReturnPool.Count == 0)
	        {
	            rPos = default(Vector3);
	            rVel = default(Vector3);
	            return false;
	        }
	
	        float selectedCutoff = UnityGERunner.Random.Range(0f, totalReceivedStrength);
	        float currentSelectTotal = 0f;
	        LockReturn selectedReturn = lockReturnPool[lockReturnPool.Count - 1];
	        for (int k = 0; k < lockReturnPool.Count; k++)
	        {
	            currentSelectTotal += lockReturnPool[k].weight;
	            if (currentSelectTotal >= selectedCutoff)
	            {
	                selectedReturn = lockReturnPool[k];
	                break;
	            }
	        }
	
	        rPos = selectedReturn.pos;
	        rVel = selectedReturn.vel;
	        return true;
	    }
	
	    private bool IsReturnWithinGate(LockReturn r, LockPulseParams lp, bool debug)
	    {
	        Vector3 relPos = r.pos - lp.radarPos;
	        float distance = relPos.magnitude;
	        if (Mathf.Abs(distance - lp.xRange) > lp.rangeGate)
	        {
	            if (debug)
	            {
	                Logger.Info("[HSGE] " + $"RANGE GATE! ({distance - lp.xRange})");
	            }
	            return false;
	        }
	
	        if (Vector3.Dot(relPos / distance, lp.radarLookDir) < lp.dotLim)
	        {
	            if (debug)
	            {
	                Logger.Info("[HSGE] " + "AZIMUTH GATE!");
	            }
	            return false;
	        }
	
	        float lookVelDot = Vector3.Dot(lp.radarLookDir, r.vel);
	        //Logger.Info("[HSGE] " + $"LD: {lp.radarLookDir}, rVel: {r.vel}, speedGate {lp.speedGate}, xCloseSpeed: {lp.xClosingSpeed}, speed: {Mathf.Abs(lookVelDot - lp.xClosingSpeed)}");
	        if (Mathf.Abs(lookVelDot - lp.xClosingSpeed) > lp.speedGate)
	        {
	            if (debug)
	            {
	                Logger.Info("[HSGE] " + $"VEL GATE! ({lookVelDot - lp.xClosingSpeed})");
	            }
	            return false;
	        }
	        return true;
	    }
	}
	
}