using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Scriptable Objects/EnemyData")]
public class EnemyData : ScriptableObject
{
    public Sprite enemySprite;
    public AnimationClip IdleAnimation;
    public AnimationClip WalkingAnimation;
    public AnimationClip HitAnimation;
    public AnimationClip DeathAnimation;
    public AnimationClip AttackAnimation;

    public float health;
    public float movementSpeed;
    
}
