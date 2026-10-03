using System;
using Unity.Behavior;
using UnityEngine;
using Unity.Properties;

#if UNITY_EDITOR
[CreateAssetMenu(menuName = "Behavior/Event Channels/Enemy Vision Event")]
#endif
[Serializable, GeneratePropertyBag]
[EventChannelDescription(name: "Enemy Behavior Event", message: "Vision Found [Collision] which [hasEntered]", category: "Events", id: "fad65e1636ad94b5aee60ea6b639f29d")]
public sealed partial class EnemyVisionEvent : EventChannel<GameObject, bool> { }

