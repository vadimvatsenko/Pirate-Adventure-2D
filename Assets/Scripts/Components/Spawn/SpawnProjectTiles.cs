using System.Collections;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.Weapons;
using UnityEngine;

namespace Components.Spawn
{
    public class SpawnProjectTiles : SpawnComponent
    {
        [SerializeField] private Creature owner;
        [SerializeField] private float duration = 3f;
        
        private float _timer = 0;
        
        

        private void OnEnable()
        {
            owner.SubscribeOnThrowEvent(Spawn);
        }

        private void OnDisable()
        {
            owner.UnsubscribeOnThrowEvent(Spawn);
        }

        public override void Spawn() => StartCoroutine(SpawnPjTiles());


        private IEnumerator SpawnPjTiles()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector3 spawnPos = target.position;
                
                GameObject spawnObj = Instantiate(prefab, spawnPos, Quaternion.identity);
            
                ProjectTile projectTile = spawnObj.GetComponent<ProjectTile>();
            
                if (projectTile != null)
                {
                    projectTile.SetDirection(owner.FacingDirection);
                }
            
                spawnObj.transform.localScale = target.lossyScale;
                spawnObj.transform.parent = SpawnParent.transform;
                spawnObj.SetActive(true);
                yield return new WaitForSeconds(1);
            }
        }
    }
}