///
/// Author: Christopher Beck
/// Description: This class can help boost performance by disableing lights that are far away from the player.
///              One can also adjust the delay between each call to the UpdateLights function, to reduce its performance impact on the game
/// ==============================================
/// Changelog:
/// 06/20/2020 - Christopher Beck - Added class
/// ==============================================
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightHandler : MonoBehaviour
{
    /// <summary>
    /// Lights that are farther away from the player than this distance will be disabled
    /// </summary>
    [SerializeField]
    private float maxDistance;

    /// <summary>
    /// Time between each check, the larger the better the performance
    /// </summary>
    [SerializeField]
    private float delay;

    private GameObject player;
    private Component[] lightComponents;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lightComponents = FindObjectsOfType<Light>();
        InvokeRepeating("UpdateLights", 0.0f, delay);
    }

    /// <summary>
    /// Checks the distance between each light and the player and enables/disables the light according to the maxDistance setting
    /// </summary>
    void UpdateLights()
    {
        foreach (Light light in lightComponents.Where(l => l != null))
        {
            if (Vector3.Distance(player.transform.position, light.transform.position) > maxDistance)
            {
                light.enabled = false;
            }
            else
            {
                light.enabled = true;
            }
        }
    }
}
