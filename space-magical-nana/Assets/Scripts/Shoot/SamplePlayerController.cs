using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is just a test script, please don't use it 
/// in a production basis
/// </summary>
[RequireComponent(typeof(WeaponManager))]
public class SamplePlayerController : MonoBehaviour
{

    private WeaponManager wm;  

    private void Start()
    {
        wm = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if      (Input.GetKeyDown(KeyCode.Q))
            wm.SwapWeapon(0);
        else if (Input.GetKeyDown(KeyCode.E))
            wm.SwapWeapon(1);
        else if (Input.GetKeyDown(KeyCode.Mouse0))
            wm.Shoot();
            
    }

}
