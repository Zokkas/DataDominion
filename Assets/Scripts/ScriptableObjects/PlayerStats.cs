using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public int Level = 1;
    public int CurrentExp;
    public int Coins;
    public int EnemiesKilled;
    public int BossesKilled;
    public int RunsCompleted;
}
