using Cysharp.Threading.Tasks;
using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;
using System.Threading;
using UnityEngine;

namespace ConsoleSFSample
{
	/// <summary>
	/// Provides functions to display dialogue texts on the console.
	/// </summary>
	public class ConsoleDialogueWriter : IReflectable
	{
		[CommandMethod("log dialogue async")]
		[Category("Dialogue")]
		[Description("Display a character name and a dilogue line on the console.")]
		[Snippet("{${1:name}}:")]
		[Snippet("{${2:line}}")]
		public UniTask LogDialogueAsync(string characterName, string dialogueLine, CancellationToken cancellationToken)
		{
			Debug.Log($"{characterName}: {dialogueLine}");
			return UniTask.CompletedTask;
		}

		[CommandMethod("log colorful dialogue async")]
		[Category("Dialogue")]
		[Description("Display a character name and a dilogue line on the console.")]
		[Description("You can specify the text color.")]
		[Snippet("{${1:name}}:")]
		[Snippet("{${2:line}}")]
		[Snippet("{${3:#FFFFFF}}")]
		[DialogueSnippet("Text color: {${1:#FFFFFF}}")]
		public UniTask LogColorfulDialogueAsync(string characterName, string dialogueLine, Color textColor, CancellationToken cancellationToken)
		{
			var colorCode = ColorUtility.ToHtmlStringRGB(textColor);
			return LogDialogueAsync($"<color=#{colorCode}>{characterName}</color>", $"<color=#{colorCode}>{dialogueLine}</color>", cancellationToken);
		}
	}
}