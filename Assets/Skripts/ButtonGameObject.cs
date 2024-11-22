using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonGameObject : MonoBehaviour
{
    [SerializeField] private PlayerController pc;

    public GameObject door;

    public float maxHegit;
    public bool doorIsOpening;
    // Start is called before the first frame update
    void Awake()
    {
        pc = FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (doorIsOpening)
        {
            door.transform.Translate(Vector3.up * Time.deltaTime * 5);
        }

        if (door.transform.position.y > maxHegit)
        {
            doorIsOpening = false;
        }

        gameObject.GetComponent<Outline>().enabled = pc.looksButton;
    }
}
