using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurn : TacticsMove
{
	bool Alredy_atack=false;
	void Start () 
	{
        Init();
	}
	
	// Update is called once per frame
	void Update () 
	{
        Debug.DrawRay(transform.position, transform.forward);

        if (!turn)
        {
            return;
        }

        if (!moving)
        {
            FindSelectableTiles();
            CheckMouse();
        }
        else
        {
            Move();
        }
	}

    void CheckMouse()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
				if (hit.collider.tag == "Tile") {
					Tile t = hit.collider.GetComponent<Tile> ();

					if (t.selectable) {
						MoveToTile (t);
						Alredy_atack = false;
					}
				}
				else if (hit.collider.tag == "NPC" && Alredy_atack==false) {
                    Vector3 distance = new Vector3();
                    distance = hit.collider.transform.position - transform.position;
                    if (Mathf.Abs(distance.magnitude)>1)
						hit.collider.GetComponent<BaseStats> ().HP -= this.GetComponent<BaseStats> ().RangeAtack;
                    else
                        hit.collider.GetComponent<BaseStats>().HP -= this.GetComponent<BaseStats>().MeleAtack;
                    Alredy_atack = true;
                    if (hit.collider.GetComponent<BaseStats>().HP <= 0)
                    {
                        Destroy(hit.collider.gameObject);

                        
                    }
                }
            }
        }
    }
}
