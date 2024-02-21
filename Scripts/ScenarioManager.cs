using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using ScenarioFlow;
using ScenarioFlow.Scripts;
using ScenarioFlow.Tasks;
using UnityEngine;

namespace ConsoleSFSample
{
	public class ScenarioManager : MonoBehaviour
    {
        [SerializeField]
        private ScenarioScript scenarioScript;

        private void Start()
        {
            //Build ScenarioBookReader ------
            EnterKeyNotifier enterKeyNotifier = new EnterKeyNotifier();

            INextNotifier nextNotifier = enterKeyNotifier;
            ICancellationNotifier cancellationNotifier = enterKeyNotifier;
            ScenarioTaskExecutor scenarioTaskExecutor = new ScenarioTaskExecutor(nextNotifier, cancellationNotifier);

            IScenarioTaskExecutor scenarioTaskExecutorInterface = scenarioTaskExecutor;
            ScenarioBookReader scenarioBookReader = new ScenarioBookReader(scenarioTaskExecutorInterface);
            //------

            //Build ScenarioBookPublisher ------
            //Register commands and decoders
            ILabelOpener labelOpener = scenarioBookReader;
            ICancellationTokenDecoder cancellationTokenDecoder = scenarioTaskExecutor;
            ScenarioBookPublisher scenarioBookPublisher = new ScenarioBookPublisher(
                new IReflectable[]
                {
                    new BranchMaker(labelOpener),
                    new CancellationTokenDecoder(cancellationTokenDecoder),
                    new ColorDecoder(),
                    new ConsoleDialogueWriter(),
                    new DelayMaker(),
                    new PrimitiveDecoder(),
                });
            //------

			//The skip mode is switched with the S key
			ISkipActivator skipActivator = scenarioTaskExecutor;
			skipActivator.Duration = 0.05f;
			UniTaskAsyncEnumerable.EveryUpdate()
				.Select(_ => Input.GetKeyDown(KeyCode.S))
				.Where(x => x)
				.ForEachAsync(_ =>
				{
					skipActivator.IsActive = !skipActivator.IsActive;
					Debug.Log($"Skip Mode: {skipActivator.IsActive}");
				}, cancellationToken: this.GetCancellationTokenOnDestroy()).Forget();


            //Start running a script ------
			//Convert the scenario script to a scenario book
			IScenarioBookPublisher scenarioBookPublisherInterface = scenarioBookPublisher;
            IScenarioScript scenarioScriptInterface = scenarioScript;
            ScenarioBook scenarioBook = scenarioBookPublisherInterface.Publish(scenarioScriptInterface);

            //Start to read the scenario book
            IScenarioBookReader scenarioBookReaderInterface = scenarioBookReader;
            scenarioBookReaderInterface.ReadAsync(scenarioBook, this.GetCancellationTokenOnDestroy()).Forget();
            //------
        }
    }
}