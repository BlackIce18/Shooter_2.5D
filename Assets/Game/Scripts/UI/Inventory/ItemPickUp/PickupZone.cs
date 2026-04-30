using System;
using System.Collections;
using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Pickable"))
        {
            other.enabled = false;
            StartCoroutine(Timer(other));
        }
    }

    private IEnumerator Timer(Collider other)
    {
        PickupItemUI item = other.GetComponent<PickupItemUI>();
        item.Canvas.gameObject.SetActive(true);
        item.Text.text = item.Money.ToString();
        item.Mesh.gameObject.SetActive(false);
        item.OnPickup();
        yield return new WaitForSeconds(2);
        Destroy(item.gameObject);
    }
}
