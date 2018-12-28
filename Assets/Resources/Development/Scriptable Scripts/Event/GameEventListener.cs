using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Raskulls.Event
{
    public class GameEventListener : MonoBehaviour
    {
        public GameEvent Event;
        public UnityEvent Response;

        internal void OnEnable()
        {
            Event.RegisterListener(this);
        }
        internal void OnDisable()
        {
            Event.UnregisterListener(this);
        }
        internal void OnEventRaised()
        {
            Response.Invoke();
        }
    }
}
