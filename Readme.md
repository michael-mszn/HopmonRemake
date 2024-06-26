 ![HopmonReimaginedAlphaLogo](https://github.com/michael-mszn/HopmonRemake/assets/74096536/7ac841a7-a54b-4ed1-8770-4cd50f229405)

# Hopmon Reimagined

A remake in the C# Unity engine of the original game "Hopmon", made by Saito Games in 2002. 
Hopmon Reimagined is a Single Player 3D Action Puzzle Game that requires creativity and strategy to solve levels.

![image](https://github.com/michael-mszn/HopmonRemake/assets/74096536/17c456fd-e003-4b92-96e0-48a8d86828f5)

**This game is still actively in development.**

# Gameplay

- Find all crystals hidden in the level and return them to your base.

- Defeat dangerous monsters with an energy ball by pressing R or tools in the levels. Once your health reach 0, it is Game Over and the level needs to be restarted.

- The more crystals you carry, the slower you will walk. Be mindful when it is time to return to base!

- Currently, there are 2 playable levels.

- Rotate the camera with Q to the left, or E to the right. Sometimes a different perspecive can be insightful!

- Can't figure out the solution? Simply return later! Hopmon Reimagined saves all your progress.

# Create your own Levels

Hopmon Reimagined allows you to create your own levels over a plug-and-play text file and share them with your friends.

**No programming knowledge required!**

1. Head to the directory of the game's executable and follow this file path: `\HopmonRemake_Data\StreamingAssets\Levels`

2. Locate the file named `TEMPLATE` and make a copy of it.

3. Rename your copy to `Level[levelnumber]` Do not add the brackets.

4. Open your new file. You can start replacing the empty tiles now to your likings. Refer to the following legend:

   ![LevelEditorCheatSheet](https://github.com/michael-mszn/HopmonRemake/assets/74096536/7fd9bf32-15fc-42bb-80c7-b5438260d8e3)

   

6. Each tile can host an optional passenger. Declare a Passenger suffix after making a `|` straight line.
   
   For example, you want a Floor Tile with a Cookie Monster on it. The declaration for this would be `FL|CM`.

   An example level can look like this:
   
   ![image](https://github.com/michael-mszn/HopmonRemake/assets/74096536/5eae5f0b-ccfc-47d1-b42d-aa8a83757699)


8. Head ingame to check out your newly created level. Do not worry about making mistakes! Hopmon Reimagined will replace
   invalid codes with an empty `-----` tile.

# Requirements

- Unity Hub
  
- Unity Editor (recommended version: 2022.3.24f1)

# How To Build An Executable and Run

- Clone the repository on your computer

- Import the project into Unity Hub (**Projects > Add**)

- Open the project in Unity Hub

- Click **File > Build Settings** inside the Unity Editor

- Select **Windows, Max, Linux** as Platform

- Click on **Build** and select the location for the executable

- Once Unity finished building, head to the designated location and run the executable
