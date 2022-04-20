
# Battleship API

A Web API written in C# .NET 6, that allows you to create a board for the battleship game, add a ship and an option to attack the ship.

# How to Install?

Download the release or compile the code with Visual Studio. Select Windows x64 for IIS or Linux x64 if you are installing on Linux using the built in Kestrel Web Server.
Download the the appropriate .NET 6 runtine for your x64 operating system.

# Install for IIS

For IIS simply extract the Windows x64 binary place it in a folder and create a new site & port within IIS and select the folder 

# Install for Linux Kestrel

You will need copy the Linux x64 binary to a folder with appropriate permissions. 
Create a new service file in /etc/systemd/system/BattleAPI.service

Example
---------------
[Unit]

Description=BattleAPI

[Service]

WorkingDirectory=/path/to/extracted/folder

ExecStart=/path/to/dotnet/folder /path/to/extracted/folder/Battleship.API.dll

Restart=always
RestartSec=10

-------

Run the service and will be available on port 5000


# Viewing Swagger

 View swagger documentation visit the /swagger/index.html directive.

# Using the API

# Setup the game

Can be accessed via the /Game/SetupGame

It takes 2 GET variables SizeX & SizeY, both are integers. You cannot use negative numbers. Min values are 3. Coordinates for the board start at 0 so with a min value of 3 the board will be 4x4 as a minimum.

If you get a Status Code of 400 you have input an invalid size.

Status 200 and you have setup the board correctly.


# Add a Small Ship to the board

Can be access via /Game/AddSmallShip. 
Small ships take a single unit on the board/grid.

It takes 2 GET variables PosX & PosY. Both are integers.

If you get a status 400 if you place the ship outside of bounds or if you place the ship on top of another ship. Look for the 'error' key in the body return for the error message.

Status code of 200 if all good!

# Strike a Position

Can be accessed via the /Game/StrikePosition

It takes 2 GET variables PosX & PosY.

Status 400 if you place the Coordinates out of bounds. Look for the 'error' key for the error message.

Status 200 if all good and look for the 'result' key and value in the body return to see if it was a HIT, HIT and Destroy or a MISS.

# Run Unit Tests

These are best run from the development enviroment. However this can be accessed via /UnitTest/RunUnitTests/

Status code of 200 all unit tests passed

Status code of 500 if there was a unit test falied the error will show in the body. However this is best reserved for running in dev enviroment of visual studio.

# Restart the game.

Simply run /Game/SetupGame again.


