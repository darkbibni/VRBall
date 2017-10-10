using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour 
{
	public Vector2 TimeMinMaxSpawn;
	public List<SpawnCaract> AllSpawn;

	int timeBeforeSpawn;

	int saveLast = 0;
	int getCurr;

	void Awake ( )
	{
		timeBeforeSpawn = (int) Random.Range ( TimeMinMaxSpawn.x, TimeMinMaxSpawn.y );
	}

	void Update ( )
	{
		getCurr = ( int ) Time.timeSinceLevelLoad;

		if ( getCurr % timeBeforeSpawn == 0 && saveLast != getCurr )
		{
			timeBeforeSpawn = (int)Random.Range ( TimeMinMaxSpawn.x, TimeMinMaxSpawn.y );
			saveLast = getCurr;

			newSpawn ( );
		}
	}

	void newSpawn ( )
	{
		List<SpawnCaract> getAllSpawn = AllSpawn;
		Transform getSpawnPos;
		GameObject getNewObj;
		Rigidbody getNewRig;
		Vector3 getSizeParent;
		Vector3 sizeNewOjb;

		int a;
		int b;
		int c;
		bool objSpawn;

		for ( a = 0; a < getAllSpawn.Count; a++ )
		{
			if ( getAllSpawn [ a ].SpawnEnable )
			{
				for ( b = Random.Range ( 2, 5 ); b > 0; b-- )
				{
					objSpawn = false;

					do
					{
						for ( c = 0; c < getAllSpawn[ a ].ObjAttached.Count; c ++)
						{
							if ( Random.Range ( 0, 101 ) < getAllSpawn[a].ObjAttached[ c ].PourcSpawn)
							{
								objSpawn = true;

								getSpawnPos = getAllSpawn[ a ].SpawnPos;
								getNewObj = (GameObject) Instantiate ( getAllSpawn[ a ].ObjAttached[ c ].ObjectPref, getSpawnPos);

								sizeNewOjb = getNewObj.GetComponent<MeshRenderer>().bounds.size;
								sizeNewOjb = new Vector3 ( sizeNewOjb.x /2, sizeNewOjb.y / 2, sizeNewOjb.z / 2 );

								getSizeParent = getSpawnPos.GetComponent<BoxCollider>().size;
								getSizeParent = new Vector3 ( getSizeParent.x / 2 - sizeNewOjb.x, getSizeParent.y / 2 - sizeNewOjb.y, getSizeParent.z / 2 - sizeNewOjb.z );
								getSizeParent = new Vector3 ( Random.Range ( -getSizeParent.x, getSizeParent.x ), Random.Range (-getSizeParent.y, getSizeParent.y ), Random.Range (-getSizeParent.z, getSizeParent.z ) );

								getNewObj.transform.localPosition = getSizeParent - getSpawnPos.up;

								getNewRig = getNewObj.GetComponent<Rigidbody> ( );
								getNewRig.velocity = -getSpawnPos.up * Random.Range ( 0, getAllSpawn[ a ].ObjAttached[ c ].MaxStartSpeed );

								break;
							}
						}
					} while( !objSpawn );
				}
			}
		}
	}
}

[System.Serializable]
public class SpawnCaract 
{
	public bool SpawnEnable = false;
	public Transform SpawnPos;
	public List<ObjectCaract> ObjAttached;
}

[System.Serializable]
public class ObjectCaract 
{
	public GameObject ObjectPref;
	public float PourcSpawn = 100;
	public float MaxStartSpeed = 10;
}