using System.Collections.Generic;
using System.Collections;
using UnityEngine;

namespace VRBall 
{

	public class SpawnManager : MonoBehaviour
	{
		public Vector2 TimeMinMaxSpawn;
		public List<SpawnCaract> AllSpawn;
		public Transform stockGarbage;

		List<GameObject> getBalls;
		List<GarbageColl> preOjb;

		public int timeBeforeSpawn = 2;

		int saveLast = 0;
		int getCurr;
		int getTotalOb = 0;



		void Awake()
		{
			/*preOjb = new List<GarbageColl> ( 100 );

			GarbageColl getObj;
			for ( int a = 0; a < 100; a++ )
			{
				getObj = new GarbageColl ( );
				getObj.thisObj = new GameObject ( );
				getObj.thisObj.name = a.ToString ( );
				getObj.thisObj.transform.SetParent ( stockGarbage );

				preOjb.Add ( getObj );
			}*/
			getBalls = new List<GameObject> ( );
			timeBeforeSpawn = (int)Random.Range(TimeMinMaxSpawn.x, TimeMinMaxSpawn.y);
		}

		void Update()
		{
			getCurr = (int)Time.timeSinceLevelLoad;

			if (getCurr % timeBeforeSpawn == 0 && saveLast != getCurr)
			{
				timeBeforeSpawn = (int)Random.Range(TimeMinMaxSpawn.x, TimeMinMaxSpawn.y);
				saveLast = getCurr;

				newSpawn();
			}
		}

		void newSpawn()
		{
			List<SpawnCaract> getAllSpawn = AllSpawn;
			List<GameObject> balls = getBalls;
			Transform getSpawnPos;
			GameObject getNewObj;
			Rigidbody getNewRig;
			Vector3 getSize;
			Vector3 newObjSize;

			int a;
			int b;
			int c;
			bool objSpawn;
			int roomsUnlocked = GameManager.instance.RoomUnlocked;

			// Spawn balls in all spawners.
			for (a = 0; a < roomsUnlocked; a++)
			{
				// Random number of balls spawned.
				for (b = Random.Range(1, 2); b > 0; b--)
				{
					objSpawn = false;

					// Spawn all balls.
					do
					{
						for (c = 0; c < getAllSpawn[a].ObjAttached.Count; c++)
						{
							if (Random.Range(0, 101) < getAllSpawn[a].ObjAttached[c].PourcSpawn)
							{
								objSpawn = true;

								getSpawnPos = getAllSpawn[a].SpawnPos;
								/*getNewObj = getObjGarb();

								if ( getNewObj == null)
								{
									return;
								}*/
								getNewObj = (GameObject)Instantiate(getAllSpawn[a].ObjAttached[c].ObjectPref, getSpawnPos);
								getNewObj.name = getTotalOb.ToString();
								getTotalOb ++;
                                //getNewObj = getAllSpawn[a].ObjAttached[c].ObjectPref;
                                //getNewObj.transform.SetParent ( getSpawnPos );

                                WaitBump wb = getNewObj.GetComponent<WaitBump>();
                                wb.audioBump = GameManager.instance.soundMgr;

                                newObjSize = getNewObj.GetComponent<MeshRenderer>().bounds.size;
								newObjSize = new Vector3(newObjSize.x / 2, newObjSize.y / 2, newObjSize.z / 2);

								getSize = getSpawnPos.GetComponent<BoxCollider>().size;
								getSize = new Vector3(getSize.x / 2 - newObjSize.x, getSize.y / 2 - newObjSize.y, getSize.z / 2 - newObjSize.z);
								getSize = new Vector3(Random.Range(-getSize.x, getSize.x), Random.Range(-getSize.y, getSize.y), Random.Range(-getSize.z, getSize.z));

								getNewObj.transform.localPosition = getSize - getSpawnPos.up;

								getNewRig = getNewObj.GetComponent<Rigidbody>();
								getNewRig.velocity = -getSpawnPos.up * Random.Range(0, getAllSpawn[a].ObjAttached[c].MaxStartSpeed);

								balls.Add ( getNewObj );

								break;
							}
						}
					} while (!objSpawn);
				}
			}
		}

		public void RemoveObj ( GameObject thisObj )
		{
			List<GameObject> balls = getBalls;
			for ( int a = 0; a < balls.Count; a++ )
			{
				if ( balls [ a ].name == thisObj.name )
				{
					balls.RemoveAt ( a );
					break;
				}
			}
		}

		public void ClearObj ( )
		{
			getTotalOb = 0;
			List<GameObject> balls = getBalls;
			while ( balls.Count > 0 )
			{
				StartCoroutine ( balls [ 0 ].GetComponent<ObjSpawnable> ( ).FadeThenDestroy ( ) );
				balls.RemoveAt ( 0 );
			}
		}

		GameObject getObjGarb ( )
		{
			List<GarbageColl> getGarb = preOjb;

			for ( int a = 0; a < getGarb.Count; a++ )
			{
				if ( getGarb [ a ].canBeUse )
				{
					getGarb [ a ].canBeUse = false;
					return getGarb [ a ].thisObj;
				}
			}

			return null;
		}

		public void ReAddObj ( GameObject thisObj )
		{
			List<GarbageColl> getGarb = preOjb;

			for ( int a = 0; a < getGarb.Count; a++ )
			{
				if ( getGarb [ a ].thisObj == thisObj )
				{
					getGarb [ a ].canBeUse = true;
					return;
				}
			}
		}
	}

	[System.Serializable]
	public class SpawnCaract
	{
		public Transform SpawnPos;
		public List<ObjectCaract> ObjAttached;
        public Vector2 ballsSpawned;
	}

	[System.Serializable]
	public class ObjectCaract
	{
		public GameObject ObjectPref;
		public float PourcSpawn = 100;
		public float MaxStartSpeed = 10;
	}

	public class GarbageColl 
	{
		public GameObject thisObj;
		public bool canBeUse = true;
	}

}