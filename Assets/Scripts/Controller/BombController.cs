using UnityEngine;
using Enums;
using System.Collections.Generic;

public class BombController : MonoBehaviour
{
    [SerializeField] public float bombTimer = 30f;
    [SerializeField] private Vector3 workPosition;
    [SerializeField] private Vector3 transitionOut;
    [SerializeField] private ButtonType buttonType;
    [SerializeField] private BoltType[] bolts = new BoltType[3];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        buttonType = Random.Range(0, 4) switch
        {
            0 => ButtonType.Circle,
            1 => ButtonType.Star,
            2 => ButtonType.Triangle,
            _ => ButtonType.Umbrella
        };

        for (int i = 0; i < bolts.Length; i++)
        {
            bolts[i] = Random.Range(0, 3) switch
            {
                0 => BoltType.One,
                1 => BoltType.Two,
                _ => BoltType.Three
            };
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
