|[README](../README.md)|[How to Play](how-to-play.md)|[Project Architecture](architecture.md)|[User Stories](user-stories.md)|[Change Log](change-log.md)|
|-|-|-|-|-|

# Change Log
**Version** 1.0.0

**10-31-2019:**
* Created GitHub repo
* Created scaffolding for project
* Wrote user stories
* Started working on project architecture
* Created opening "graphic" for welcome screen

**11-01-2019:**
* Created 'rough drafts' of all classes, interfaces and enums
* Organized files into folders
* Began working on welcome screen

**11-02-2019:**
* Finished welcome screen
  * Users can select number of players
* Exported display functionality to new utility class Display
* Started working on displaying the boat placing screen and functionality

**11-03-2019:**
* Made steady progess on boat placing functionality, able to move a point around the grid

**11-04-2019**
* Able to move boats around the grid including changing the boats orientation
* Able to add horizontally orientated boats to the game board

**11-05-2019**
* Fixed bug where boats weren't rendering properly when moving over other boats
* Able to add vertically oriented boats to the game board
* Able to "attack" and update game boards
* Validate user input during attack to make sure coordinates are on game board
* Added explosion.txt and miss.txt to notify users if they hit or miss during their turn

**11-06-2019**
* Added end.txt, functionality to check for win and display final screen
* Exported human turn taking functionality to HumanTurnTaker.cs
* Computer player functionality complete
* Updated documentation

**11-07-2019**
* Updated documentation