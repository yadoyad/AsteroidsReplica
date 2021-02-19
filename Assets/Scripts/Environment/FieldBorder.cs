using UnityEngine;

public class FieldBorder : MonoBehaviour
{
    public GameObject opposedBorder;
    [SerializeField] private float offset = 1f;
    [SerializeField] private bool longBorder = true;

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Player" || other.gameObject.tag == "Asteroid" || other.gameObject.tag == "UFO")
        {
            Teleport(other.gameObject);
        }
    }

    void Teleport(GameObject obj)
    {
        var tPosition = obj.transform.position;

        if(longBorder)
        {
            if(transform.position.y < 0)
            {
                tPosition.y = opposedBorder.transform.position.y - offset;
            }
            else
            {
                tPosition.y = opposedBorder.transform.position.y + offset;
            }
        }
        else
        {
            if(transform.position.x < 0)
            {
                tPosition.x = opposedBorder.transform.position.x - offset;
            }
            else
            {
                tPosition.x = opposedBorder.transform.position.x + offset;
            }
        }

        obj.transform.position = tPosition;
    }
}
