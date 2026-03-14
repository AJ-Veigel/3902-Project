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
