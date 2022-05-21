using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fuel : MonoBehaviour
{
    Collider collider;
    Renderer renderer;

    Color colorActive;
    Color colorInactive = new Color(0.5f, 0.5f, 0.5f);

    private void Start()
    {
        collider = GetComponent<Collider>();
        renderer = GetComponent<Renderer>();

        colorActive = renderer.material.color;
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0.4f, -0.4f, 0.4f));
    }

    public IEnumerator UseFuel()
    {
        collider.enabled = false;
        renderer.material.color = colorInactive;
        renderer.material.SetColor("_EmissionColor", colorInactive);

        yield return new WaitForSeconds(10);

        collider.enabled = true;
        renderer.material.color = colorActive;
        renderer.material.SetColor("_EmissionColor", colorActive);
    }

    private void OnTriggerEnter(Collider other)
    {
        AddFuel auto = other.GetComponent<AddFuel>();
        if(auto != null && auto.Add())
        {
            StartCoroutine(UseFuel());
        }
    }
}
