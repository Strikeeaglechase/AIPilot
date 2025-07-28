# Simulation/Game Info

The simulation utility is meant to be compatible/mimic [VTOL VR](https://store.steampowered.com/app/667970/VTOL_VR/) as much as possible, so that the AI can fight in a VTOL server and behave like normal. If you are familiar with VTOL's mechanics then much of this should be familiar, however there are some sim-specific differences.

## Weapons

The sim currently has two weapons, the AIM-120 and the AIRS-T

-  AIM-120 is a medium range radar guided missile, initially after launch the missile will receive target updates from the radar to correct its flight path. Once within a set distance it switches to it's onboard radar and independently tracks the target. STT or TWS can be used to guide the missile
-  AIRS-T is a heat-seeking missile, it guides on it's own to the strongest heat source in its FOV. You must tell the seeker what direction to look prior to launch

### Hardpoints

A weapon is attached to the aircraft via a hardpoint, in AIPSim hardpoints do not actually exist (no RCS or drag), but are what you use to spawn weapons onto the aircraft.

> [!NOTE]
> All hardpoint names are prefixed with "HPEquips/AFighter/", and all weapon names are prefixed with "Weapons/Missiles/", so when using the table below ensure you've added these prefixes.

| Hardpoint      | Weapon  | Allowed Slots | Weapon Count |
| -------------- | ------- | ------------- | ------------ |
| af_amraamRail  | AIM-120 | 1,2,3,4       | 1            |
| fa26_iris-t-x1 | AIRS-T  | 2,6           | 1            |

## Radar

The radar is the primary source of target detection/tracking. It scans a 120 degree arc (60 either side of the nose) and has a vertical FOV of 50 degrees (25 up/down). The vertical horizontal FOV can be commanded to be narrower in order to increase scan rate in an area, and the radar can be steered left/right/up/down in order to change where exactly it is looking.

While scanning, anytime the radar detects a target you will receive the ID and team of the detected target. If you would like actual target data (position/velocity etc) then the target must be selected for Track While Scan (TWS). TWS allows the radar to continue scanning while tracking a target. You may have up to 4 TWS targets, and chose one of them to be the Primary Designated Target (PDT). The PDT is what the next AIM-120 fired will target. Each successive TWS target selected will slow the radar scan rate down.

The final mode for the radar is Single Target Track (STT). This mode has the radar focused on one primary target, and stops scanning (TWS contacts will continue to be updated). STT locks are much harder to break than TWS locks. To fire at an STT ensure that the PDT index is set to -1.

## Radar Warning Receiver (RWR)

The RWR allows you to gain information about radar's that are scanning you. The RWR will be able to detect radar scans even beyond where the radar is actually able to detect anything due to the requirement for the radar signal to perform a round trip, where as the RWR only has to detect a half-trip. Range can be estimated via the received signal strength, however the RWR has imprecision in both bearing and signal strength.

## Visual

Any missiles within 5km, and aircraft within 10km will be visually spotted, which provides perfect bearing and elevation however no range.

## IR

With a heat seeking missile selected it is possible to read heat data from the sensor. The sensor does not slew instantly, so you may wish to readback the current slave angle in order to confirm where exactly it is pointing. With trigger uncage set the seeker will follow the heat sources it can see independently, otherwise fallback to your command.
