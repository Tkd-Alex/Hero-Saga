public static class PlayerStats {
    /*
     * Nice static class
     * Created just for store GameStats like
     * Kills, Coins, time, Health
     * Helpful for calculate final score
     * The stats are reachable 'everywhere'
     */

    private static int kills = 0, coins = 0;
    private static float time = 0f, health = 0;

    public static int Kills {
        get { return kills; }
        set { kills = value; }
    }
    public static void IncKills(int value=1) { kills += value; }

    public static int Coins {
        get { return coins; }
        set { coins = value; }
    }
    public static void IncCoins(int value=1) { coins += value; }

    public static float Time {
        get { return time; }
        set { time = value; }
    }

    public static float Health {
        get { return health; }
        set { health = value; }
    }

    public static int CalculatePoints() {
        return (int)(((coins * 5) + (kills * 2.5)) * (time + 1));
    }
}