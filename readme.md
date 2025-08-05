# AI Pilot

AIPilot (AIP) is a tool/game to enable the creation, testing, and deployment of AI for VTOL VR. AI Clients are created via a C# DLL that exports a class implementing an AIPProvider. AI Clients can be written in any language, with the DLL acting as an interface to whatever other language you wish to use.

## Getting Started

Download the latest build from releases, then open the `AIPProvider.sln` project. Every time you build the solution `onBuild.bat` will run which will automatically produce a vtgr replay file in the `sim` directory. You can manually run this script as well. Below is a list of CLI arguments you can pass to the simulation (AIPilot.exe), you can either manually run this or update the `onBuild.bat` file to include them.

```
--allied          (Default: ) Path to an AIPProvider DLL for allied team aircraft
--enemy           (Default: ) Path to an AIPProvider DLL for enemy team aircraft
--debug-allied    (Default: false) Enable debugging for the allied team
--debug-enemy     (Default: false) Enable debugging for the enemy team
--allied-count    (Default: 1) Sets number of allied aircraft to spawn
--enemy-count     (Default: 1) Sets number of enemy aircraft to spawn
--spawn-dist      (Default: 72000) Spawn distance between teams in meters
--spawn-alt       (Default: 6000) Spawn altitude in meters
--max-time        (Default: 300) Maximum simulation duration in seconds (sim time, not real time)
--no-map          (Default: false) Disable map loading
--map             (Default: ) Path to a directory containing the map to load
--weapon-maxes    Sets limits to how many of each weapon type can be spawned. Format: "WEAPON_NAME:COUNT,WEAPON_NAME:COUNT"
--help            Display this help screen.
--version         Display version information.
```

All arguments are optional

The simulation will run until one team has no aircraft remaining, or the sim time runs out. Having multiple AIP's with debug enabled at the same time is highly discouraged, as the diagnostic files produced do not differentiate aircraft source and thus they will overbite each other's data.

In order to view the replay first the recording.json file must be converted into a replay. This is done with HeadlessClient.exe
`HeadlessClient.exe --convert --input <path to recording.json> --output <output path> --map <map path>`

> [!NOTE]
> Any file extension for output is valid, however the norm is a `.vtgr`, and it is highly recommended to configure windows to open VTGR files with HeadlessClient automatically

The replay file can now be opened by clicking the produced file, pressing "Open With", and navigating to Headless Client.

It may be helpful to setup a simple on-build script for AIPProvider to run these two steps every time you build your code. Pressing Ctrl+R on an open HC window will automatically reload the new replay without need to close and reopen it.
