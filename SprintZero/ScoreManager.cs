using System;

namespace SprintZero
{
    public static class ScoreManager
    {
        private static int stompCombo = 0;
        private static int shellCombo = 0;

        // Sequence arrays based on the SMB Wiki
        private static readonly int[] StompScores = { 100, 200, 400, 500, 800, 1000, 2000, 4000, 5000, 8000 };
        private static readonly int[] ShellScores = { 500, 800, 1000, 2000, 4000, 5000, 8000 };

        public static void ResetStompCombo() => stompCombo = 0;
        public static void ResetShellCombo() => shellCombo = 0;

        // Standard Points
        public static void BreakBrick(Game1 game) => game.marioScore += 50;
        public static void CollectCoin(Game1 game) => game.marioScore += 200;
        public static void CollectPowerUp(Game1 game) => game.marioScore += 1000;
        public static void KickShell(Game1 game) => game.marioScore += 400;

        public static void EnemyStomped(Game1 game)
        {
            if (stompCombo < StompScores.Length)
            {
                game.marioScore += StompScores[stompCombo];
            }
            else
            {
                game.livesCount++; // 1-Up!
                // Trigger 1-Up Sound here
            }
            stompCombo++;
        }

        public static void EnemyDefeatedByShell(Game1 game)
        {
            if (shellCombo < ShellScores.Length)
            {
                game.marioScore += ShellScores[shellCombo];
            }
            else
            {
                game.livesCount++; // 1-Up!
                // Trigger 1-Up Sound here
            }
            shellCombo++;
        }

        public static void FlagpoleReached(Game1 game, float touchHeight, float maxFlagpoleHeight)
        {
            // Simple percentage-based calculation to determine points
            float percentage = touchHeight / maxFlagpoleHeight;

            if (percentage >= 0.9f) game.marioScore += 5000;
            else if (percentage >= 0.7f) game.marioScore += 2000;
            else if (percentage >= 0.4f) game.marioScore += 800;
            else if (percentage >= 0.1f) game.marioScore += 400;
            else game.marioScore += 100;
        }

        public static void AddTimeBonus(Game1 game)
        {
            // 50 points per remaining second
            int remainingSeconds = (int)Math.Floor(game.gameTimer); // gameTimer is in seconds based on your Update loop
            if (remainingSeconds > 0)
            {
                game.marioScore += (remainingSeconds * 50);
                // Depending on how you want to implement this, you might want to drain the timer to 0 visibly 
                // in an end-of-level state update rather than all at once here.
            }
        }
    }
}