using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFuel : MonoBehaviour
{
    DrivingScript ds;

    private void Start()
    {
        ds = transform.parent.GetComponent<DrivingScript>();
    }

    public bool Add()
    {
        if(ds.enabled)
        {
            ds.nitroFuel += 1;
            ds.SetFuelText();
            ds.nitroFuel = Mathf.Clamp(ds.nitroFuel, 0, 5);
            return true;
        }
        else
        {
            return false;
        }
    }
}
