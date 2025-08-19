using UnityGERunner;
using Coroutine;
using Recorder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UnityGERunner.UnityApplication
{
	
	
	public struct IRWeaponState
	{
	    public float seekerFov;
	    public float heat;
	    public NetVector lookDir;
	
	    public IRWeaponState(HeatSeeker seeker)
	    {
	        seekerFov = seeker.seekerFOV;
	        heat = seeker.seenHeat;
	        lookDir = new NetVector(seeker.transform.forward);
	    }
	}
	
	
	
	public class EquipManager : ActorBehaviour, IVehicleReadyNotificationHandler, IWeaponRCSProvider
	{
	    public List<string> equips = new List<string>();
	    public List<Missile> weapons = new List<Missile>();
	    public WeaponStats weaponStats;
	    public Radar radar;
	    public SimpleDrag equipDrag;
	
	    public int selectedWeapon = 0;
	
	    public bool tryFire = false;
	
	    
	    public bool hasGun = false;
	    public int bulletCount = 800;
	    public float bulletSpeed = 1100;
	    public float dispersion = 0.1f;
	    public float lifetime = 3;
	    private List<Bullet> bullets = new List<Bullet>();
	
	    public void OnVehicleReadyNotification()
	    {
	        for (int i = 0; i < equips.Count; i++)
	        {
	            if (string.IsNullOrEmpty(equips[i])) continue;
	
	            var equipSpawnCommand = new SpawnAIPWeapon
	            {
	                path = equips[i],
	                hpIndex = i
	            };
	
	            Logger.Info("[HSGE] " + $"Requesting spawn {equips[i]} onto hp {i}");
	            HCConnector.instance.SendCommandPacket(equipSpawnCommand);
	            LocalFightController.instance.RequestWeaponSpawn(this, equipSpawnCommand);
	        }
	    }
	
	    public void HandleEquipSpawned(string equipPath, int entityId, int hpIndex)
	    {
	        Logger.Info("[HSGE] " + $"Equip spawned: {equipPath} ({entityId}) on hp {hpIndex}");
	    }
	
	    private bool CheckWeaponMaxesForSpawn(string weaponPath)
	    {
	        var weaponMaxes = Options.WeaponCountLimits;
	        if (!weaponMaxes.ContainsKey(weaponPath)) return true;
	
	        var currentWeaponCount = weapons.Where(w => w.weaponPath == weaponPath).Count();
	        if (currentWeaponCount >= weaponMaxes[weaponPath]) return false;
	
	        return true;
	    }
	
	    public void HandleWeaponSpawned(string weaponPath, int weaponEntityId, int hpEntityId, int railIndex)
	    {
	        Logger.Info("[HSGE] " + $"Weapon {weaponPath} spawned with id {weaponEntityId}, spawned onto {hpEntityId} rail idx {railIndex}");
	        var info = weaponStats.weaponPrefabs.Find(p => p.path == weaponPath);
	        if (info == null)
	        {
	            Logger.Error("[HSGE] " + $"Unable to locate prefab for {weaponPath}");
	            return;
	        }
	
	        var underWepLimits = CheckWeaponMaxesForSpawn(weaponPath);
	        if (!underWepLimits)
	        {
	            Logger.Warn("[HSGE] " + $"Cannot spawn {weaponPath} as already at the max weapon count for this type of missile");
	            return;
	        }
	
	        Logger.Info("[HSGE] " + $"Weapon path from info: {info.path}, prefab: {info.prefab}");
	
	        var missileGo = Instantiate(info.prefab);
	        missileGo.transform.SetParent(transform);
	        missileGo.transform.localPosition = Vector3.zero;
	        missileGo.transform.localRotation = Quaternion.identity;
	
	        var missileComp = missileGo.GetComponent<Missile>();
	        missileComp.entityId = weaponEntityId;
	        missileComp.weaponPath = weaponPath;
	
	        weapons.Add(missileComp);
	
	        UpdateEquipDrag();
	    }
	
	    private void FireNextARHMissile(int pdt = -1)
	    {
	        var arh = weapons.Find(w => w != null && w.GetComponent<Missile>().guidanceMode == MissileGuidanceMode.Radar);
	        if (arh == null)
	        {
	            Logger.Warn("[HSGE] " + $"Attempt to fire ARH Missile however no radar missiles found");
	            return;
	        }
	
	        var missile = arh.GetComponent<Missile>();
	        var selectedTarget = (pdt != -1 && pdt > 0 && pdt < radar.twsedTargets.Count) ? radar.twsedTargets[pdt] : radar.lockData?.actor;
	        if (selectedTarget == null)
	        {
	            //Logger.Warn("[HSGE] " + $"No radar target in fire ARH missile");
	            return;
	        }
	
	        tryFire = false;
	        Logger.Info("[HSGE] " + $"Firing ARH missile {missile} at {selectedTarget}");
	        missile.FireWithRadarData(radar, selectedTarget);
	        weapons.Remove(arh);
	
	        UpdateEquipDrag();
	    }
	
	    private Missile GetSelectedWeapon()
	    {
	        if (selectedWeapon >= 0 && selectedWeapon < weapons.Count) return weapons[selectedWeapon];
	        return null;
	    }
	
	    private static Vector3 WeightedDirectionDeviation(Vector3 direction, float maxAngle)
	    {
	        float num = UnityGERunner.Random.Range(0f, 1f);
	        float maxRadiansDelta = maxAngle * (num * num) * ((float)Math.PI / 180f);
	        return Vector3.RotateTowards(direction, Vector3.ProjectOnPlane(UnityGERunner.Random.onUnitSphere, direction), maxRadiansDelta, 0f).normalized;
	    }
	
	    private void FireBullet()
	    {
	        if (!hasGun) return;
	        if (bulletCount <= 0) return;
	        bulletCount--;
	
	        var fireDirection = WeightedDirectionDeviation(transform.forward, dispersion);
	        var velocity = fireDirection * bulletSpeed + actor.velocity;
	
	        bullets.Add(new Bullet(transform.position, velocity));
	    }
	
	    public void FireSelectedMissile()
	    {
	        if (selectedWeapon == -1)
	        {
	            FireBullet();
	            return;
	        }
	
	        var missile = GetSelectedWeapon();
	
	        if (missile == null)
	        {
	            Logger.Warn("[HSGE] " + $"Attempt to fire had selected {selectedWeapon} which is not a valid weapon index");
	            return;
	
	        }
	
	        if (missile.guidanceMode == MissileGuidanceMode.Radar)
	        {
	            var selectedTarget = (radar.pdtTwsIdx != -1 && radar.pdtTwsIdx > 0 && radar.pdtTwsIdx <= radar.twsedTargets.Count) ? radar.twsedTargets[radar.pdtTwsIdx] : radar.lockData?.actor;
	            if (selectedTarget == null)
	            {
	                Logger.Warn("[HSGE] " + $"No selected target for ARH fire, pdt idx: {radar.pdtTwsIdx}");
	                return;
	            }
	
	            missile.FireWithRadarData(radar, selectedTarget);
	            weapons.Remove(missile);
	        }
	        else
	        {
	            missile.Fire();
	            weapons.Remove(missile);
	        }
	
	        UpdateEquipDrag();
	    }
	
	    private void UpdateEquipDrag()
	    {
	        float totalDrag = 0;
	        foreach (var weapon in weapons)
	        {
	            totalDrag += weapon.info.drag;
	        }
	
	        equipDrag.area = totalDrag;
	    }
	
	    protected override void Start()
	    {
	    }
	
	    protected override void FixedUpdate()
	    {
	        //weapons.RemoveAll(weapon => weapon == null || weapon.fired);
	        //if (tryFire && weapons.Count > 0)
	        //{
	
	        //    FireNextARHMissile();
	        //    //tryFire = false;
	        //    //var missile = weapons[0].GetComponent<Missile>();
	        //    //missile.Fire();
	        //    //weapons.RemoveAt(0);
	        //}
	
	        //FireBullet();
	
	        var possibleBulletTargets = gameObject.GetComponentsInSceneImplementing<AIClient>().ToList();
	        possibleBulletTargets.Remove(actor.aiClient); // Prevent bullet from hitting ourselve
	
	        foreach (var b in bullets) b.BUpdate(possibleBulletTargets);
	        bullets.RemoveAll(b => Time.time - b.firedAt > lifetime);
	    }
	
	    private HeatSeeker GetSelectedWeaponHeatSeeker()
	    {
	        var weapon = GetSelectedWeapon();
	        if (weapon == null) return null;
	
	        var heatSeeker = weapon.heatSeeker;
	        return heatSeeker;
	    }
	
	    public IRWeaponState GetIRWeaponState()
	    {
	        var heatSeeker = GetSelectedWeaponHeatSeeker();
	        if (heatSeeker == null) return default(IRWeaponState);
	        return new IRWeaponState(heatSeeker);
	    }
	
	    public void SetIRLookDir(Vector3 dir)
	    {
	        var heatSeeker = GetSelectedWeaponHeatSeeker();
	        if (heatSeeker == null) return;
	
	        heatSeeker.commandLookDir = dir;
	    }
	
	    public void SetIRTrigUncage(bool uncage)
	    {
	        var heatSeeker = GetSelectedWeaponHeatSeeker();
	        if (heatSeeker == null) return;
	
	        heatSeeker.triggerUncaged = uncage;
	    }
	
	    public string[] GetAttachedWeapons()
	    {
	        return weapons.Select(w => w.weaponPath).ToArray();
	    }
	
	    public float GetWeaponRCS()
	    {
	        return weapons.Select(m => m.rcs).Sum();
	    }
	
	}
	
}