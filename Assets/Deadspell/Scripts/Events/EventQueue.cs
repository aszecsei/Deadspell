using System;
using System.Collections.Generic;
using UnityEngine;

namespace Deadspell.Events
{
    public static class EventQueue
    {
        public static Queue<IEvent> Queue = new Queue<IEvent>();

        public static List<Tuple<Type, Func<IEvent, bool>>> Subscribers = new List<Tuple<Type, Func<IEvent, bool>>>();

        public static void SendEvent(IEvent ev)
        {
            Queue.Enqueue(ev);
        }

        public static void ListenForEvent<T>(Func<T, bool> processor)
        where T : class, IEvent
        {
            Subscribers.Add(new Tuple<Type, Func<IEvent, bool>>(typeof(T), ev => processor(ev as T)));
        }

        public static void ProcessQueue()
        {
            while (Queue.Count > 0)
            {
                var ev = Queue.Dequeue();

                bool heard = false;
                foreach (var (ty, processor) in Subscribers)
                {
                    if (ty.IsInstanceOfType(ev))
                    {
                        heard = true;
                        if (processor(ev))
                        {
                            break;
                        }
                    }
                }

                if (!heard)
                {
                    Debug.LogError($"Event {ev} was not heard by any listeners!");
                }
            }
        }
    }
}