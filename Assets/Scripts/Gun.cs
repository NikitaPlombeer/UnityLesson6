using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform Spawn;
    public float BulletSpeed = 10;
    public float ShotPeriod = 0.2f;
    public AudioSource ShotSound;
    public GameObject Flash;
    
    private float _timer;
    
    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > ShotPeriod && Input.GetMouseButton(0))
        {
            CreateBullet();
            _timer = 0;
        }
    }

    private void CreateBullet()
    {
        GameObject bullet = Instantiate(BulletPrefab, Spawn.position, Spawn.rotation);
        
        Vector3 bulletDirection = Spawn.forward;
        bullet.GetComponent<Rigidbody>().velocity = bulletDirection * BulletSpeed;
      
        ShotSound.Play();
        Flash.SetActive(true);
        
        Invoke(nameof(HideFlash), 0.12f);
    }

    public void HideFlash()
    {
        Flash.SetActive(false);
    }
}