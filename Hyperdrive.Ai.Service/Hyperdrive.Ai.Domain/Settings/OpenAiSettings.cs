namespace Hyperdrive.Ai.Domain.Settings;

/// <summary>
///     Represents a <see cref="OpenAiSettings" /> class
/// </summary>
public class OpenAiSettings
{
    /// <summary>
    ///     Gets or Sets <see cref="Embedding" />
    /// </summary>
    public EmbeddingSettings Embedding { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Chat" />
    /// </summary>
    public ChatSettings Chat { get; set; }
}

/// <summary>
///     Represents a <see cref="EmbeddingSettings" /> class
/// </summary>
public class EmbeddingSettings
{
    /// <summary>
    ///     Gets or Sets <see cref="Model" />
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Key" />
    /// </summary>
    public string Key { get; set; }
}


/// <summary>
///     Represents a <see cref="ChatSettings" /> class
/// </summary>
public class ChatSettings
{
    /// <summary>
    ///     Gets or Sets <see cref="Model" />
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    ///     Gets or Sets <see cref="Key" />
    /// </summary>
    public string Key { get; set; }
}