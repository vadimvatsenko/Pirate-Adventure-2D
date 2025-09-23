using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.Weapons;
using UnityEngine;

namespace Components.Spawn
{
    public class SpawnProjectTiles : SpawnComponent
    {
        [SerializeField] private Creature owner;

        private void OnEnable()
        {
            owner.SubscribeOnThrowEvent(Spawn);
        }

        private void OnDisable()
        {
            owner.UnsubscribeOnThrowEvent(Spawn);
        }
        
        public override void Spawn()
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
        }
    }
}