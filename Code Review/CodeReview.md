// Sprint 2 Reviews

•	Author of the code review : Madison Gysan
•	Date of the code review:2/23/2026
•	Sprint number : Sprint two
•	Name of the .cs file being reviewed : FireMario.cs
•	Author of the .cs file being reviewed : Sam
•	Number of minutes taken to complete the review : 10 minutes 
•	Specific comments on what is readable and what is not : 
     The code is very readable. The variable names all correspond to the related usage. There could be simplification of  variable instalization. I.e. private readonly TextureRegion standingLeftSprite, standingRightSprite, jumpingLeftSprite, jumpingRightSprite, crouchLeftSprite,CrouchRightSprite; The comments are nice to fully understand what is going on within the code. The method names are very clear to what the functionality is. 

•	Author of the code review: Madison Gysan
•	Date of the code review: 2/23/2026
•	Sprint number: Sprint two
•	Name of the .cs file being reviewed : OneUp.cs
•	Author of the .cs file being reviewed : Sam 
•	Specific comments on code quality : The code is very clean, and everything makes sense. The methods are very short and readable. 
•	A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 
     One hypothetical change that could be made could be the location of the item due to the block location it is hidden in. This implementation would support this change. This would just be changing the location value. For example, location = new Vector2(500,500), the 500’s would change depending on block. 

•	Author of the code review : AJ Veigel
•	Date of the code review: 2/23/2026
•	Sprint number : Sprint two
•	Name of the .cs file being reviewed : Goomba.cs
•	Author of the .cs file being reviewed : Adam
•	Number of minutes taken to complete the review : 10 minutes 
•	Specific comments on what is readable and what is not : 
     The code is readable with variable names corresponding to what they would be used for, such as goombaWalk1Sprite being one of the sprites for the goomba walking and so on. There is a lack of comments however all of the methods and variables are well enough named to           understand what they would be used for, though for simplifications, for the double if statements in update, potentially you could use the trinary operator to replace them, though this could lead to unkowns with how people can read it that are unfamiliar with             it.

•	Author of the code review: AJ Veigel
•	Date of the code review: 2/23/2026
•	Sprint number: Sprint two
•	Name of the .cs file being reviewed : Goomba.cs
•	Author of the .cs file being reviewed : Adam
•	Specific comments on code quality : The code is very clean, and everything makes sense. The methods are short and able to be read properly. 
•	A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 
     A Hypothetical change to the potentially using a list of some kind for all of the different sprites as they would be easily accessed and stored there, though this could cause problems with reuseability for certain enemies that could be implemented later that would           be unable to understand why you choose specific points in the list every time for readability.

// Sprint 3 Reviews

•	Author of the code review: Adam Novak
•	Date of the code review: 3/13/26
•	Sprint number: Sprint three
•	Name of the .cs file being reviewed : Koopa.cs
•	Author of the .cs file being reviewed : Alex
•	Specific comments on code quality : The code is generally easy to understand (variable names are fine, everything seems logical, etc.). I also like the use of the helper method HandleTimer outside of the Update method, in order to clean up the update method. LoadTextures looks super long, but I don't know if there's a more efficient way to load the textures in besides doing what Alex did.
•	A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 
     A hypothetical change would be to align it a bit more with the IEnemy and enemy collision stuff that I wrote, in order to make it work as the Goomba stuff does. I don't think it would be a particularly difficult change to implement.

•	Author of the code review: AJ Veigel
•	Date of the code review: 3/13/2026
•	Sprint number: Sprint three
•	Name of the .cs file being reviewed : PlayerBlockCollision.cs
•	Author of the .cs file being reviewed : Madison
•	Specific comments on code quality : The code given is clean and is a helper class for all the block collisions with mario. There are 2 main methods with one checking the collision and one that checks the collision side.
•	Specific comments on code readability : The code has many different variables which are all easy enough to discern, with each collision having a specific side that it deals with denoted by CollisionSide.Top etc.
•	A hypothetical change to make is eventually we plan to update the collision detection from rectangle to the hitbox class, but since the overwhelming majority of collisions are with rectangles right now it should be fine.
     Another Hypothetical change to make is to increase the cases for the collision sides, as currently each block HAS to have a collision side rather than having a "none" collision side which would have no collision detection, which could help in later cases in which mario does not collide with a block, it would default to the collisionside bottom which would lead to false positives and collisions when there should not be.
//Sprint 4 Reviews
 
 Author of the code review: Madison Gysan
 Date of the code review: 4/13/2026
 Sprint number: 4
 Name of the .cs file being reviewed: Fireball.cs 
 Author of the .cs file being reviewed: Sam & Adam 
 Number of minutes taken to complete the review: 20 mins
 Specific Comments on what is readable and what is not:
 -The code is very readable. Comments can probably deleted in the future as we already know must of the functionality. Check to see if there any way you could bring down the number of lines used to initializing variables. For example, could combine a few to be like private con float X_SPEED = 6f, GRAVITY = 0.5F, BOUNCE_VELOCITY = -6F; Besides that everything looks good! 

Author of the code review: Madison Gysan
Date of the code review: 4/13/2026
Sprint number: 4
Name of the .cs file being reviewed: Level1.cs
Author of the .cs file being reviewed: AJ & Alex
Specific comments on code quality: 40 mins 
     The code quality is very good. Everything makes sense logically to be included.  A hypothetical change to make here would be clean up the code. Get rid of the commented out code if no longer needed. Since we are nearing the end of the course, you could get rid of the comments since we know how everything works now. The naming convention looks fine. The only note here is you may want to double check all the variable ensuring the naming convention all match each other. For example, you have filename rather than fileName.  I also have notices that this.BGColor = Color.AliceBlue is not actually affecting our background color. If we want to switch the color to AliceBlue, I believe the change needs to be made in game1 rather than this file. However, Alice blue is closer to a bright blue-white color, rather than the color we have now. 

 Author of the code review: Adam Novak
 Date of the code review: 4/13/2026
 Sprint number: 4
 Name of the .cs file being reviewed: QuestionMarkItem.cs
 Author of the .cs file being reviewed: AJ
 Number of minutes taken to complete the review: 20
 Specific Comments on what is readable and what is not:
 -The code quality is generally good; everything does make logical sense. There are a lot of else if branches in the conditional statement in the OnCollision method; a helper method /could/ be written to make OnCollision shorter, but I believe that it's fine as it is for the moment. The item blocks work as intended; mushrooms and fire flowers appear upon contact with the bottom of the item blocks, and they enter an inert state after they are hit. 

 Author of the code review: Sam Bixel
Date of the code review: 4/13
Sprint number: 4
Name of the .cs file being reviewed: TileMap.cs
Author of the .cs file being reviewed: AJ & Alex
Number of minutes taken to complete the review: 25
Specific comments on what is readable and what is not:
     The code is generally very readable and easy to follow. The class has a clear purpose, which is storing and accessing blocks by tile position, and the method names make it obvious what each method does. Methods like addBlockAt, removeBlockAt, getBlockAt, and getAllBlocks are straightforward and easy to understand. The looping structure in getBlocksInRectangle, Draw, and Update is also readable because it is consistent and uses descriptive variable names like leftTile, rightTile, topTile, and bottomTile. I don't think there is really anything to complain about in terms of readability of this file, it is straightforward and easy to follow everywhere.

Author of the code review: Sam Bixel
Date of the code review: 4/13
Sprint number: 4
Name of the .cs file being reviewed: QuestionMarkHit.cs
Author of the .cs file being reviewed: AJ & Madison
Specific comments on code quality:
     The code quality is good overall and the class is pretty easy to understand. It keeps the main behavior of the hit question mark block in one place, including the bounce, sprite updates, and collision handling. The variables like isHit, movingUp, and movingDown make it clear what state the block is in. One thing that could be improved is replacing numbers like 20f, 3f, and 0.0 with named constants so the code is easier to adjust later.

A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change
     A hypothetical change that would switch up the flow of classic Mario would be making the question mark block reset after some amount of time so it could be hit again later. The current implementation would not support that very easily, because once isHit becomes true, the block stays in that state and pauses on the final frame. To support that change the class would need extra logic for a timer and a way to switch the block back to its unused state.
