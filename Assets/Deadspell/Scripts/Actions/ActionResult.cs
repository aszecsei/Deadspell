using System;

namespace Deadspell.Actions
{
    public class ActionResult
    {
        [Flags]
        public enum ActionResultFlags : byte
        {
            Default = 0,
            Done = 1 << 1,
            NeedsPause = 1 << 2,
            Failed = 1 << 3,
            CheckForCancel = 1 << 4,
        }

        public static ActionResult Done => new ActionResult(ActionResultFlags.Done);

        public static ActionResult DoneAndPause =>
            new ActionResult(ActionResultFlags.Done | ActionResultFlags.NeedsPause);

        public static ActionResult NotDone => new ActionResult(ActionResultFlags.Default);
        public static ActionResult Fail => new ActionResult(ActionResultFlags.Failed | ActionResultFlags.Done);

        public static ActionResult CheckForCancel =>
            new ActionResult(ActionResultFlags.CheckForCancel | ActionResultFlags.Done);

        private ActionResultFlags _flags = ActionResultFlags.Default;
        private Action _alternate = null;

        public Action Alternate => _alternate;

        public bool Success => !_flags.HasFlag(ActionResultFlags.Failed);
        public bool NeedsPause => _flags.HasFlag(ActionResultFlags.NeedsPause);
        public bool IsDone => _flags.HasFlag(ActionResultFlags.Done);
        public bool NeedsCheckForCancel => _flags.HasFlag(ActionResultFlags.CheckForCancel);

        public ActionResult(ActionResultFlags flags)
        {
            _flags = flags;
        }

        public ActionResult(Action alternate)
        {
            _alternate = alternate;
        }
    }
}