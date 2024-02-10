using Code.Combat.Units.Entities;
using UnityEngine;

namespace Code.Combat {

    public class Context
    {
        public UnitEntity Source { get; private set; }
        public UnitEntity Receiver { get; private set; }
        public Vector3 Position { get; private set; }
        
        public string SourceName  { get; private set; }
        public string ReceiverName  { get; private set; }
    
        public Context() {
        }
        public Context(UnitEntity source) {
            Source = source;
            Position = Source.GetPosition();
        }
        
        public Context GetCopy() {
            return new Context {
                Source = Source,
                Receiver = Receiver,
                Position = Position
            };
        }

        public void SetSource(UnitEntity source) {
            Source = source;
            SourceName = source.GetName();
        }

        public void SetReceiver(UnitEntity receiver) {
            if (receiver == null) {
                Receiver = null;
                ReceiverName = "null";
                return;
            }
            Receiver = receiver;
            ReceiverName = receiver.GetName();
        }

        public void SetPosition(Vector3 position) {
            Position = position;
        }
    }

}
