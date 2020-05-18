public static class PlayerStats {
    /*
     * Nice static class
     * Created just for store GameStats like
     * Kills, Coins, time, Health
     * Helpful for calculate final score
     * The stats are reachable 'everywhere'
     * Used also for recovery data between levels
     */

    private static int kills = 0, coins = 0, extraPoints = 0;
    private static float time = 0f, health = 0;

    private static bool attackPowerUP = false;
    private static bool defensePowerUP = false;
    private static bool doubleCoinsPowerUP = false;

    public static int ExtraPoints {
        get { return extraPoints; }
        set { extraPoints = value; }
    }
    public static void IncreaseExtraPoint(int value = 1) {
        extraPoints += value;
    }

    public static int Kills {
        get { return kills; }
        set { kills = value; }
    }
    public static void IncreaseKills(int value=1) { 
        kills += value; 
    }

    public static int Coins {
        get { return coins; }
        set { coins = value; }
    }
    public static void IncreaseCoins(int value=1) {
        coins += doubleCoinsPowerUP ? value * 2 : value ;
    }

    public static float Time {
        get { return time; }
        set { time = value; }
    }

    public static float Health {
        get { return health; }
        set { health = value; }
    }

    public static bool AttackPowerUP {
        get { return attackPowerUP; }
        set { attackPowerUP = value; }
    }

    public static bool DefensePowerUp {
        get { return defensePowerUP; }
        set { defensePowerUP = value; }
    }

    public static bool DoubleCoinsPowerUP {
        get { return doubleCoinsPowerUP; }
        set { doubleCoinsPowerUP = value; }
    }

    public static int CalculatePoints() {
        return extraPoints + (123 * ((int)(
            (coins * 7.0) +
            (kills * 6.0) +
            (health < 0 ? 1 : health * 4.0) +
            (time < 0 ? 1 : time * 3.0)
        )));
    }

    public static void ResetAll() {
        kills = coins = extraPoints = 0;
        time = health = 0f;
        attackPowerUP = defensePowerUP = doubleCoinsPowerUP = false;
    }
}