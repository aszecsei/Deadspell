using System;

namespace Deadspell.Core
{
    public class ActionResult
    {
        [Flags]
        public enum Flags : byte
        {
            Default = 0,
            Done = 1 << 1,
            NeedsPause = 1 << 2,
            Failed = 1 << 3,
            CheckForCancel = 1 << 4,
        }

        public static ActionResult Done => new ActionResult(Flags.Done);
        public static ActionResult DoneAndPause => new ActionResult(Flags.Done | Flags.NeedsPause);
        public static ActionResult NotDone => new ActionResult(Flags.Default);
        public static ActionResult Fail => new ActionResult(Flags.Failed);
        public static ActionResult CheckForCancel => new ActionResult(Flags.CheckForCancel | Flags.Done);

        public Action Alternate => _alternate;

        public bool Success => !_flags.HasFlag(Flags.Failed);
        public bool NeedsPause => _flags.HasFlag(Flags.NeedsPause);
        public bool IsDone => _flags.HasFlag(Flags.Done);
        public bool NeedsCheckForCancel => _flags.HasFlag(Flags.CheckForCancel);

        private Flags _flags;
        private Action _alternate;
        
        public ActionResult(Flags flags)
        {
            _flags = flags;
        }

        public ActionResult(Action alternate)
        {
            _alternate = alternate;
        }
    }
    
    public abstract class Action
    {
        public static implicit operator ActionResult(Action action)
        {
            return new ActionResult(action);
        }
        
        public abstract bool Execute();
    }
}