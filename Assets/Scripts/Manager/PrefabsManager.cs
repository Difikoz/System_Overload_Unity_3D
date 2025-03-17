//using Lean.Pool;
using UnityEngine;

namespace WinterUniverse
{
    public class PrefabsManager : MonoBehaviour
    {
        [SerializeField] private GameObject _playerControllerPrefab;
        [SerializeField] private GameObject _npcControllerPrefab;
        [SerializeField] private GameObject _pawnControllerPrefab;

        //public PlayerController GetPlayer(Transform point)
        //{
        //    return GetPlayer(point.position, point.rotation);
        //}

        //public PlayerController GetPlayer(Vector3 position, Quaternion rotation)
        //{
        //    return LeanPool.Spawn(_playerControllerPrefab, position, rotation).GetComponent<PlayerController>();
        //}

        //public NPCController GetNPC(Transform point)
        //{
        //    return GetNPC(point.position, point.rotation);
        //}

        //public NPCController GetNPC(Vector3 position, Quaternion rotation)
        //{
        //    return LeanPool.Spawn(_pawnControllerPrefab, position, rotation).GetComponent<NPCController>();
        //}

        //public PawnController GetPawn(Transform point)
        //{
        //    return GetPawn(point.position, point.rotation);
        //}

        //public PawnController GetPawn(Vector3 position, Quaternion rotation)
        //{
        //    return LeanPool.Spawn(_pawnControllerPrefab, position, rotation).GetComponent<PawnController>();
        //}

        //public void DespawnObject(GameObject go, float delay = 0f)
        //{
        //    LeanPool.Despawn(go, delay);
        //}
    }
}