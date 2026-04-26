using System;

namespace SprintZero
{
    public static class ScoreManager
    {
        private static int stompCombo = 0;
        private static int shellCombo = 0;

        private static readonly int[] StompScores = { 100, 200, 400, 500, 800, 1000, 2000, 4000, 5000, 8000 };
        private static readonly int[] ShellScores = { 500, 800, 1000, 2000, 4000, 5000, 8000 };

        public static void ResetStompCombo() => stompCombo = 0;
        public static void ResetShellCombo() => shellCombo = 0;

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
                game.livesCount++; 
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
                game.livesCount++;
                Music.oneupSound.play();
            }
            shellCombo++;
        }

        public static void FlagpoleReached(Game1 game, float touchHeight, float maxFlagpoleHeight)
        {
            float percentage = touchHeight / maxFlagpoleHeight;

            if (percentage >= 0.9f) game.marioScore += 5000;
            else if (percentage >= 0.7f) game.marioScore += 2000;
            else if (percentage >= 0.4f) game.marioScore += 800;
            else if (percentage >= 0.1f) game.marioScore += 400;
            else game.marioScore += 100;
        }

        public static void AddTimeBonus(Game1 game)
        {
            int remainingSeconds = (int)Math.Floor(game.gameTimer);
            if (remainingSeconds > 0)
            {
                game.marioScore += (remainingSeconds * 50);

            }
        }
    }
}