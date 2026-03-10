using UnityEngine;

public class LevelTriggerPhone : MonoBehaviour
{
    private bool triggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            triggered = true;
            Level1SequenceManager.instance.TriggerPhoneRinging();
        }
    }
}