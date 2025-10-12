using System.Collections;
using Creatures.CreaturesStateMachine.CreatureBasic;
using Creatures.Weapons;
using UnityEngine;

namespace Components.Spawn
{
    public class SpawnProjectTiles : SpawnComponent
    {
        [SerializeField] private BasicCreature owner;
        [SerializeField] private float spawnDelay = 0.5f;
        private void OnEnable() => owner.SubscribeOnThrowEvent(SpawnRoutine);
        
        private void OnDisable() => owner.UnsubscribeOnThrowEvent(SpawnRoutine);
        
        [ContextMenu("Spawn ProjectTiles")]
        
        public void SpawnRoutine(int amount) => StartCoroutine(Spawn(amount));
        
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

        private IEnumerator Spawn(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Spawn();
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}