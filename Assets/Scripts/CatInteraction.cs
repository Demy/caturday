using UnityEngine;

public class CatInteraction : MonoBehaviour
{
    public float interactionDistance = 1.7f;

    public void InteractWithClosest()
    {
        Interactable closest = FindClosest();
        if (closest != null) closest.Interact(gameObject);
    }

    public Interactable FindClosest()
    {
        float dist = float.MaxValue;
        float nextDist;
        Interactable closest = null;
        Object[] objects = FindObjectsOfType(typeof(Interactable));
        Interactable interactable;
        float interactionDistanceSqr = interactionDistance * interactionDistance;
        foreach (var item in objects)
        {
            interactable = item as Interactable;
            nextDist = CalculateSqrDistance(interactable.transform.position - transform.position);
            if (nextDist < dist && nextDist <= interactionDistanceSqr)
            {
                closest = interactable;
                dist = nextDist;
            }
        }
        return closest;
    }

    private float CalculateSqrDistance(Vector3 dif)
    {
        return dif.x * dif.x + dif.z * dif.z;
    }
}