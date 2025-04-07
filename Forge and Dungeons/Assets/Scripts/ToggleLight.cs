using UnityEngine;

public class ToggleLight : MonoBehaviour
{
    private Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (myLight != null)
            {
                myLight.enabled = !myLight.enabled;
            }
        }
    }
}

