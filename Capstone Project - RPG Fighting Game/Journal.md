# Our Super Awesome Markdown Notebook

## Feb. 11, 2021
* **To-Do List Before Next Meeting:**
  * Install Unity 2020.2.3f1 on our own devices and DO NOT UPDATE IT. (Everyone)
  * Email Dr. Summet with the installation information for the Unity Engine. (Kenneth)
  * Copy over the game component information from Sprint 1 to Sprint 2 Info Dump. (Elliot)
  * Brainstorm components for game development and insert it into the Sprint 2 Deliverable Info Dump file. (Everyone)
  * Decide where to meet for the Monday meeting. (Everyone)

## Feb. 15, 2021
* **To-Do List Before Next Meeting:**
  * Begin looking at tutorials for Unity Engine and programming in C#. (Everyone)
  * Start getting use to using the markdown for logging work on the project. (Everyone)
  * Look over the game components before finalizing them at the next meeting. (Everyone) (edited) 

* **Elliot Wurst:**
Today I installed Unity 2020.2.3f1 and began setuping up repositories for later use. I copied over our group's discussion of game elements over to our Sprint 2 Deliverables document on Google Drive. I will proceed with looking over the component list for finalization when the whole group meets.

* **Kenneth Shortrede:**
Installed the newest version of Unity and searched some additional tutorials. 

## Feb. 18, 2021
* **To-Do List Before Next Meeting:**
  * Check out Unity Collab - Free Version vs Student Version
  * Test out computers in the library
  * Go over learning resources - Update Markdown
  * Start thinking about specifics for your individual tasks

## Feb. 20, 2021
* **Alexander Wehr:**
I installed Unity 2020.2.3f1 a couple days ago on my devices.  I then have continued watching Unity tutorials.  Later I got the base character model for the user player and have been testing out basic animations.  I will test out animation transition and other timing techniques
* **Oliver Luo:**
I installed Unity 2020.2.3f1 on my desktop computer at home. I had previous versions that I removed to ensure everybody is on the same page. Right now I am reviewing the bare bones basics of how to use Unity, to give myself an easier time transitioning to more content-specific tutorials.

## Feb. 21, 2021
* **Kenneth Shortrede:**
I have been watching some videos on GUI, Menu creations, and tools such as TextMeshPro.

## Feb. 22, 2021
* **Elliot Wurst:**
Today I began watching a tutorial on Scripts in Unity as well as how incorporate C# code into Unity. I learned that C# is very similar in structure to Java. 

* **Team Meeting:**
Meeting today was relatively short as no one presented any issues with their content, and everyone seems to continue to explore the learning resources we discussed. Reminder to check on Professor/IT about the Unity installations' status.

## Feb. 25, 2021
* **To-Do List Before Next Meeting:**
  * Keep studying/practicing your topics
  * Integrate first tasks at the end of 3rd sprint or first week of 4th sprint
  * Alex creates gameobjects of NPCs --> Kenneth Adds Missions/Dialogue --> Elliot pops them up in the map
  * Find if assets are available to download freely (rocks, walls, other models, etc)
  * Don't care about particle collisions
  * Add all the updates to the markdown as needed

## Feb. 26, 2021
* **Elliot Wurst:**
Today I began watching informative videos on maze generation with Kruskal's algorithm and working with nodes.

## Feb. 28, 2021
* **Kenneth Shortrede:**
I searched and found a really good asset to help out in the creation of a dialogue system for Unity. Might use it as an aid or create my own from scratch depending on time constraints. 

## March 1, 2021
* **Oliver Luo:**
I watched some more detailed tutorials on how to implement a very basic turn-based battle system. I will be looking for more tutorials relevant to making a solid foundation for our fighting system. Hopefully going to test out some snippets soon.

* **Elliot Wurst:**
I continued watching videos on maze generation and began looking into how to generate environments and objects in Unity. The tutorial videos I have been watching have not given me precisely the topics I wanted, so I will pursue articles directly from Unity.

* **Kenneth Shortrede:**
I started thinking about how to organize and plan a Mission System with the different parameters that we talked about with the team. I also looked at a couple videos talking about the topic. 

* **Alexander Wehr:**
I have been continuing to select the best animations.  Now I am trying to make the main character movement possible.  Looking into how to do the particle effects by studying particle systems.

* **Team Meeting:**
We had our usual monday meeting today to discuss the last steps to finalize the sprint. We also discussed a couple ways of doing the maze generation of walls. 

## March 2, 2021
* **Elliot Wurst:**
After researching how the maze algorithm would work, I decided to watch videos about GameObjects to learn how to generate objects that would appear in the game space. My current method for developing the maze algorithm and generating the maze is as follows:
    1. Implement a script for nodes for the maze.
    2. Establish an invisible plane of nodes.
    3. Implement a script for creating modular walls.
    4. Implement a script for generating the maze and the maze algorithm.
    5. Run the maze algorithm and create the walls in the node connections.
* **Oliver Luo:**
Started working alongside turn-based fighting system tutorials. I am planning to implement a very rudimentary fight "scene" between the player and an enemy. The skeleton of the fighting system is essentially having gamestates that describe whose turn it is, basic functions like attack or block, and having basic visual feedback that an action was performed by both the player and enemy.

## March 4, 2021
**In-Class Meeting:**
* After all of us have been looking over each of the learning resources and tutorials, we have gained more insight into the scope of our respective portions of the project. We have analyzed each portion and subdivided it into mandatory goals and stretch goals for the final learning demonstration. This list is on our Sprint 2 Brainstorming Google Doc. Overall, we are prepared to hit the ground running by beginning to implement our respective pieces of the project.
* We established that we should ensure that our scripts and other objects are as modular as possible to ensure that ease of use. Elliot said that he would make sure that the maze would have easily-interchangeable data values for the various variables for flexibility.

## March 5, 2021
* **Oliver Luo:**
  * I have made some decent headway into the core functions of our battle system, including game states, a working fighting system GUI, and the default Mixamo models that Alex is going to be using. ![image](https://user-images.githubusercontent.com/32621227/110193906-b9f8ee80-7e04-11eb-91e9-7c38ec498081.png) A lot of this was based on Brackey's turn-based combat tutorial on YouTube: https://www.youtube.com/watch?v=_1pz_ohupPs&t=458s However, there will be some obvious changes because our game will be in 3D rather than 2D.

  * I also went ahead and edited the gitignore file in our main repository to the official github unity gitignore standards, as that will allow github to ignore any meta/hidden files that don't need to be accounted for when pushing and pulling. Furthermore, I have added my own branch of this starting point labeled "oliver" using the github desktop application.

  * My next goal is to continue on with the core functionality of the battle system and try to coordinate with the animator/modeller (Alex) for the movement and animations.

## March 7, 2021
* **Elliot Wurst:**
  * I began working on figuring out how to create GameObjects using C# scripts. I was able to create a cube that I was able to manipulate the dimensions of. This script will be used for creating individual walls that will be used later for creating the maze walls. I developed a Wall and Node class for the maze generation. 
  * My next goal is to create the nodes for the maze and create a plane to set the position of the nodes. From there, I will attempt to spawn the cube at the nodes to test it out.

## March 8, 2021
* **Oliver Luo:**
  * Added a mana bar for the player (no functionality yet). 
  * I need to figure out the following things:
    * Adding a victory screen
    * Adding a working EXP system
    * Adding items
    * Maybe a guard button to reduce the impact of next attack
    * More attack types (randomized) for both the player and enemy
    * Animations when performing actions like attacking, receiving damage, healing, etc.
    * Highlighting the current unit's silhouette on their turn (visual feedback for player)
    * Moving the units towards each other when attacking

* **Kenneth Shortrede:**
* Finished planning and implemented a basic mission system. This includes the varied connections between the following scripts:
  * NPC Interactions: controls the "brain" of the NPC, how it is goign to talk, and whether it has a mission available. It will trigger the correct conversation depening on the state of the mission (if any) or the current state of the NPC itself.
  *  Mission: parent class for all the different mission classes. It has some basic features such as references to the GUI needed to show messages, the title and description of the mission, the rewards for finishing the mission, a list of dialogues, a reference to the player, and the state of the mission.
  *  Player: the player class will have a list of Missions currently active, and variables to keep track of the current open dialogue with the different NPCs. It will also detect NPCs in the range with an OnTriggerEnter in order to start dialogue. It contains methods such as: adding a mission to list, removing a mission from list, adding a kill, adding a completed delivery, and showing a message.
  *  MissionDelivery: subclass for the Mission parent. 



## March 10, 2021
* **Oliver Luo:**
  * I have added a crude mana system. Here's a gif of it in action: https://imgur.com/a/0K6yvoP
  * I am thinking of implementing a "CharacterSkills" script to handle all the nitty gritty numbers when it comes to skill usage for the character, and maybe separate scripts for different enemies. For example, this would make it so that I could modify the heal amount of using the Heal skill separately from the battle system. This could also be better from an architectural standpoint if we decide to hammer in a true EXP system, as the healing amount would have to scale with the player's level. 
  * In addition to character skills, it would probably be necessary to have a CharacterStats script to handle the player's combat attributes such as ATK, HP at the most basic, and maybe DEF and SPD if we have time. I don't want to go too crazy with this as it can get really complicated really quickly, so the former 2 stats might be a good starting point when it comes to a level up system.
  * I am still working on the long list of stuff from March 7th. I am adding camera work as a very low priority idea as well, so that it tracks the movements of whoever's performing an action currently.
  * In short, I need to determine how I will plan out the fighting system from a design perspective from now on, now that the core fundamentals are for the most part done.

## March 11, 2021
*  **Alexander Wehr:**
   *  I added movement and basic animation (also completed the colliders).  I also made the first person Camera for the player.  Now I am busy now making sure the animation and movement sync up together.  I am still reading up on particle systems

## March 14, 2021
* **Elliot Wurst:**
   * I was successfully able to implement the Recursive Division Maze Algorithm by generating a grid of cells and assigning the whether a wall existed in reference to that cell. My next goal is to create an empty room with Wall objects in the Unity Engine. From there, I will translate the grid produced from the recursive division maze algorithm to a format that is compatible with the node plane system developed prior.

* **Kenneth Shortrede:**
   * I finished the implementation of a Delivery type mission Script. The MissionDelivery extends the Mission class, and has some additional features such as the gameobject that will receive the package, and a checker to see if mission is finished on delivery. 

## March 15, 2021
* **Oliver Luo:**
   * https://imgur.com/a/l229yoX shows a clip of two things: some crude animations and more importantly -- stat persistence.
   * I added persistence to the player's basic stats including HP, Mana, and LVL. This essentially means that regardless of how many times you switch in and out of the World scene and Fighting scene instance, the player's information will not be lost. This was through the use of static variables for data members in the player script, while using regular non-static variables in the enemy instances. 
   * I also played around with the animator, as while I'm not the animator I still have to be able to modify animation sequences for the various actions that can occur within the fighting scene. 
   * My next goal is to start drawing up a level up/experience system. To do this, I will probably need a wider variety of enemies to test with. One direct way is to have a separate prefab for each enemy, like Zombie prefab, Dragon prefab, Skeleton prefab, etc. I can then perhaps use a single script to handle enemy skills. From the BattleSystem script I have now, it will draw from the EnemyAction script (or whatever I'll call it) and firstly determine which enemy type it is via an enum. Based on that, the EnemyAction script will set the HP, Mana, Level, and Exp that can be gained and instantiate that enemy. 
* **Elliot Wurst:**
   * I was able to successfully translate the values from the grid that was generated by the Recursive Division algorithm into the values need to connect the nodes of the wall node plane. I connected the nodes that corresponded to the direction that the grid outputted for each cell. After that, I created an empty room where the maze would be housed then created the physical objects that would make up the maze. I also created a way to create a floor based on the dimensions of the maze. The process for generating the maze was relatively quick for my computer, however I may need to look into improving performance if it does not run as well on other systems. Here is a picture of the maze after being generated.
   * https://imgur.com/a/riBpeQJ

## March 16, 2021
* **Oliver Luo:**
   * https://www.youtube.com/watch?v=MpUTkzQk7NI 
   * The above link simulates encountering different enemies in the maze (Elliot). Clicking one of the Encounter buttons is akin to the main player colliding with a specific enemy. So for example clicking the "Encounter Zombie" button represents touching a zombie in the maze. Clicking the button in the World scene will set a static int variable to a specific # index, which will be accessed by the Fighting scene's battle system. The battle system looks at the enemy's prefab's children, which represent specific enemy types, and matches the index # from the World encounter to the corresponding index of the correct enemy type. This is great because this means I just have to make a single scene for the "Fighting Scene" instead of making separate scenes for each enemy. This demonstrates persistence between the World scene enemy, and the Fighting scene enemy.
   * I have also simplified the implementation of each enemy's unique stats. Simply attach the same Enemy.cs script component across all prefabs, but of course each prefab enemy will have different statlines (HP, Mana, Exp, etc.).
   * With all of this in mind, I should be drawing up a crude exp/level up system like previously-mentioned.

* **Kenneth Shortrede:**
   * Planned out a Saving System that incorporates the different parts of information and data needed in order to save the complete state of the game and recover it whenever the player comes back and load.
   * Planned different classes to gather and save the information saved: NPCData, PlayerData, MissionData
   * Planned a SaveSystem to control the information it received, save it to the system, and be able to load it and assign it to the necessary objects. 

## March 18, 2021
*  **Kenneth Shortrede:**
    * Developed the necessary classes to take in the information that is going to be needed to keep the state of the game.
    * PlayerData hold the position, a list of MissionData, experience, gold, and enemies killed.
    * MissionData holds the name of the mission, the name of the NPC that holds the mission, and its state (Not started, started, completed, finished). 
    * NPCData will hold the position of the NPC, it's current active mission, the state of whether there are any missions available, etc.

## March 19, 2021
*  **Kenneth Shortrede:**
    * Developed the SaveSystem static class that will be accessed whenever the User saves or loads the game. This can be done from the Main Menu or the Pause Menu only. There is abstraction so that other objects don't have to worry about saving or loading their own information.
    * Save System is composed of four methods: 
     - SavePlayer: saves the information strictly related to the player, it's locations, stats, and missions.
     - SaveNPC: saves the information related to the NPCs in the scene.
     - LoadPlayer: returns the informaiton from the file and creates a PlayerData object to then assign the information to the correct places.
     - LoadNPC: return the information from the file and creates a NPCData object to then assign the information to the correct places.
    * The Save System uses Binary Formatter to save the information in such a way that is hardly accesible by the user and can keep the state of the game without worrying about external modifications.    

## March 21, 2021
* **Alexander Wehr:**
   * Submitted my completed character model with respective animations, controls, physics, and camerawork.  Elliot will test it out in his maze.  Working on debugging integration errors and making the particle system.

## March 22, 2021
* **Oliver Luo:**
   * I added a crude level up system. There is also an exp bar on the bottom right to let the player know how much exp they need to level up. The amount of exp needed to level up is based on a mathematical formula.
   * Fixed the status bar visuals (hp, mana, exp) using image objects instead of the built in Unity sliders.
   * I need to work on adding the following:
       * Adding a speed component to both the enemy and player
       * Figuring out how to use Animation events to seemlessly transition from animations that involve 2 things like if a player attacks an enemy
       * Make it so that the fighters move (run) to the other before doing their attack animation. This will require moving + animating at the same time
       * Make enemy UI "stick" to the enemy model, instead of being plastered as a 2D UI element on the top left of the screen

*  **Kenneth Shortrede:**
    * Added additional NPCs to the scene and planned out new Missions to add in the design of the main scene.
    * Debugged and took out some errors on the Saving and Loading System    


## March 24, 2021
* **Elliot Wurst:**
   * I implemented a grid system for the maze that allows for populating the maze with the player, enemies, and the exit door. I initially tested it with cubes in order to test whether the objects were placed properly or not. Depending on the object, I had to add a height offset so that it would not be in the middle of the ground. I also moved the placement of the floor below y = 0 for easier translation of positions when spawning an object within the maze.

## March 25, 2021
* **Kenneth Shortrede:**
   * Worked on different ways to keep information across scenes, especifically focused on the misison system that the user would need to have access once he gets into the maze. 
   * Faced issues when taking the Mission Objects across the scenes to the Maze

## March 26, 2021
* **Kenneth Shortrede:**
   * Change the implementation to keep only the name and description of the active missions across scenes, and not the entire Missionobject that is not needed in the maze. This way, the user can see the information needed to solve the different quests, without having to load all the objects that are not needed.

## March 27, 2021
* **Kenneth Shortrede:**
   * Implement a system to save all the kills done in the maze in a Dictionary<EnemyType, int> to have access to them when coming out of the maze and update the necessary missions for the player to fulfill.

## March 28, 2021
* **Oliver Luo:**
   * I added smooth movement in the form of translating from point A to point B and vice versa whenever an enemy goes up to attack a player, instead of the enemy teleporting right up to the player's face and attacking. 

## March 31, 2021
* **Meeting:**
   * Our group met up for 3 hours to compile a version 1.0 of the game. We are able to integrate all of the components of our project together, as buggy as they were. We pushed this to the main project in GitHub. We created additional tasks for one another in order to ensure that the components are properly functioning and posted them in our Slack group.

## April 1, 2021
* **Oliver Luo:**
   * The level up system now updates the player's HP, Mana, and Speed stats instead of just simply adding EXP. 

## April 2, 2021
* **Kenneth Shortrede:**
   * Day 1 of Design of the Mission Scene to change basic geometrical models for real models of characters, buildings, etc. 

## April 4, 2021
* **Kenneth Shortrede:**
   * Day 2 of Design of the Mission Scene to change basic geometrical models for real models of characters, buildings, etc. 

## April 5, 2021
* **Elliot Wurst:**
   * I added two different stones textures to the floor and the walls of the maze to give it more depth. I also implemented the randomized spawning of enemies as well as a placeholder for other objects that could be spawned in.
   * I started working on fixing the maze persistence issue that we have. Currently, the maze resets after every battle with any enemy. I implemented a list that stores all relevant GameObjects. The current theory as to how this might work is to make a clone of the GameObjects that are in the physical world then insert them back into the world after loading the maze scene again.

* **Kenneth Shortrede:**
   * Day 3 of Design of the Mission Scene: added collisions to prevent the player from going outside of the map. Added some new missions to NPCs.  

## April 5, 2021
* **Oliver Luo:**
   * Added music, sound effects and 2 additional enemy types. GUI has also been revamped to be easier to read with text on top of the health/mana/exp bars. 

## April 7, 2021
* **Elliot Wurst:**
   * I solved the maze persistence issue that we have! I continued with the same idea of storing the relevant GameObjects in a list. However, before I stored it in the list, I used DontDestroyOnLoad(...) on the GameObject. That way, whenever it goes to the Fight Scene, the GameObjects persist. In order to ensure that the player doesn't spawn on an enemy again, I used Destroy(...) on the enemy after the player defeats it. Whenever the player exits the maze via the exit door, I reset the maze by destroying every GameObject in the static list and empty the list.
   * One caveat of this implementation is that I had to move where the Fight Scene took place because the maze would clip into it. I moved the physical platform where the fight happens out of view of the maze. 

## April 9, 2021
* **Kenneth Shortrede:**
   * Added new missions
   * Corrected bugs and tried out different dialogue paths
   * Solved problem with the display of multiple active missions

## April 10, 2021
* **Oliver Luo:**
   * Added a new skill that makes even greater use of Animation Events that's used by the player. GUI has further been improved by removing any superfluous buttons.
   * Some enemies can now heal as part of their skill repetoire -- also utilizing unique animation events that match up with sound effects.

## April 11, 2021
* **Elliot Wurst:**
   * I replaced the cube placeholder in the maze with two prefabs that resemble debris or rubble within the maze. I also randomized the starting orientation of all GameObjects as well as their offset from the node position so they look more natural.
   * I integrated my code with Oliver's expansion of the enemy list as well as updates to the overall fighting scene GUI.
   
* **Oliver Luo:**
   * Integrated Alex's blood particle system with my fighting system -- whenever a player or enemy attacks, a blood particle effect will emit at the attacked location.

## April 12, 2021
* **Alexander Wehr:**
   * I finally fixed the camera integration issue, and now the camera perspective can successfully transition between third and first person (by moving the camera controller object into the main character).  
   * The "blood effect" particle system is completed and successfully implemented into project.  Now I'm seeing if I can improve on other effects.

## April 15, 2021
* **Oliver Luo:**
   * I fixed some edge cases for the level up system -- now it can handle situations where you can level up multiple times from a single encounter. 
   * I added a level up screen to show the player what stats are increased and by how much.
   * I added a camera option to toggle between the player's shoulder view and a general "field" view
   * I dded QoL text features like highlighted text for damage values, mana usage, and health recovery in the action text box.

* **Alexander Wehr:**
   * Collaborated with Elliot and fixed the "infinite jump" issue impacting the player in the maze scene.  (Changed the character Groundmask setting from "Default" [which consequently resulted in the game physics thinking the main character was on ground at all times] to "Ground" and set the maze and debris to the "Ground" Layer.)
