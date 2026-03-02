In the plaintext file for a readability review, include the following information:
•	Author of the code review : Madison Gysan
•	Date of the code review:2/23/2026
•	Sprint number : Sprint two
•	Name of the .cs file being reviewed : FireMario.cs
•	Author of the .cs file being reviewed : Sam
•	Number of minutes taken to complete the review : 10 minutes 
•	Specific comments on what is readable and what is not : 
     The code is very readable. The variable names all correspond to the related usage. There could be simplification of  variable instalization. I.e. private readonly TextureRegion standingLeftSprite, standingRightSprite, jumpingLeftSprite, jumpingRightSprite, crouchLeftSprite,CrouchRightSprite; The comments are nice to fully understand what is going on within the code. The method names are very clear to what the functionality is. 

In the plaintext file for a code quality review, include the following information: 
•	Author of the code review: Madison Gysan
•	Date of the code review: 10 minutes
•	Sprint number: Sprint two
•	Name of the .cs file being reviewed : OneUp.cs
•	Author of the .cs file being reviewed : Sam 
•	Specific comments on code quality : The code is very clean, and everything makes sense. The methods are very short and readable. 
•	A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 
     One hypothetical change that could be made could be the location of the item due to the block location it is hidden in. This implementation would support this change. This would just be changing the location value. For example, location = new Vector2(500,500), the 500’s would change depending on block. 

     In the plaintext file for a readability review, include the following information:
•	Author of the code review : AJ Veigel
•	Date of the code review:2/23/2026
•	Sprint number : Sprint two
•	Name of the .cs file being reviewed : Goomba.cs
•	Author of the .cs file being reviewed : Adam
•	Number of minutes taken to complete the review : 10 minutes 
•	Specific comments on what is readable and what is not : 
     The code is readable with variable names corresponding to what they would be used for, such as goombaWalk1Sprite being one of the sprites for the goomba walking and so on. There is a lack of comments however all of the methods and variables are well enough named to           understand what they would be used for, though for simplifications, for the double if statements in update, potentially you could use the trinary operator to replace them, though this could lead to unkowns with how people can read it that are unfamiliar with             it.

In the plaintext file for a code quality review, include the following information: 
•	Author of the code review: AJ Veigel
•	Date of the code review: 10 minutes
•	Sprint number: Sprint two
•	Name of the .cs file being reviewed : Goomba.cs
•	Author of the .cs file being reviewed : Adam
•	Specific comments on code quality : The code is very clean, and everything makes sense. The methods are short and able to be read properly. 
•	A hypothetical change to make to the game related to file being reviewed and how the current implementation could or could not easily support that change 
     A Hypothetical change to the potentially using a list of some kind for all of the different sprites as they would be easily accessed and stored there, though this could cause problems with reuseability for certain enemies that could be implemented later that would           be unable to understand why you choose specific points in the list every time for readability.
