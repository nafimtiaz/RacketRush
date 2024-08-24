using UnityEngine;

namespace RacketRush.RR.Views
{
    public class BaseView : MonoBehaviour
    {
        protected virtual bool IsValidComponent => true;

        private void Awake()
        {
            if (!IsValidComponent)
            {
                Debug.LogError($"Component [{name}] on Gameobject [{gameObject.name}] is INVALID!!");
            }
        }
    }
}