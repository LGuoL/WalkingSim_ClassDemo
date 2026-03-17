using System.Collections;
using UnityEngine;

public class LevelTriggerPhone : MonoBehaviour
{
    private bool triggered = false;
    public float ringDelay = 2f;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered PhoneAreaTrigger: " + other.name);

        if (triggered) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            Debug.Log("Player entered PhoneAreaTrigger.");
            triggered = true;
            StartCoroutine(DelayedRing());
        }
    }

    IEnumerator DelayedRing()
    {
        Debug.Log("Waiting to ring...");
        yield return new WaitForSeconds(ringDelay);

        Debug.Log("Calling TriggerPhoneRinging()");
        Level1SequenceManager.instance.TriggerPhoneRinging();
    }
}