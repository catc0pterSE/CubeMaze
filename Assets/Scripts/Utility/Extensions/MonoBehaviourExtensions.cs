using UnityEngine;

namespace Utility.Extensions
{
    public static class MonoBehaviourExtensions
    {
        public static void EnableObject(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.gameObject.SetActive(true);
        
        public static void DisableObject(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.gameObject.SetActive(false);

        public static void EnableComponent(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.enabled = true;

        public static void DisableComponent(this MonoBehaviour monoBehaviour) =>
            monoBehaviour.enabled = false;
    }
}