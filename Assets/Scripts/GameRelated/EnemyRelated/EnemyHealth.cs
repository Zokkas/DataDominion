using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private EnemyData data;
    private float maxHealth;
    private float currentHealth;
    void Awake(){
        data = GetComponent<EnemyStats>().data;
        maxHealth = data.health;
        currentHealth = data.health;
    }
    public void Heal(float healAmount){
        if(healAmount + currentHealth > maxHealth)
            currentHealth = maxHealth;
        else
            currentHealth += healAmount;
    }
    public void TakeDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            DropItem();
            Destroy(gameObject);
        }
    }
    private void DropItem(){
        Debug.Log("Loot dropped");
    }
}
