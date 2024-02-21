using ScenarioFlow;
using ScenarioFlow.Scripts.SFText;

namespace ConsoleSFSample
{
	/// <summary>
	/// Provides decoders for primitive types.
	/// </summary>
	public class PrimitiveDecoder : IReflectable
	{
		private static readonly string HalfSpaceSymbol = "<sp>";

		[DecoderMethod]
		[Description("A decoder for the 'int' type.")]
		public int ConvertToInt(string input)
		{
			return int.Parse(input);
		}

		[DecoderMethod]
		[Description("A decoder for the 'float' type.")]
		public float ConvertToFloat(string input)
		{
			return float.Parse(input);
		}

		[DecoderMethod]
		[Description("A decoder for the 'string' type.")]
		[Description("The symbol '<sp>' will be replaced by a half-width white space.")]
		public string ConvertToString(string input)
		{
			input = input.Replace(HalfSpaceSymbol, " ");
			return input;
		}
	}
}