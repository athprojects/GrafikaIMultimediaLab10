using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPoints : MonoBehaviour
{

    public GameObject[] Walls;
    private GameObject _winningWall;
    public Text PointsText;
    private int _poinCount;

    // Use this for initialization
    void Start ()
    {
        RandomNewWinningWall();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DoorInsides"))
        {
            var door = other.gameObject.transform.parent.gameObject;
            UpdatePoints(door == _winningWall ? 1 : -1);
            if(door == _winningWall)
                RandomNewWinningWall();
        }
    }

    private void RandomNewWinningWall()
    {
        _winningWall = Walls[Random.Range(0, Walls.Length)];
    }

    private void UpdatePoints(int addPointCount)
    {
        _poinCount += addPointCount;
        PointsText.text = "Points: " + _poinCount;
    }
}
