using System.Collections;
using UnityEngine;

public class LevelTriggerPhone : MonoBehaviour
{
    private bool triggered = false;
    public float ringDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            triggered = true;
            StartCoroutine(DelayedRing());
        }
    }

    IEnumerator DelayedRing()
    {
        yield return new WaitForSeconds(ringDelay);
        Level1SequenceManager.instance.TriggerPhoneRinging();
    }

}