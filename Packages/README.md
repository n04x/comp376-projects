# Alice in Cyberland
COMP376 Fall 2018 Video Game Project on Unity    
![My image](/logo.png)    
### Team Members
Alexia Soucy: Art direction & assets  
Earl Steven Aromin: Project management & programming    
Jesse Tremblay: VFX implementation & QA  
Marco Tropiano: Sound design & composition  
Tan-Phat Pham: Programming & QA  
Thomas Backs: Programming & QA  
### Settings
The story takes place in a near future, in an advanced digital environment.    

### Summary
A young talented hacker named Alice who enjoys doing penetration testing, has attracted the attention of the wrong kind of people, a dangerous criminal organization who launders money through encrypted online bank transfers between their casinos. They noticed Alice’s skills which allow her to decrypt and uncover their scheme. The crime organization made an offer to her, which she refused. The crime organization doesn’t take ‘No’ for an answer, Alice has no other choice, she must save her life, she must hack into the shady organization database via their network, to uncover their schemes to the world, in order to achieve her goals, no one must stand in her way.

### Input
![My image](/xbox-controller.png)    
LT - Ranged Attack    
  * Shoot bullet in the direction Alice is facing
    
X - Melee Attack    
  * Slash attack with blade
    
RB - Shield
  * Maintain a barrier in the direction Alice is facing
  * Block a certain amount of damage before breaking, need some time to recover
    
LB - Dash    
  * Dashes in a short distance in the direction Alice is facing
  * Can be chained, depending on the number of charge left.    
  
Y - Draw    
  * Draw a new card from the deck    
  
B - Vent    
  * Vent the current player deck to get a bonus based on the cards    
  
### Known Issue    
`Assets/Script/room/PropEditor.cs(7,27): error CS0246: The type or namespace name` Editor' could not be found. Are you missing an assembly reference?    
This also `Assets/Script/room/RoomEditor.cs(7,27): error CS0246: The type or namespace name` Editor' could not be found. Are you missing an assembly reference?    
The solution for this is pretty trivial, create a folder under Assets named Editor, the path should looks like this    
`Assets/Editor`    
Then move the Editor Script into this folder in Unity3d `PropEditor.cs, PropEditor.cs.meta, RoomEditor.cs, RoomEditor.cs.meta`
