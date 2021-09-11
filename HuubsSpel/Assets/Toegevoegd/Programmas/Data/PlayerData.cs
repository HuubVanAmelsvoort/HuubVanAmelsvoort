using System.Collections;
using System.Collections.Generic;
using Scripts.Characters;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;
    public int currentHealth;
    public int maxHealth;
    public int damage;
    public float movementSpeed;
    public float[] position;
    public Statistics TotalStats;
    public Statistics stats;
    public int PerfectRuns;

    public PlayerData()
    {
        Player player = Player.Instance();
        TotalStats = player.totalStats;

        level = player.attributes.Level;

    currentHealth = player.attributes.CurrentHealth;
        maxHealth = player.attributes.MaxHealth;

        damage = player.attributes.Damage;

        movementSpeed = player.attributes.MovementSpeed;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        stats = player.stats;
        if (TotalStats != null)
        {
            TotalStats.npcsDefeated += stats.npcsDefeated;
            TotalStats.secretsFound += stats.secretsFound;
            TotalStats.itemsUsed += stats.itemsUsed;
            TotalStats.score += stats.score;
        }
        
        if(stats.isPerfectRun())
        {
             PerfectRuns += 1;
        }
    }
}