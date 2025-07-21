using Unity.VisualScripting;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    [SerializeField] private float bulletSpeed = 5f;
    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate(){
        rigidbody.MovePosition(rigidbody.position + (Vector2)transform.right * bulletSpeed * Time.deltaTime);
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag != "Player" && collision.gameObject.tag != "Enemy" ){
            Destroy(gameObject);
        } else if(collision.gameObject.tag == "Enemy"){
            DamageEnemyAndDestroyBullet(collision.gameObject);
        }
    }
    void DamageEnemyAndDestroyBullet(GameObject enemy){
        EnemyHealth eH =  enemy.GetComponent<EnemyHealth>();
        if(eH != null)
            eH.TakeDamage(1f);
        Debug.Log("Enemy" + enemy.name + " Damaged");
        Destroy(gameObject);
    }
}
