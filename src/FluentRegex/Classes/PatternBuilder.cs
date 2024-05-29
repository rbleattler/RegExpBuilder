using System;
using System.Text;
namespace FluentRegex;

/// <summary>
/// Builds a regex pattern.
/// </summary>
/// <example>
/// <para>This example shows how to build a regex pattern using the <see cref="PatternBuilder"/> class.</para>
/// <code language="csharp">
/// class Program
/// {
///   static void Main()
///   {
///     var regexBuilder = new PatternBuilder();
///     var regex = regexBuilder
///         .StartAnchor().Build()
///         .StartGroup()
///             .AppendLiteral("abc")
///             .StartCharacterClass()
///                 .AppendLiteral("0-9")
///                 .Build()
///             .StartGroup()
///                 .AppendLiteral("xyz")
///                 .EndGroup()
///             .Build()
///         .EndAnchor()
///         .ToString();
///     Console.WriteLine($"Generated Regex: {regex}");
///   }
/// }
/// </code>
/// </example>
public class PatternBuilder : Builder, IBuilder
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PatternBuilder"/> class.
  /// </summary>
  public override StringBuilder Pattern
  {
    get => _pattern;
    set => _pattern = value;
  }
  // internal StringBuilder _pattern = new StringBuilder();


  /// <summary>
  /// Validates, then returns the <see cref="PatternBuilder"/>.
  /// </summary>
  public override PatternBuilder Build()
  {
    Validate();
    return this;
  }


  /// <summary>
  /// Adds an <see cref="AnchorBuilder"/> to the pattern.
  /// </summary>
  /// <returns><see cref="AnchorBuilder"/></returns>
  public override AnchorBuilder StartAnchor()
  {
    return new AnchorBuilder(this);
  }

  /// <summary>
  /// Adds a <see cref="GroupBuilder"/> to the pattern.
  /// </summary>
  /// <returns><see cref="GroupBuilder"/></returns>
  ///
  public override GroupBuilder StartGroup()
  {
    return CaptureGroup();
  }

  /// <summary>
  /// Adds a <see cref="GroupBuilder"/> to the pattern.
  /// </summary>
  /// <returns><see cref="GroupBuilder"/></returns>
  public GroupBuilder CaptureGroup()
  {
    return new GroupBuilder(this);
  }

  /// <summary>
  /// Adds a <see cref="GroupBuilder"/> to the pattern.
  /// </summary>
  public GroupBuilder NonCaptureGroup()
  {
    return new GroupBuilder(this, GroupType.NonCapturing);
  }

  /// <summary>
  /// Adds a <see cref="GroupBuilder"/> to the pattern.
  /// </summary>
  /// <param name="namedGroupStyle"></param>
  /// <param name="groupName"></param>
  public GroupBuilder NamedCaptureGroup(NamedGroupStyle namedGroupStyle = NamedGroupStyle.AngleBrackets, string? groupName = null)
  {
    return new GroupBuilder(this, namedGroupStyle, groupName);
  }


  /// <summary>
  /// Adds a <see cref="CharacterClassBuilder"/> to the pattern.
  /// </summary>
  /// <returns><see cref="CharacterClassBuilder"/></returns>
  public override CharacterClassBuilder StartCharacterClass()
  {
    return new CharacterClassBuilder(this);
  }

}


