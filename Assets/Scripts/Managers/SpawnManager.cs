﻿using System.Collections.Generic;
using UnityEngine;

namespace VRBall {

    public class SpawnManager : MonoBehaviour
    {
        public Vector2 TimeMinMaxSpawn;
        public List<SpawnCaract> AllSpawn;

        public int timeBeforeSpawn = 2;

        int saveLast = 0;
        int getCurr;

        void Awake()
        {
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
                for (b = Random.Range(1, 3); b > 0; b--)
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
                                getNewObj = (GameObject)Instantiate(getAllSpawn[a].ObjAttached[c].ObjectPref, getSpawnPos);

                                newObjSize = getNewObj.GetComponent<MeshRenderer>().bounds.size;
                                newObjSize = new Vector3(newObjSize.x / 2, newObjSize.y / 2, newObjSize.z / 2);

                                getSize = getSpawnPos.GetComponent<BoxCollider>().size;
                                getSize = new Vector3(getSize.x / 2 - newObjSize.x, getSize.y / 2 - newObjSize.y, getSize.z / 2 - newObjSize.z);
                                getSize = new Vector3(Random.Range(-getSize.x, getSize.x), Random.Range(-getSize.y, getSize.y), Random.Range(-getSize.z, getSize.z));

                                getNewObj.transform.localPosition = getSize - getSpawnPos.up;

                                getNewRig = getNewObj.GetComponent<Rigidbody>();
                                getNewRig.velocity = -getSpawnPos.up * Random.Range(0, getAllSpawn[a].ObjAttached[c].MaxStartSpeed);
                                
                                break;
                            }
                        }
                    } while (!objSpawn);
                }
            }
        }
    }

    [System.Serializable]
    public class SpawnCaract
    {
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

}