using UnityEngine; // 유니티 엔진의 기본 기능을 사용하기 위한 네임스페이스

[CreateAssetMenu(fileName = "RangedAttackSO", menuName = "TopDownController/Attacks/Ranged", order =1)]

public class RangedAttackSO : AttackSO
{
    [Header("Ranged Attack Data")]
    public string bulletNameTag;
    public float duration;
    public float spread;
    public int numberofProjectilesPerShot;
    public float multipleProjectilesAngel;
    public Color projectileColor;
}