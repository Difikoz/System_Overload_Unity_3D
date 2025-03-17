using System.Collections.Generic;
using UnityEngine;

namespace WinterUniverse
{
    [System.Serializable]
    public class ArmorRenderer
    {
        [SerializeField] private ArmorItemConfig _config;
        [SerializeField] private List<GameObject> _meshes = new();

        public ArmorItemConfig Config => _config;

        public void Toggle(bool visible)
        {
            if (visible)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            foreach (GameObject go in _meshes)
            {
                go.SetActive(true);
            }
        }

        public void Hide()
        {
            foreach (GameObject go in _meshes)
            {
                go.SetActive(false);
            }
        }
    }
}