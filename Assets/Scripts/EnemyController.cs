using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public float force = 1;

    private Rigidbody2D body;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate () {
        var target = NearestPlayer();
        var direction = (target.transform.position - transform.position).normalized;
        body.AddForce(force * direction);
    }

    private GameObject NearestPlayer()
    {
        GameObject nearestPlayer = null;
        float minDistance = float.PositiveInfinity;

        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < minDistance)
            {
                nearestPlayer = player;
            }
        }
        return nearestPlayer;
    }
}
