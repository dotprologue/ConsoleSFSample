using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow.Tasks;
using System.Threading;
using UnityEngine;

namespace ConsoleSFSample
{
    /// <summary>
    /// Provides notifiers that trigger the next/cancellation instruction when the enter key is pressed.
    /// </summary>
	public class EnterKeyNotifier : INextNotifier, ICancellationNotifier
    {
        public UniTask NotifyNextAsync(CancellationToken cancellationToken)
        {
            return WaitUntilEnterPressedAsync(cancellationToken);
        }

        public UniTask NotifyCancellationAsync(CancellationToken cancellationToken)
        {
            return WaitUntilEnterPressedAsync(cancellationToken);
        }

        private UniTask WaitUntilEnterPressedAsync(CancellationToken cancellationToken)
        {
            return UniTaskAsyncEnumerable
                .EveryUpdate()
                .Select(_ => Input.GetKeyDown(KeyCode.Return))
                .Where(x => x)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }
    }
}