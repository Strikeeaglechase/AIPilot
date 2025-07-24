# AI Pilot

AIPilot (AIP) is a tool/game to enable the creation, testing, and deployment of AI for VTOL VR. AI Clients are created via a C# DLL that exports a class implementing an AIPProvider. AI Clients can be written in any language, with the DLL acting as an interface to whatever other language you wish to use.

## Getting Started

Download the following:

1. [AIP Sim](https://github.com/Strikeeaglechase/AIPilot/releases), this is what allows running local fights with AI against each other.
2. [Headless Client](https://github.com/Strikeeaglechase/VTOLLiveViewerClient/releases/), which is used to visualize/replay a fight.
3. [AIPProvider Example](https://github.com/Strikeeaglechase/AIPProvider), an example AIP implementation

After downloading all of the above, build the AIPProvider which can then be used with the AIP Sim by running the `AIPilot.exe` file and providing the DLL path via one of the arguments:

```
--allied <path>			Path to an AIPProvider DLL for allied team aircraft
--enemy <path> 			Path to an AIPProvider DLL for enemy team aircraft
--debug-allied				Enable debugging for the allied team
--debug-enemy				Enable debugging for the enemy team
--spawn-dist <number>	Spawn distance between teams in meters
--spawn-alt <number>		Spawn altitude in meters
--no-map						Disable map loading
--max-time	<number>		Maximum simulation duration in seconds (sim time, not real time) (TODO)
```

All arguments are optional

The simulation will run until one team has no aircraft remaining, or the sim time runs out. Having multiple AIP's with debug enabled at the same time is highly discouraged, as the diagnostic files produced do not differentiate aircraft source and thus they will overbite each other's data.

In order to view the replay first the recording.json file must be converted into a replay. This is done with HeadlessClient.exe
`HeadlessClient.exe --convert --input <path to recording.json> --output <output path> --map <map path>`

> [!NOTE]
> Any file extension for output is valid, however the norm is a `.vtgr`, and it is highly recommended to configure windows to open VTGR files with HeadlessClient automatically

The replay file can now be opened by clicking the produced file, pressing "Open With", and navigating to Headless Client.

It may be helpful to setup a simple on-build script for AIPProvider to run these two steps every time you build your code. Pressing Ctrl+R on an open HC window will automatically reload the new replay without need to close and reopen it.
