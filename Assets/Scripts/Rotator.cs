using UnityEngine;

public class Rotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Rotates object based on its starting Vector3 rotation * time passed 
        transform.Rotate(new Vector3 (15, 30, 45) * Time.deltaTime);
    }
}
