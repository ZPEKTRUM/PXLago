[System.Serializable]

public class PlayerStats
{
#if UNITY_2022_1_OR_NEWER
    public string _PlayerName;
    public int _playerLevel;
    public int _PlayerHealth;
    public int _gold;
    public int _boatBonus;
    public int _boatMalus;
#elif PLATFORM_ANDROID
    string _PlayerName;
    int _playerLevel;
    int _PlayerHealth;
    int _gold;
    int _boatBonus;
    int _boatMalus;
#elif PLATFORM_SUPPORTS_MONO
     string _PlayerName;
    int _playerLevel;
    int _PlayerHealth;
    int _gold;
    int _boatBonus;
    int _boatBMalus;
#endif
}