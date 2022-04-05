using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class gunController : MonoBehaviour
{
    [Header("Gun Setting")]
    public float fireRate = 0.1f;
    public int clipSize = 30;
    public int reservedAmmoCapacity = 270;

    public bool _canShoot;
    public int _currentAmmoInClip;
    public int _ammoInReserve;

    //muzzle flash
    public Image muzzleFlashImage;
    public Sprite[] flashes;

    private void Start()
    {
        _currentAmmoInClip = clipSize;
        _ammoInReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && _canShoot && _currentAmmoInClip > 0)
        {
            _canShoot = false;
            _currentAmmoInClip--;
            StartCoroutine(ShootGun());
        }
        else if(Input.GetKeyDown(KeyCode.R) && _currentAmmoInClip < clipSize && _ammoInReserve > 0)
        {
            int AmmonNeeded = clipSize - _currentAmmoInClip;
            if (AmmonNeeded >= _ammoInReserve)
            {
                _currentAmmoInClip += _ammoInReserve;
                _ammoInReserve -= AmmonNeeded;
            }
            else
            {
                _currentAmmoInClip = clipSize;
                _ammoInReserve -= AmmonNeeded;
            }
        }
    }

    IEnumerator ShootGun()
    {
        StartCoroutine(MuzzleFlash());
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
    IEnumerator MuzzleFlash()
    {
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.Length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
