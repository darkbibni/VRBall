using System.Collections.Generic;
using UnityEngine;

namespace VRBall 
{

	public class SpawnManager : MonoBehaviour
	{
        public int timerFirstSpawn = 2;
		public Vector2 TimeMinMaxSpawn;

		public List<SpawnCaract> AllSpawn;
		public Transform stockGarbage;

		List<GameObject> getBalls;
		//List<GarbageColl> preOjb;

		public int timeBeforeSpawn = 2;

		int saveLast = 0;
		int getCurr;
		int getTotalOb = 0;

		void Awake()
		{
			getBalls = new List<GameObject> ();
			timeBeforeSpawn = (int)Random.Range(TimeMinMaxSpawn.x, TimeMinMaxSpawn.y);
		}

		void Update()
		{
			getCurr = (int)Time.timeSinceLevelLoad;

			if ((getCurr == timerFirstSpawn || getCurr % timeBeforeSpawn == 0) && saveLast != getCurr)
			{
                saveLast = getCurr;
                timeBeforeSpawn = (int)Random.Range(TimeMinMaxSpawn.x, TimeMinMaxSpawn.y);

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
            
            // Determine where ball will spawn.
            int spawnRoomIndex = Random.Range(0, GameManager.instance.RoomUnlocked);

            int ballCount;
			int c;
			bool objSpawn;

			// Spawn balls in the random selected spawner.
			
            // Random number of balls spawned.
            ballCount = Random.Range((int)getAllSpawn[spawnRoomIndex].MinMaxSpawn.x, (int)getAllSpawn[spawnRoomIndex].MinMaxSpawn.y);
            for (int i = ballCount; i > 0; i--)
			{
				objSpawn = false;

				// Spawn all balls.
				do
				{
					for (c = 0; c < getAllSpawn[spawnRoomIndex].ObjAttached.Count; c++)
					{
						if ( Random.Range(0, 101) < getAllSpawn[spawnRoomIndex].ObjAttached[c].PourcSpawn )
						{
							objSpawn = true;

							getSpawnPos = getAllSpawn[spawnRoomIndex].SpawnPos;
							
							getNewObj = (GameObject)Instantiate(getAllSpawn[spawnRoomIndex].ObjAttached[c].ObjectPref, getSpawnPos);
							getNewObj.name = getTotalOb.ToString();
							getTotalOb ++;
						
							newObjSize = getNewObj.GetComponent<MeshRenderer>().bounds.size;
							newObjSize = new Vector3(newObjSize.x / 2, newObjSize.y / 2, newObjSize.z / 2);

                            // Assign sound manager to the ball.
                            BallSounds wb = getNewObj.GetComponent<BallSounds>();
                            wb.soundMgr = GameManager.instance.soundMgr;

							getSize = getSpawnPos.GetComponent<BoxCollider>().size;
							getSize = new Vector3(getSize.x / 2 - newObjSize.x, getSize.y / 2 - newObjSize.y, getSize.z / 2 - newObjSize.z);
							getSize = new Vector3(Random.Range(-getSize.x, getSize.x), Random.Range(-getSize.y, getSize.y), Random.Range(-getSize.z, getSize.z));

							getNewObj.transform.localPosition = getSize - getSpawnPos.up;

							getNewRig = getNewObj.GetComponent<Rigidbody>();
							getNewRig.velocity = -getSpawnPos.up * Random.Range(0, getAllSpawn[spawnRoomIndex].ObjAttached[c].MaxStartSpeed);

							balls.Add ( getNewObj );

							break;
						}
					}
				} while (!objSpawn);

                // Spawn feedback.
                GameManager.instance.SpawnFeedback(spawnRoomIndex);
			}
		}

		public void RemoveObj ( GameObject thisObj )
		{
			List<GameObject> balls = getBalls;
			for ( int a = 0; a < balls.Count; a++ )
			{
				if ( balls [ a ] == null )
				{
					balls.RemoveAt ( a );
					a--;
				}

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
            for (int i = 0; i < balls.Count; i++)
            {
                balls[i].GetComponent<ObjSpawnable>().Clean();
            }
            balls.Clear();
		}

		/*GameObject getObjGarb ( )
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
		}*/

		/*public void ReAddObj ( GameObject thisObj )
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
		}*/
	}

	[System.Serializable]
	public class SpawnCaract
	{
		public Transform SpawnPos;
		public List<BallParams> ObjAttached;
		public Vector2 MinMaxSpawn;
	}

	/*public class GarbageColl 
	{
		public GameObject thisObj;
		public bool canBeUse = true;
	}*/
}