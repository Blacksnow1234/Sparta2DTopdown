using System;
using UnityEngine;

public enum StatsChangeType
{
    Add, // 0
    Mutiple, // 1
    Override // 2
}

[System.Serializable] //데이터 폴더처럼 사용할 수 있게 만들어주는 Atribute

public class CharacterStat
{
    public StatsChangeType statsChangeType;
    [Range(1, 100)] public int maxHealth;
    [Range(1f, 20f)] public float speed;
    public AttackSO attackSO;
}