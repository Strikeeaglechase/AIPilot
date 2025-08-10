# AI Pilot

AIPilot (AIP) is a tool/game to enable the creation, testing, and deployment of AI for VTOL VR. AI Clients are created via a C# DLL that exports a class implementing an AIPProvider. AI Clients can be written in any language, with the DLL acting as an interface to whatever other language you wish to use.

## Getting Started

Download the latest build from releases, then open the `AIPProvider.sln` project. Every time you build the solution `onBuild.bat` will run which will automatically produce a vtgr replay file in the `sim` directory. You can manually run this script as well.

The simulation now uses a JSON configuration file instead of command line arguments. You can run the simulation by providing a config file:

```
AIPilot.exe [CONFIG_FILE]
```

Where `CONFIG_FILE` is a filepath to a JSON document containing any of the following options:

```json
{
	"allied": "",
	"enemy": "",
	"debugAllied": false,
	"debugEnemy": false,
	"alliedCount": 1,
	"enemyCount": 1,
	"spawnDist": 72000,
	"spawnAlt": 6000,
	"maxTime": 300,
	"noMap": false,
	"map": "",
	"weaponMaxes": "",
	"alliedArgs": [],
	"enemyArgs": []
}
```

### Configuration Options

-  **allied** - Path to an AIPProvider DLL for allied team aircraft
-  **enemy** - Path to an AIPProvider DLL for enemy team aircraft
-  **debugAllied** - Enable debugging for the allied team
-  **debugEnemy** - Enable debugging for the enemy team
-  **alliedCount** - Sets number of allied aircraft to spawn
-  **enemyCount** - Sets number of enemy aircraft to spawn
-  **spawnDist** - Spawn distance between teams in meters
-  **spawnAlt** - Spawn altitude in meters
-  **maxTime** - Maximum simulation duration in seconds (sim time, not real time)
-  **noMap** - Disable map loading
-  **map** - Path to a directory containing the map to load
-  **weaponMaxes** - Sets limits to how many of each weapon type can be spawned. Format: "WEAPON_NAME:COUNT,WEAPON_NAME:COUNT"
-  **alliedArgs** - List of arguments to pass into the SetupInfo call to the Allied AIP
-  **enemyArgs** - List of arguments to pass into the SetupInfo call to the Enemy AIP

All options are optional and will use their default values if not specified.

Use `AIPilot.exe --help` to see the available options.

The simulation will run until one team has no aircraft remaining, or the sim time runs out. Having multiple AIP's with debug enabled at the same time is highly discouraged, as the diagnostic files produced do not differentiate aircraft source and thus they will overbite each other's data.

In order to view the replay first the recording.json file must be converted into a replay. This is done with HeadlessClient.exe
`HeadlessClient.exe --convert --input <path to recording.json> --output <output path> --map <map path>`

> [!NOTE]
> Any file extension for output is valid, however the norm is a `.vtgr`, and it is highly recommended to configure windows to open VTGR files with HeadlessClient automatically

The replay file can now be opened by clicking the produced file, pressing "Open With", and navigating to Headless Client.

It may be helpful to setup a simple on-build script for AIPProvider to run these two steps every time you build your code. Pressing Ctrl+R on an open HC window will automatically reload the new replay without need to close and reopen it.
