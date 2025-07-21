using UnityEngine;
// TODO: sort out the values to have an easier time finding values.
[CreateAssetMenu(fileName = "Gun", menuName = "Scriptable Objects/GunData")]
public class GunData : ScriptableObject
{
    public string weaponName;
    public Sprite gunSprite;
    public FireMode fireMode;
    public float spreadAngle;
    public float fireRate;        // For full-auto
    public float timeBetweenShots; // For Semi and Burst (in seconds)
    public int burstCount;        // For burst
    public float reloadTime;      // For sniper/reload-style
    public int maxAmmo;
    public AudioClip fireSound;
    public AudioClip chamberSound;
    public GameObject projectilePrefab;
    public enum FireMode{
        Semi,
        Auto,
        Burst,
        Sniper
    }
}
