using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class NPCSpawner : MonoBehaviour
    {
        public PawnConfig Data;

        private AIController _spawnedNPC;

        private void OnEnable()
        {
            Spawn();
        }

        public void Spawn()
        {
            if (_spawnedNPC == null)
            {
                _spawnedNPC = LeanPool.Spawn(GameManager.StaticInstance.ObjectManager.AIControllerPrefab, transform.position, transform.rotation).GetComponent<AIController>();
                _spawnedNPC.Initialize();
                _spawnedNPC.Pawn.CreatePawn(Data.GetData(), "NPC");
            }
            else if (_spawnedNPC.Pawn.IsDead)
            {
                _spawnedNPC.Pawn.transform.SetLocalPositionAndRotation(transform.position, transform.rotation);
                _spawnedNPC.Pawn.Revive();
            }
        }
    }
}